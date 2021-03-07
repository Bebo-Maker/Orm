using System;
using System.Linq.Expressions;

namespace Orm.Querying.Builders
{
  public class SelectDistinctQueryBuilder<T> : SelectQueryBuilder<T>
  {
    protected override string Keyword { get; } = "SELECT DISTNCT";

    public SelectDistinctQueryBuilder(ISqlTranslator translator, Expression<Func<T, object>>[] selectColumns = null) : base(translator, selectColumns) { }
  }
}
