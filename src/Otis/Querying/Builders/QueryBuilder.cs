using Otis.Entities;
using Otis.Expressions;
using Otis.Factories;
using Otis.Utils;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Otis.Querying.Builders
{
  internal class QueryBuilder<T> : IQueryBuilder<T>
  {
    protected readonly StringBuilder _sb = new();
    private readonly SqlExpressionVisitor _visitor = new();
    protected readonly Table Table;

    public QueryBuilder(ITableFactory factory)
    {
      Table = factory.GetOrCreateTable<T>();
    }

    public IQueryBuilder<T> Where(Expression<Func<T, bool>> predicate)
    {
      string condition = _visitor.Translate(predicate);

      _sb.Append(" WHERE ").Append(condition);

      return this;
    }

    public IQueryBuilder<T> OrderBy<TProperty>(Expression<Func<T, TProperty>> expression)
    {
      string columnName = GetColumnNameFromExpression(expression);

      _sb.Append(" ORDER BY ").Append(columnName).Append(" ASC");

      return this;
    }

    public IQueryBuilder<T> OrderByDescending<TProperty>(Expression<Func<T, TProperty>> expression)
    {
      string columnName = GetColumnNameFromExpression(expression);

      _sb.Append("ORDER BY ").Append(columnName).Append(" DESC");

      return this;
    }

    public string Build() => _sb.ToString();

    protected IQueryBuilder<T> AppendFrom()
    {
      _sb.Append(" FROM ").Append(Table.Name);

      return this;
    }

    protected string GetColumnNameFromExpression<TProperty>(Expression<Func<T, TProperty>> expression)
    {
      var propName = ExpressionUtil.GetProperty(expression).Name;
      return Table.Columns.FirstOrDefault(c => c.Name == propName).Alias;
    }
  }
}
