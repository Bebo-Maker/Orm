using System;
using System.Linq;
using System.Linq.Expressions;

namespace Orm.Querying.Builders
{
  public class SelectQueryBuilder<T> : QueryBuilder<T>
  {
    protected virtual string Keyword { get; } = "SELECT";


    public SelectQueryBuilder(Expression<Func<T, object>>[] selectColumns = null)
    {
      _sb.Append("SELECT DISTINCT ");

      var columns = selectColumns?.Select(e => GetColumnNameFromExpression(e)).ToArray();
      if (selectColumns == null)
        columns = Table.Columns.Select(a => a.Alias).ToArray();

      for (int i = 0; i < columns.Length - 1; i++)
        _sb.Append(columns[i]).Append(", ");

      _sb.Append(columns[^1]);

      AppendFrom();
    }
  }
}
