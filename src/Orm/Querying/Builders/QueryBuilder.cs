using Orm.Entities;
using Orm.Expressions;
using Orm.Factories;
using Orm.Utils;
using System;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace Orm.Querying.Builders
{
  public class QueryBuilder<T> : IQueryBuilder<T>
  {
    private readonly ISqlTranslator _translator;
    protected readonly StringBuilder _sb = new StringBuilder();
    private readonly SqlExpressionVisitor _visitor = new SqlExpressionVisitor();
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

    private static string GetColumnNameFromExpression<TProperty>(Expression<Func<T, TProperty>> expression)
    {
      var prop = GetPropertyFromExpression(expression);
      return AliasUtils.GetColumnName(prop);
    }

    private static PropertyInfo GetPropertyFromExpression<TProperty>(Expression<Func<T, TProperty>> expression)
    {
      return (expression.Body as MemberExpression)?.Member is PropertyInfo prop
          ? prop
          : throw new InvalidOperationException("Please provide a valid property expression.");
    }
  }
}
