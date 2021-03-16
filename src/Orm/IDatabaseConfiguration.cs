using Orm.Configuration;
using System.Collections.Generic;

namespace Orm
{
  public interface IDatabaseConfiguration
  {
    IReadOnlyList<EntityMap> Mappings { get; }
  }
}
