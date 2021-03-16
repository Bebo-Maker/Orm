using Otis.Configuration;
using System.Collections.Generic;

namespace Otis
{
  public interface IDatabaseConfiguration
  {
    IEnumerable<EntityMap> Mappings { get; }
  }
}
