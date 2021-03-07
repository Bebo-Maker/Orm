using Orm.Factories;
using Orm.Querying;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Orm
{
  public static partial class IDbConnectionQueryExtensions
  {
    public static List<T> Query<T>(this IDbConnection conn, Action<IQueryBuilder<T>> action = null)
    {
      string sql = BuilderFactory.CreateSelectBuilder<T>(action);
      return Query<T>(conn, sql);
    }

    public static List<T> QueryDistinct<T>(this IDbConnection conn, Action<IQueryBuilder<T>> action = null)
    {
      string sql = BuilderFactory.CreateSelectDistinctBuilder<T>(action);
      return Query<T>(conn, sql);
    }

    public static List<T> Query<T>(this IDbConnection conn, string sqlStatement)
    {
      using var cmd = DbFactory.CreateCommand(conn, sqlStatement);
      var reader = cmd.ExecuteReader();
      return Engine.CreateObjects<T>(reader);
    }

    public static Task<List<T>> QueryAsync<T>(this IDbConnection conn, Action<IQueryBuilder<T>> action = null)
    {
      string sql = BuilderFactory.CreateSelectBuilder<T>(action);
      return QueryAsync<T>(conn, sql);
    }

    public static Task<List<T>> QueryDistinctAsync<T>(this IDbConnection conn, Action<IQueryBuilder<T>> action = null)
    {
      string sql = BuilderFactory.CreateSelectDistinctBuilder<T>(action);
      return QueryAsync<T>(conn, sql);
    }

    public static async Task<List<T>> QueryAsync<T>(this IDbConnection conn, string sqlStatement)
    {
      var cmd = DbFactory.TryCreateAsyncComand(conn, sqlStatement);
      var reader = await cmd.ExecuteReaderAsync().ConfigureAwait(false);
      return await Engine.CreateObjectsAsync<T>(reader);
    }
  }
}
