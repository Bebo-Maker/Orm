using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Otis.Utils
{
  public static class ExpressionUtil
  {
    public static PropertyInfo GetProperty<T, TProperty>(Expression<Func<T, TProperty>> expression)
    {
      return (expression.Body as MemberExpression)?.Member is PropertyInfo prop
        ? prop
        : throw new InvalidOperationException("Please provide a valid property expression.");
    }
  }
}
