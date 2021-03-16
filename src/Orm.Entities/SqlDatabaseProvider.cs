using System.Data;
using System.Data.SqlClient;

namespace Orm.Entities
{
  public static class Connection
  {
    public static IDatabaseProvider Provider { get; } = new SqlDatabaseProvider();
  }

  public class SqlDatabaseProvider : IDatabaseProvider
  {
    public IDbConnection CreateConnection()
    {
      return new SqlConnection(@"Server=localhost\SQLEXPRESS;Database=TestDB;Trusted_Connection=True;");
    }
  }
}
