using Otis.Querying;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Otis.Core
{
  internal partial class Database
  {
    public T SelectById<T, TIdentifier>(TIdentifier identifier)
    {
      throw new NotImplementedException();
    }

    public List<T> SelectById<T, TIdentifier>(params TIdentifier[] identifier)
    {
      throw new NotImplementedException();
    }

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

    public Task<T> SelectByIdAsync<T, TIdentifier>(TIdentifier identifier)
    {
      throw new NotImplementedException();
    }

    public Task<List<T>> SelectByIdAsync<T, TIdentifier>(params TIdentifier[] identifier)
    {
      throw new NotImplementedException();
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
