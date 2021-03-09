using Orm.Factories;
using Orm.Querying;
using System;
using System.Data;
using System.Threading.Tasks;

namespace Orm
{
  public static partial class IDbConnectionExtensions
  {
    public static int Delete<T>(this IDbConnection conn, Action<IQueryBuilder<T>> action)
    {
      string sql = BuilderFactory.CreateDeleteBuilder(action);
      using var cmd = DbFactory.CreateCommand(conn, sql);
      return cmd.ExecuteNonQuery();
    }

    public static Task<int> DeleteAsync<T>(this IDbConnection conn, Action<IQueryBuilder<T>> action)
    {
      string sql = BuilderFactory.CreateDeleteBuilder(action);
      using var cmd = DbFactory.TryCreateAsyncComand(conn, sql);
      return cmd.ExecuteNonQueryAsync();
    }
  }
}
