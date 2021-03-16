using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;

namespace Otis.Core
{
  public interface IEngine
  {
    List<T> CreateObjects<T>(IDataReader reader);
    Task<List<T>> CreateObjectsAsync<T>(DbDataReader reader);
  }
}
