using Orm.Querying;
using Orm.Querying.Builders;
using System;

namespace Orm.Factories
{
  internal class BuilderFactory
  {
    private readonly ITableFactory _factory;

    public BuilderFactory(ITableFactory factory)
    {
      _factory = factory;
    }

    public string CreateSelectBuilder<T>(Action<IQueryBuilder<T>> action) => BuildBuilder(action, new SelectQueryBuilder<T>(_factory));
    public string CreateSelectDistinctBuilder<T>(Action<IQueryBuilder<T>> action) => BuildBuilder(action, new SelectDistinctQueryBuilder<T>(_factory));
    public string CreateDeleteBuilder<T>(Action<IQueryBuilder<T>> action) => BuildBuilder(action, new DeleteQueryBuilder<T>(_factory));
    public string CreateInsertBuilder<T>(Action<IQueryBuilder<T>> action) => BuildBuilder(action, new InsertQueryBuilder<T>(_factory));
    public string CreateUpdateBuilder<T>(Action<IQueryBuilder<T>> action) => BuildBuilder(action, new UpdateQueryBuilder<T>(_factory));

    private static string BuildBuilder<T>(Action<IQueryBuilder<T>> action, QueryBuilder<T> builder)
    {
      action?.Invoke(builder);
      return builder.Build();
    }
  }
}
