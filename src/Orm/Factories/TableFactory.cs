using Orm.Entities;
using Orm.Reflection;
using Orm.Utils;
using System;
using System.Collections.Concurrent;

namespace Orm.Factories
{
  public class TableFactory
  {
    private static readonly ConcurrentDictionary<Type, Table> _tableCache = new();
    private static readonly DelegateCreator _delegateCreator = new();

    public static Table GetOrCreateTableDefinition<T>()
    {
      var type = typeof(T);
      if (_tableCache.TryGetValue(type, out var table))
        return table;

      var props = PropertyCache<T>.Properties;
      var columns = new Column[props.Length];
      for (int i = 0; i < props.Length; i++)
      {
        var prop = props[i];
        columns[i] = new Column(prop.Name, AliasUtils.GetColumnName(prop), _delegateCreator.CreatePropertySetter(prop));
      }

      table = new Table(AliasUtils.GetTableName(type), columns);

      _tableCache.TryAdd(type, table);
      return table;
    }
  }
}
