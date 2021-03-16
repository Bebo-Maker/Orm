using Otis.Factories;
using System;
using System.Linq.Expressions;

namespace Otis.Querying.Builders
{
  internal class SelectDistinctQueryBuilder<T> : SelectQueryBuilder<T>
  {
    protected override string Keyword { get; } = "SELECT DISTINCT";

    public SelectDistinctQueryBuilder(ITableFactory factory, int elementsToSelect = 0, Expression<Func<T, object>>[] selectColumns = null) 
      : base(factory, elementsToSelect, selectColumns) { }
  }
}
