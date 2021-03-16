using Otis.Factories;
using Otis.Querying;
using System;
using System.Threading.Tasks;

namespace Otis
{
  public partial class Database
  {
    public int Delete<T>(Action<IQueryBuilder<T>> action)
    {
      string sql = _builderFactory.CreateDeleteBuilder(action);
      using var conn = _provider.CreateConnection();
      conn.Open();
      using var cmd = DatabaseCommandFactory.CreateCommand(conn, sql);
      return cmd.ExecuteNonQuery();
    }

    public async Task<int> DeleteAsync<T>(Action<IQueryBuilder<T>> action)
    {
      string sql = _builderFactory.CreateDeleteBuilder(action);
      using var conn = _commandFactory.TryCreateAsyncConnection();
      await conn.OpenAsync().ConfigureAwait(false);
      using var cmd = DatabaseCommandFactory.TryCreateAsyncComand(conn, sql);
      return await cmd.ExecuteNonQueryAsync().ConfigureAwait(false);
    }
  }
}
