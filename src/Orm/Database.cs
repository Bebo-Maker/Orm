using Orm.Attributes;
using Orm.Reflection;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Orm
{
  public class Database
  {
    private readonly string _connectionString;

    private Database(string connectionString)
    {
      _connectionString = connectionString;
    }

    public static Database Connect(string connectionString) => new Database(connectionString);

    public List<T> Query<T>(string sqlStatement)
    {
      using var connection = new SqlConnection(_connectionString);
      connection.Open();
      using var cmd = new SqlCommand(sqlStatement, connection);
      var reader = cmd.ExecuteReader();
      return CreateObjects<T>(reader);
    }

    public async Task<List<T>> QueryAsync<T>(string sqlStatement)
    {
      using var connection = new SqlConnection(_connectionString);
      await connection.OpenAsync().ConfigureAwait(false);
      var cmd = new SqlCommand(sqlStatement, connection);
      var reader = await cmd.ExecuteReaderAsync().ConfigureAwait(false);
      return await CreateObjectsAsync<T>(reader);
    }

    private static List<T> CreateObjects<T>(SqlDataReader reader)
    {
      var type = typeof(T);
      var ctor = type.GetConstructors().FirstOrDefault(c => c.CustomAttributes.FirstOrDefault(a => a.AttributeType == typeof(DbConstructorAttribute)) != null);

      return ctor != null
          ? HandleConstructorInjection<T>(reader, type, ctor.GetParameters().Select(a => a.Name.ToLower()).ToArray())
          : HandlePropertyInjection<T>(reader, type);
    }

    private static Task<List<T>> CreateObjectsAsync<T>(SqlDataReader reader)
    {
      var type = typeof(T);
      var ctor = type.GetConstructors().FirstOrDefault(c => c.CustomAttributes.FirstOrDefault(a => a.AttributeType == typeof(DbConstructorAttribute)) != null);

      return ctor != null
          ? HandleConstructorInjectionAsync<T>(reader, type, ctor.GetParameters().Select(a => a.Name.ToLower()).ToArray())
          : HandlePropertyInjectionAsync<T>(reader, type);
    }

    private static async Task<List<T>> HandleConstructorInjectionAsync<T>(SqlDataReader reader, Type type, string[] parameters)
    {
      var entities = new List<T>();

      while (await reader.ReadAsync().ConfigureAwait(false))
      {
        var dbData = new object[reader.FieldCount];
        for (int i = 0; i < reader.FieldCount; i++)
        {
          string fieldName = reader.GetName(i).ToLower();
          int index = Array.IndexOf(parameters, fieldName);
          if (index != -1)
            dbData[index] = reader[i];
          else
            Console.WriteLine($"ERROR no matching paramter for field {fieldName}");
        }

        entities.Add((T)Activator.CreateInstance(type, dbData));
      }

      return entities;
    }

    private static async Task<List<T>> HandlePropertyInjectionAsync<T>(SqlDataReader reader, Type type)
    {
      var entities = new List<T>();
      var propertyMap = BuildPropertyMap<T>();

      T entity;

      while (await reader.ReadAsync().ConfigureAwait(false))
      {
        entity = (T)Activator.CreateInstance(type);
        for (int i = 0; i < reader.FieldCount; i++)
        {
          string fieldName = reader.GetName(i);
          propertyMap[fieldName]?.SetValue(entity, reader[i]);
        }
        entities.Add(entity);
      }

      return entities;
    }

    private static List<T> HandleConstructorInjection<T>(SqlDataReader reader, Type type, string[] parameters)
    {
      var entities = new List<T>();

      while (reader.Read())
      {
        var dbData = new object[reader.FieldCount];
        for (int i = 0; i < reader.FieldCount; i++)
        {
          string fieldName = reader.GetName(i).ToLower();
          int index = Array.IndexOf(parameters, fieldName);
          if (index != -1)
            dbData[index] = reader[i];
          else
            Console.WriteLine($"ERROR no matching paramter for field {fieldName}");
        }

        entities.Add((T)Activator.CreateInstance(type, dbData));
      }

      return entities;
    }

    private static List<T> HandlePropertyInjection<T>(SqlDataReader reader, Type type)
    {
      var entities = new List<T>();
      var propertyMap = BuildPropertyMap<T>();

      T entity;

      while (reader.Read())
      {
        entity = (T)Activator.CreateInstance(type);
        for (int i = 0; i < reader.FieldCount; i++)
        {
          string fieldName = reader.GetName(i);
          propertyMap[fieldName]?.SetValue(entity, reader[i]);
        }
        entities.Add(entity);
      }

      return entities;
    }

    private static Dictionary<string, FastPropertyInfo> BuildPropertyMap<T>()
    {
      var props = PropertyAccessor<T>.Properties;
      var propertyMap = new Dictionary<string, FastPropertyInfo>();
      for (int i = 0; i < props.Length; i++)
      {
        var prop = props[i];
        var dbColumnAttr = prop.GetCustomAttribute<DbColumnAttribute>();
        propertyMap.Add(dbColumnAttr?.Alias ?? prop.Name, prop);
      }

      return propertyMap;
    }
  }
}
