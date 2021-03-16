using System.Data;
using System.Data.SqlClient;

namespace Otis.Entities
{
  public static class Infrastructure
  {
    public static IDatabase Database { get; } = new DatabaseBuilder().WithConnection(GetConnection)
                                                                     .Build();

    public static IDbConnection GetConnection() => new SqlConnection(@"Server=localhost\SQLEXPRESS;Database=TestDB;Trusted_Connection=True;");
  }
}
