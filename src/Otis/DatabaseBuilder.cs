using Otis.Configuration;
using Otis.Core;
using System;
using System.Collections.Generic;
using System.Data;

namespace Otis
{
  public class DatabaseBuilder
  {
    private class DatabaseConfig : IDatabaseConfiguration
    {
      public IEnumerable<EntityMap> Mappings { get; }

      public DatabaseConfig(IEnumerable<EntityMap> mappings)
      {
        Mappings = mappings;
      }
    }
    private class DatabaseProvider : IDatabaseProvider
    {
      private readonly Func<IDbConnection> _factory;

      public DatabaseProvider(Func<IDbConnection> resolver)
      {
        _factory = resolver;
      }

      public IDbConnection CreateConnection() => _factory();
    }

    private readonly List<EntityMap> _maps = new();
    private IDatabaseProvider _provider;

    public DatabaseBuilder WithProvider(IDatabaseProvider provider)
    {
      _provider = provider;
      return this;
    }

    public DatabaseBuilder WithConnection(Func<IDbConnection> factory)
    {
      _provider = new DatabaseProvider(factory);
      return this;
    }

    public DatabaseBuilder WithMap(EntityMap map)
    {
      _maps.Add(map);
      return this;
    }

    public DatabaseBuilder WithMaps(IEnumerable<EntityMap> maps)
    {
      _maps.AddRange(maps);
      return this;
    }

    public IDatabase Build() => new Database(_provider, new DatabaseConfig(_maps));
  }
}
