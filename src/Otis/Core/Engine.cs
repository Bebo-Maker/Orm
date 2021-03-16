using Otis.Attributes;
using Otis.Entities;
using Otis.Factories;
using Otis.ObjectCreator;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Otis.Core
{
  internal class Engine : IEngine
  {
    private readonly ITableFactory _tableFactory;
    private readonly IObjectCreator _objectCreator;

    public Engine(IObjectCreator objectCreator, ITableFactory tableFactory)
    {
      _objectCreator = objectCreator;
      _tableFactory = tableFactory;
    }

    public List<T> CreateObjects<T>(IDataReader reader)
    {
      var type = typeof(T);
      var ctor = GetConstructorWithDbConstructorAttribute(type);

      return ctor != null
          ? HandleConstructorInjection<T>(reader, type, ctor.GetParameters().Select(a => a.Name.ToLower()).ToArray())
          : HandlePropertyInjection<T>(reader, type);
    }

    public Task<List<T>> CreateObjectsAsync<T>(DbDataReader reader)
    {
      var type = typeof(T);
      var ctor = GetConstructorWithDbConstructorAttribute(type);

      return ctor != null
          ? HandleConstructorInjectionAsync<T>(reader, type, ctor.GetParameters().Select(a => a.Name.ToLower()).ToArray())
          : HandlePropertyInjectionAsync<T>(reader, type);
    }

    private static ConstructorInfo GetConstructorWithDbConstructorAttribute(Type type) => type.GetConstructors()
                                                                                              .FirstOrDefault(c => c.CustomAttributes.FirstOrDefault(a => a.AttributeType == typeof(DatabaseConstructorAttribute)) != null);

    private List<T> HandleConstructorInjection<T>(IDataReader reader, Type type, string[] parameters)
    {
      var entities = new List<T>();

      while (reader.Read())
        entities.Add(CreateEntity<T>(reader, type, parameters));

      return entities;
    }

    private async Task<List<T>> HandleConstructorInjectionAsync<T>(DbDataReader reader, Type type, string[] parameters)
    {
      var entities = new List<T>();

      while (await reader.ReadAsync().ConfigureAwait(false))
        entities.Add(CreateEntity<T>(reader, type, parameters));

      return entities;
    }

    private T CreateEntity<T>(IDataReader reader, Type type, string[] parameters)
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

    private List<T> HandlePropertyInjection<T>(IDataReader reader, Type type)
    {
      var entities = new List<T>();
      var table = _tableFactory.GetOrCreateTable(type);

      while (reader.Read())
        entities.Add(CreateEntity<T>(reader, type, table));

      return entities;
    }

    private async Task<List<T>> HandlePropertyInjectionAsync<T>(DbDataReader reader, Type type)
    {
      var entities = new List<T>();
      var table = _tableFactory.GetOrCreateTable(type);

      while (await reader.ReadAsync().ConfigureAwait(false))
        entities.Add(CreateEntity<T>(reader, type, table));

      return entities;
    }

    private T CreateEntity<T>(IDataReader reader, Type type, Table table)
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
