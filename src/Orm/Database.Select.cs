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
      return ExecuteReader<T>(sql);
    }

    public Task<List<T>> SelectAsync<T>(Action<IQueryBuilder<T>> action = null)
    {
      string sql = _builderFactory.CreateSelectBuilder(action);
      return ExecuteReaderAsync<T>(sql);
    }

    public List<T> SelectDistinct<T>(Action<IQueryBuilder<T>> action = null)
    {
      string sql = _builderFactory.CreateSelectDistinctBuilder(action);
      return ExecuteReader<T>(sql);
    }

    public Task<List<T>> SelectDistinctAsync<T>(Action<IQueryBuilder<T>> action = null)
    {
      string sql = _builderFactory.CreateSelectDistinctBuilder(action);
      return ExecuteReaderAsync<T>(sql);
    }
  }
}
