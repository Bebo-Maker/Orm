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

    T SelectById<T, TIdentifier>(TIdentifier identifier);
    List<T> SelectById<T, TIdentifier>(params TIdentifier[] identifier);
    List<T> Select<T>(Action<IQueryBuilder<T>> action = null);

    Task<T> SelectByIdAsync<T, TIdentifier>(TIdentifier identifier);
    Task<List<T>> SelectByIdAsync<T, TIdentifier>(params TIdentifier[] identifier);
    Task<List<T>> SelectAsync<T>(Action<IQueryBuilder<T>> action = null);

    List<T> SelectDistinct<T>(Action<IQueryBuilder<T>> action = null);
    Task<List<T>> SelectDistinctAsync<T>(Action<IQueryBuilder<T>> action = null);

    int Insert<T>(T entity, Action<IQueryBuilder<T>> action = null);
    Task<int> InsertAsync<T>(T entity, Action<IQueryBuilder<T>> action = null);

    int Update<T>(T entity, Action<IQueryBuilder<T>> action = null);
    Task<int> UpdateAsync<T>(T entity, Action<IQueryBuilder<T>> action = null);

    int Delete<T>(Action<IQueryBuilder<T>> action = null);
    Task<int> DeleteAsync<T>(Action<IQueryBuilder<T>> action = null);
  }
}
