using Orm.Entities;
using Orm.Expressions;
using Orm.Factories;
using Orm.Utils;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace Orm.Querying.Builders
{
  public class QueryBuilder<T> : IQueryBuilder<T>
  {
    private readonly ISqlTranslator _translator;
    protected readonly StringBuilder _sb = new();
    private readonly SqlExpressionVisitor _visitor = new();
    protected readonly Table Table;

    public QueryBuilder(ISqlTranslator translator)
    {
      _translator = translator;
      Table = TableFactory.GetOrCreateTableDefinition(typeof(T));
    }

    public IQueryBuilder<T> Where(Expression<Func<T, bool>> predicate)
    {
      string condition = _visitor.Translate(predicate);

      _sb.Append(' ').Append(_translator.Where(condition));

      return this;
    }

    public IQueryBuilder<T> OrderBy<TProperty>(Expression<Func<T, TProperty>> expression)
    {
      string columnName = GetColumnNameFromExpression(expression);

      _sb.Append(' ').Append(_translator.OrderBy(columnName));

      return this;
    }

    public IQueryBuilder<T> OrderByDescending<TProperty>(Expression<Func<T, TProperty>> expression)
    {
      string columnName = GetColumnNameFromExpression(expression);

      _sb.Append(' ').Append(_translator.OrderByDescending(columnName));

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
      var propName = GetPropertyFromExpression(expression).Name;
      return Table.Columns.FirstOrDefault(c => c.Name == propName).Alias;
    }

    protected static PropertyInfo GetPropertyFromExpression<TProperty>(Expression<Func<T, TProperty>> expression)
    {
      return (expression.Body as MemberExpression)?.Member is PropertyInfo prop
          ? prop
          : throw new InvalidOperationException("Please provide a valid property expression.");
    }
  }
}
