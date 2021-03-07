using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Orm.Expressions
{
  public class SqlExpressionVisitor : ExpressionVisitor
  {
    private static readonly IReadOnlyDictionary<ExpressionType, string> _expressionTypeToSqlOperands = new ConcurrentDictionary<ExpressionType, string>
    {
      [ExpressionType.Not] = "NOT",
      [ExpressionType.AndAlso] = "AND",
      [ExpressionType.OrElse] = "OR",
      [ExpressionType.Equal] = "=",
      [ExpressionType.NotEqual] = "<>",
      [ExpressionType.GreaterThan] = ">",
      [ExpressionType.GreaterThanOrEqual] = ">=",
      [ExpressionType.LessThan] = "<",
      [ExpressionType.LessThanOrEqual] = "<="
    };

    private readonly StringBuilder _sb = new StringBuilder();
    private readonly Stack<string> _fieldNames = new Stack<string>();

    public string Translate(LambdaExpression expression)
    {
      Visit(expression.Body);
      return _sb.ToString();
    }

    protected override Expression VisitUnary(UnaryExpression node)
    {
      if (node.NodeType != ExpressionType.Not)
        throw new NotSupportedException("Only not \"!\" unary operator is supported!");

      _sb.Append(_expressionTypeToSqlOperands[ExpressionType.Not]);

      AppendOpeningBracket();
      Visit(node.Operand);
      AppendClosingBracket();

      return node;
    }

    protected override Expression VisitBinary(BinaryExpression node)
    {
      AppendOpeningBracket();
      Visit(node.Left);
      _sb.Append(_expressionTypeToSqlOperands[node.NodeType]);
      Visit(node.Right);
      AppendClosingBracket();

      return node;
    }

    protected override Expression VisitMember(MemberExpression node)
    {
      if (node.Expression.NodeType is ExpressionType.Constant or ExpressionType.MemberAccess)
      {
        _fieldNames.Push(node.Member.Name);
        Visit(node.Expression);
      }
      else
        _sb.Append(node.Member.Name);

      return node;
    }

    protected override Expression VisitConstant(ConstantExpression node)
    {
      _sb.Append(GetValue(node.Value));
      return node;
    }


    private void AppendOpeningBracket() => _sb.Append('(');
    private void AppendClosingBracket() => _sb.Append(')');

    private string GetValue(object input)
    {
      var type = input.GetType();
      if (type.IsClass && type != typeof(string))
      {
        string fieldName = _fieldNames.Pop();
        var fieldInfo = type.GetField(fieldName);
        object value = fieldInfo != null ? fieldInfo.GetValue(input) : type.GetProperty(fieldName).GetValue(input);
        return GetValue(value);
      }

      return input.ToString();
    }
  }
}
