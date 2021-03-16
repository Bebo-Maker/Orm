using Orm.Utils;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Orm.Configuration
{
  public abstract class EntityMap
  {
    internal List<ColumnMap> Columns { get; } = new();

    internal abstract Type EntityType { get; }

    public string Name { get; private set; }

    protected void Alias(string name)
    {
      Name = name;
    }
  }

  public class EntityMap<TEntity> : EntityMap
  {
    internal override Type EntityType { get; } = typeof(TEntity);

    public ColumnMap<TEntity> Map<TProperty>(Expression<Func<TEntity, TProperty>> expression)
      => CreateMap(expression);

    private ColumnMap<TEntity> CreateMap<TProperty>(Expression<Func<TEntity, TProperty>> expression)
    {
      var prop = ExpressionUtil.GetProperty(expression);
      var map = new ColumnMap<TEntity>(prop);
      Columns.Add(map);
      return map;
    }
  }
}