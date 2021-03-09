using Orm.Factories;
using Orm.Querying;
using System;
using System.Data;
using System.Threading.Tasks;

namespace Orm
{
  public static partial class IDbConnectionExtensions
  {
    public static int Insert<T>(this IDbConnection conn, T entity, Action<IQueryBuilder<T>> action = null)
    {
      var sql = BuilderFactory.CreateInsertBuilder(action);
      using var cmd = DbFactory.CreateCommandWithParameters(conn, sql, entity);
      return cmd.ExecuteNonQuery();
    }

    public static Task<int> InsertAsync<T>(this IDbConnection conn, T entity, Action<IQueryBuilder<T>> action = null)
    {
      var sql = BuilderFactory.CreateInsertBuilder(action);
      using var cmd = DbFactory.TryCreateAsyncCommandWithParameters(conn, sql, entity);
      return cmd.ExecuteNonQueryAsync();
    }
  }
}
