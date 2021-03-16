using Orm.Factories;
using Orm.Querying;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Orm
{
  public partial class Database
  {
    public List<T> Select<T>(Action<IQueryBuilder<T>> action = null)
    {
      string sql = _builderFactory.CreateSelectBuilder(action);
      return Select<T>(sql);
    }

    public List<T> Select<T>(string sqlStatement)
    {
      using var conn = _provider.CreateConnection();
      conn.Open();
      using var cmd = DatabaseCommandFactory.CreateCommand(conn, sqlStatement);
      using var reader = cmd.ExecuteReader();
      return _engine.CreateObjects<T>(reader);
    }

    public Task<List<T>> SelectAsync<T>(Action<IQueryBuilder<T>> action = null)
    {
      string sql = _builderFactory.CreateSelectBuilder(action);
      return SelectAsync<T>(sql);
    }

    public async Task<List<T>> SelectAsync<T>(string sqlStatement)
    {
      using var conn = _commandFactory.TryCreateAsyncConnection();
      await conn.OpenAsync().ConfigureAwait(false);
      using var cmd = DatabaseCommandFactory.TryCreateAsyncComand(conn, sqlStatement);
      using var reader = await cmd.ExecuteReaderAsync().ConfigureAwait(false);
      return await _engine.CreateObjectsAsync<T>(reader).ConfigureAwait(false);
    }

    public List<T> SelectDistinct<T>(Action<IQueryBuilder<T>> action = null)
    {
      string sql = _builderFactory.CreateSelectDistinctBuilder(action);
      return Select<T>(sql);
    }

    public Task<List<T>> SelectDistinctAsync<T>(Action<IQueryBuilder<T>> action = null)
    {
      string sql = _builderFactory.CreateSelectDistinctBuilder(action);
      return SelectAsync<T>(sql);
    }
  }
}
