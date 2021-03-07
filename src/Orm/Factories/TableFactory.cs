using Orm.Entities;
using Orm.Reflection;
using Orm.Utils;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Orm.Factories
{
  public class TableFactory
  {
    private static readonly ConcurrentDictionary<Type, Table> _tableCache = new ConcurrentDictionary<Type, Table>();

    public static Table GetOrCreateTableDefinition(Type type)
    {
      if (_tableCache.TryGetValue(type, out var table))
        return table;

      var cols = new Dictionary<string, FastPropertyInfo>();
      var colNames = new List<string>();

      foreach(var prop in type.GetProperties())
      {
        string columnName = AliasUtils.GetColumnName(prop);
        colNames.Add(columnName);
        cols.Add(columnName, new FastPropertyInfo(prop));
      }
      
      table = new Table(AliasUtils.GetTableName(type), colNames.ToArray(), cols);

      _tableCache.TryAdd(type, table);
      return table;
    }
  }
}
