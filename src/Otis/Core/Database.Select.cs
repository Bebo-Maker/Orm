using Otis.Extensions;
using Otis.Querying;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Otis.Core
{
  internal partial class Database
  {
    public T SelectSingle<T>(Action<IQueryBuilder<T>> action = null) => Select(action, 1).FirstOrDefault();

    public T SelectSingleById<T, TIdentifier>(TIdentifier identifier)
    {
      throw new NotImplementedException();
    }

    public T SelectById<T, TIdentifier>(TIdentifier identifier)
    {
      throw new NotImplementedException();
    }

    public List<T> SelectById<T, TIdentifier>(params TIdentifier[] identifier)
    {
      throw new NotImplementedException();
    }

    public List<T> Select<T>(Action<IQueryBuilder<T>> action = null, int top = 0)
    {
      string sql = _builderFactory.CreateSelectBuilder(action, top);
      return ExecuteReader<T>(sql);
    }

    public Task<T> SelectSingleAsync<T>(Action<IQueryBuilder<T>> action = null) => SelectAsync(action, 1).FirstOrDefault();

    public Task<T> SelectSingleByIdAsync<T, TIdentifier>(TIdentifier identifier)
    {
      throw new NotImplementedException();
    }

    public Task<List<T>> SelectByIdAsync<T, TIdentifier>(params TIdentifier[] identifier)
    {
      throw new NotImplementedException();
    }

    public Task<List<T>> SelectAsync<T>(Action<IQueryBuilder<T>> action = null, int top = 0)
    {
      string sql = _builderFactory.CreateSelectBuilder(action, top);
      return ExecuteReaderAsync<T>(sql);
    }

    public List<T> SelectDistinct<T>(Action<IQueryBuilder<T>> action = null, int top = 0)
    {
      string sql = _builderFactory.CreateSelectDistinctBuilder(action, top);
      return ExecuteReader<T>(sql);
    }

    public Task<List<T>> SelectDistinctAsync<T>(Action<IQueryBuilder<T>> action = null, int top = 0)
    {
      string sql = _builderFactory.CreateSelectDistinctBuilder(action, top);
      return ExecuteReaderAsync<T>(sql);
    }
  }
}
