using Orm.Attributes;
using Orm.Entities;
using Orm.Factories;
using Orm.ObjectCreator;
using Orm.Querying;
using Orm.Querying.Builders;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Orm
{
  public class Database
  {
    private readonly string _connectionString;
    private static readonly IObjectCreator _objectCreator = new ActivatorObjectCreator();
    private static readonly ISqlTranslator _translator = new SqlTranslator();

    public Database(string connectionString)
    {
      _connectionString = connectionString;
    }

    public List<T> Query<T>(Action<IQueryBuilder<T>> action = null)
    {
      string sql = CreateSelectBuilder(action);

      return Query<T>(sql);
    }

    public List<T> Query<T>(string sqlStatement)
    {
      using var connection = new SqlConnection(_connectionString);
      connection.Open();
      using var cmd = new SqlCommand(sqlStatement, connection);
      var reader = cmd.ExecuteReader();
      return CreateObjects<T>(reader);
    }

    public Task<List<T>> QueryAsync<T>(Action<IQueryBuilder<T>> action = null)
    {
      string sql = CreateSelectBuilder(action);

      return QueryAsync<T>(sql);
    }

    public async Task<List<T>> QueryAsync<T>(string sqlStatement)
    {
      using var connection = new SqlConnection(_connectionString);
      await connection.OpenAsync().ConfigureAwait(false);
      var cmd = new SqlCommand(sqlStatement, connection);
      var reader = await cmd.ExecuteReaderAsync().ConfigureAwait(false);
      return await CreateObjectsAsync<T>(reader);
    }

    private static string CreateSelectBuilder<T>(Action<IQueryBuilder<T>> action)
    {
      var builder = new SelectQueryBuilder<T>(_translator);
      action?.Invoke(builder);
      return builder.Build();
    }

    private static List<T> CreateObjects<T>(SqlDataReader reader)
    {
      var type = typeof(T);
      var ctor = GetConstructorWithDbConstructorAttribute(type);

      return ctor != null
          ? HandleConstructorInjection<T>(reader, type, ctor.GetParameters().Select(a => a.Name.ToLower()).ToArray())
          : HandlePropertyInjection<T>(reader, type);
    }

    private static Task<List<T>> CreateObjectsAsync<T>(SqlDataReader reader)
    {
      var type = typeof(T);
      var ctor = GetConstructorWithDbConstructorAttribute(type);

      return ctor != null
          ? HandleConstructorInjectionAsync<T>(reader, type, ctor.GetParameters().Select(a => a.Name.ToLower()).ToArray())
          : HandlePropertyInjectionAsync<T>(reader, type);
    }

    private static ConstructorInfo GetConstructorWithDbConstructorAttribute(Type type) => type.GetConstructors().FirstOrDefault(c => c.CustomAttributes.FirstOrDefault(a => a.AttributeType == typeof(DatabaseConstructorAttribute)) != null);

    private static List<T> HandleConstructorInjection<T>(SqlDataReader reader, Type type, string[] parameters)
    {
      var entities = new List<T>();

      while (reader.Read())
        entities.Add(CreateEntity<T>(reader, type, parameters));

      return entities;
    }

    private static async Task<List<T>> HandleConstructorInjectionAsync<T>(SqlDataReader reader, Type type, string[] parameters)
    {
      var entities = new List<T>();

      while (await reader.ReadAsync().ConfigureAwait(false))
        entities.Add(CreateEntity<T>(reader, type, parameters));

      return entities;
    }

    private static T CreateEntity<T>(SqlDataReader reader, Type type, string[] parameters)
    {
      var dbData = new object[reader.FieldCount];
      for (int i = 0; i < reader.FieldCount; i++)
      {
        string fieldName = reader.GetName(i).ToLower();
        int index = Array.IndexOf(parameters, fieldName);
        if (index != -1)
          dbData[index] = reader[i];
        else
          throw new ArgumentException($"No matching parameter for field {fieldName}");
      }

      return _objectCreator.Create<T>(type, dbData);
    }

    private static List<T> HandlePropertyInjection<T>(SqlDataReader reader, Type type)
    {
      var entities = new List<T>();
      var table = TableFactory.GetOrCreateTableDefinition(type);

      while (reader.Read())
        entities.Add(CreateEntity<T>(reader, type, table));

      return entities;
    }

    private static async Task<List<T>> HandlePropertyInjectionAsync<T>(SqlDataReader reader, Type type)
    {
      var entities = new List<T>();
      var table = TableFactory.GetOrCreateTableDefinition(type);

      while (await reader.ReadAsync().ConfigureAwait(false))
        entities.Add(CreateEntity<T>(reader, type, table));

      return entities;
    }

    private static T CreateEntity<T>(SqlDataReader reader, Type type, Table table)
    {
      T entity = _objectCreator.Create<T>(type);
      for (int i = 0; i < reader.FieldCount; i++)
      {
        string fieldName = reader.GetName(i);
        table.SetValueOfColumn(fieldName, entity, reader[i]);
      }

      return entity;
    }
  }
}
