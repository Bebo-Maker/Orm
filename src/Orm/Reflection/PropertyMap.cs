using Orm.Attributes;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace Orm.Reflection
{
  public static class PropertyMap
  {
    private static readonly ConcurrentDictionary<Type, Dictionary<string, FastPropertyInfo>> _tableCache = new ConcurrentDictionary<Type, Dictionary<string, FastPropertyInfo>>();

    public static Dictionary<string, FastPropertyInfo> GetOrCreatePropertyMap<T>() => GetOrCreate<T>();

    private static Dictionary<string, FastPropertyInfo> GetOrCreate<T>()
    {
      var type = typeof(T);
      if (_tableCache.TryGetValue(type, out var table))
        return table;

      var props = type.GetProperties().Select(p => new FastPropertyInfo(p));

      var map = new Dictionary<string, FastPropertyInfo>();
      foreach(var prop in props)
      {
        var dbColumnAttr = prop.GetCustomAttribute<DbColumnAttribute>();
        map.Add(dbColumnAttr?.Alias ?? prop.Name, prop);
      }

      _tableCache.TryAdd(type, map);
      return map;
    }
  }
}
