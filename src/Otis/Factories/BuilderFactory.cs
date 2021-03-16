using Otis.Querying;
using Otis.Querying.Builders;
using System;

namespace Otis.Factories
{
  internal class BuilderFactory
  {
    private readonly ITableFactory _factory;

    public BuilderFactory(ITableFactory factory)
    {
      _factory = factory;
    }

    public string CreateSelectBuilder<T>(Action<IQueryBuilder<T>> action, int elementsToSelect = 0) 
      => BuildBuilder(action, new SelectQueryBuilder<T>(_factory, elementsToSelect));

    public string CreateSelectDistinctBuilder<T>(Action<IQueryBuilder<T>> action, int elementsToSelect = 0) 
      => BuildBuilder(action, new SelectDistinctQueryBuilder<T>(_factory, elementsToSelect));

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
