using System.Data;

namespace Orm
{
  public interface IDatabaseProvider
  {
    IDbConnection CreateConnection();
  }
}
