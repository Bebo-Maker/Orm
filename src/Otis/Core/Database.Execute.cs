using Otis.Factories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Otis.Core
{
  internal partial class Database
  {
    public List<T> ExecuteReader<T>(string sql)
    {
      using var conn = _provider.CreateConnection();
      conn.Open();
      using var cmd = DatabaseCommandFactory.CreateCommand(conn, sql);
      using var reader = cmd.ExecuteReader();
      return _engine.CreateObjects<T>(reader);
    }

    public async Task<List<T>> ExecuteReaderAsync<T>(string sql)
    {
      using var conn = _commandFactory.TryCreateAsyncConnection();
      await conn.OpenAsync().ConfigureAwait(false);
      using var cmd = DatabaseCommandFactory.TryCreateAsyncComand(conn, sql);
      using var reader = await cmd.ExecuteReaderAsync().ConfigureAwait(false);
      return await _engine.CreateObjectsAsync<T>(reader).ConfigureAwait(false);
    }
  }
}
