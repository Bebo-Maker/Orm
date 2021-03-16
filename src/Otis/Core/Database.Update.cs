using Otis.Querying;
using System;
using System.Threading.Tasks;

namespace Otis.Core
{
  internal partial class Database
  {
    public int Update<T>(T entity, Action<IQueryBuilder<T>> action = null)
    {
      var sql = _builderFactory.CreateUpdateBuilder(action);
      using var conn = _provider.CreateConnection();
      conn.Open();
      using var cmd = _commandFactory.CreateCommandWithParameters(conn, sql, entity);
      return cmd.ExecuteNonQuery();
    }

    public async Task<int> UpdateAsync<T>(T entity, Action<IQueryBuilder<T>> action = null)
    {
      var sql = _builderFactory.CreateUpdateBuilder(action);
      using var conn = _commandFactory.TryCreateAsyncConnection();
      await conn.OpenAsync().ConfigureAwait(false);
      using var cmd = _commandFactory.TryCreateAsyncCommandWithParameters(conn, sql, entity);
      return await cmd.ExecuteNonQueryAsync().ConfigureAwait(false);
    }
  }
}
