using Orm.Attributes;
using Orm.Entities;
using Orm.Reflection;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Reflection;

namespace Orm.Factories
{
  public class TableFactory
  {
    private static readonly ConcurrentDictionary<Type, TableDefinition> _tableCache = new ConcurrentDictionary<Type, TableDefinition>();

    public static TableDefinition GetOrCreateTableDefinition(Type type)
    {
      if (_tableCache.TryGetValue(type, out var table))
        return table;

      var cols = new Dictionary<string, FastPropertyInfo>();

      foreach(var prop in type.GetProperties())
      {
        var columnAttr = prop.GetCustomAttribute<ColumnAttribute>();
        cols.Add(columnAttr?.Alias ?? prop.Name, new FastPropertyInfo(prop));
      }

      var tableAttr = type.GetCustomAttribute<TableAttribute>();
      table = new TableDefinition(tableAttr?.Alias ?? type.Name, cols);

      _tableCache.TryAdd(type, table);
      return table;
    }
  }
}
