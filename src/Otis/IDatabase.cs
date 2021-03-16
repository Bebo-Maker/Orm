using Otis.Querying;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Otis
{
  public interface IDatabase
  {
    List<T> ExecuteReader<T>(string sql);
    Task<List<T>> ExecuteReaderAsync<T>(string sql);

    T SelectSingle<T>(Action<IQueryBuilder<T>> action = null);
    T SelectSingleById<T, TIdentifier>(TIdentifier identifier);
    List<T> SelectById<T, TIdentifier>(params TIdentifier[] identifier);
    List<T> Select<T>(Action<IQueryBuilder<T>> action = null, int top = 0);

    Task<T> SelectSingleAsync<T>(Action<IQueryBuilder<T>> action = null);
    Task<T> SelectSingleByIdAsync<T, TIdentifier>(TIdentifier identifier);
    Task<List<T>> SelectByIdAsync<T, TIdentifier>(params TIdentifier[] identifier);
    Task<List<T>> SelectAsync<T>(Action<IQueryBuilder<T>> action = null, int top = 0);

    List<T> SelectDistinct<T>(Action<IQueryBuilder<T>> action = null, int top = 0);
    Task<List<T>> SelectDistinctAsync<T>(Action<IQueryBuilder<T>> action = null, int top = 0);

    int Insert<T>(T entity, Action<IQueryBuilder<T>> action = null);
    Task<int> InsertAsync<T>(T entity, Action<IQueryBuilder<T>> action = null);

    int Update<T>(T entity, Action<IQueryBuilder<T>> action = null);
    Task<int> UpdateAsync<T>(T entity, Action<IQueryBuilder<T>> action = null);

    int Delete<T>(Action<IQueryBuilder<T>> action = null);
    Task<int> DeleteAsync<T>(Action<IQueryBuilder<T>> action = null);
  }
}
