using Otis.Factories;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace Otis.Querying.Builders
{
  internal class SelectQueryBuilder<T> : QueryBuilder<T>
  {
    protected virtual string Keyword { get; } = "SELECT";

    public SelectQueryBuilder(ITableFactory factory, Expression<Func<T, object>>[] selectColumns = null) : base(factory)
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
