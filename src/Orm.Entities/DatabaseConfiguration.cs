using Orm.Configuration;
using System.Collections.Generic;
using System.Linq;

namespace Orm.Entities
{
  public class DatabaseConfiguration : IDatabaseConfiguration
  {
    public IReadOnlyList<EntityMap> Mappings { get; }

    public DatabaseConfiguration(params EntityMap[] maps)
    {
      Mappings = maps.ToList().AsReadOnly();
    }
  }
}
