using Otis.Attributes;
using Otis.Configuration;
using Otis.Entities;
using Otis.Reflection;
using Otis.Utils;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Otis.Factories
{
  public class TableFactory : ITableFactory
  {
    private static readonly ConcurrentDictionary<Type, Table> _tableCache = new();
    private static readonly DelegateFactory _delegateFactory = new();

    private readonly Dictionary<Type, EntityMap> _mappingLookup = new();

    public TableFactory(IDatabaseConfiguration configuration)
    {
      CreateMapLookup(configuration?.Mappings);
    }

    private void CreateMapLookup(IEnumerable<EntityMap> mappings)
    {
      if(mappings != null)
        foreach (var map in mappings)
          _mappingLookup.Add(map.EntityType, map);
    }


    public Table GetOrCreateTable<T>() => GetOrCreateTable(typeof(T));
    public Table GetOrCreateTable(Type type)
      => _tableCache.TryGetValue(type, out var table)
        ? table
        : CreateTable(type);

    private Table CreateTable(Type type)
    {
      Table table;
      if (_mappingLookup.TryGetValue(type, out var map))
      {
        var cols = CreateColumns(map.Columns);
        table = new Table(map.Name, cols);
      }
      else
      {
        var props = GetProperties(type);
        var cols = CreateColumns(props);
        table = new Table(AliasUtils.GetTableName(type), cols);
      }

      _tableCache.TryAdd(type, table);
      return table;
    }

    private static Column[] CreateColumns(IEnumerable<ColumnMap> columns)
      => columns.Select(c => new Column(c.IsPrimary,
                                        c.Property.Name,
                                        c.Alias ?? c.Property.Name,
                                        _delegateFactory.CreatePropertyGetter(c.Property),
                                        _delegateFactory.CreatePropertySetter(c.Property))).ToArray();

    private static PropertyInfo[] GetProperties(Type type)
      => type.GetProperties().Where(p => p.GetCustomAttribute<IgnoreAttribute>() == null).ToArray();

    private static Column[] CreateColumns(IEnumerable<PropertyInfo> props)
      => props.Select(p => new Column(p.GetCustomAttribute<PrimaryKeyAttribute>() != null,
                                      p.Name,
                                      AliasUtils.GetColumnName(p),
                                      _delegateFactory.CreatePropertyGetter(p),
                                      _delegateFactory.CreatePropertySetter(p))).ToArray();
  }
}
