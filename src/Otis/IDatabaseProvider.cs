using System.Data;

namespace Otis
{
  public interface IDatabaseProvider
  {
    IDbConnection CreateConnection();
  }
}
