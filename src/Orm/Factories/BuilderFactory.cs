using Orm.Querying;
using Orm.Querying.Builders;
using System;

namespace Orm.Factories
{
  internal class BuilderFactory
  {
    public static string CreateSelectBuilder<T>(Action<IQueryBuilder<T>> action) => BuildBuilder(action, new SelectQueryBuilder<T>());
    public static string CreateSelectDistinctBuilder<T>(Action<IQueryBuilder<T>> action) => BuildBuilder(action, new SelectDistinctQueryBuilder<T>());
    public static string CreateDeleteBuilder<T>(Action<IQueryBuilder<T>> action) => BuildBuilder(action, new DeleteQueryBuilder<T>());
    public static string CreateInsertBuilder<T>(Action<IQueryBuilder<T>> action) => BuildBuilder(action, new InsertQueryBuilder<T>());

    private static string BuildBuilder<T>(Action<IQueryBuilder<T>> action, QueryBuilder<T> builder)
    {
      action?.Invoke(builder);
      return builder.Build();
    }
  }
}
