using Otis.Configuration;
using System.Collections.Generic;

namespace Otis
{
  public interface IDatabaseConfiguration
  {
    IReadOnlyList<EntityMap> Mappings { get; }
  }
}
