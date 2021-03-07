using System;
using System.Linq.Expressions;

namespace Orm.Querying
{
  public interface IQueryBuilder<T>
  {
    IQueryBuilder<T> Where(Expression<Func<T, bool>> predicate);
    IQueryBuilder<T> OrderBy<TProperty>(Expression<Func<T, TProperty>> expression);
    IQueryBuilder<T> OrderByDescending<TProperty>(Expression<Func<T, TProperty>> expression);
  }
}
