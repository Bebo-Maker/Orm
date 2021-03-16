using System.Reflection;

namespace Orm.Configuration
{
  public class ColumnMap
  {
    public bool IsPrimary { get; protected set; } = false;
    public PropertyInfo Property { get; protected set; }
    public string Alias { get; protected set; }

    internal ColumnMap(PropertyInfo property)
    {
      Property = property;
    }
  }

  public sealed class ColumnMap<TEntity> : ColumnMap
  {
    internal ColumnMap(PropertyInfo property) : base(property) { }

    public ColumnMap<TEntity> WithAlias(string alias)
    {
      Alias = alias;
      return this;
    }

    public ColumnMap<TEntity> AsPrimaryKey()
    {
      IsPrimary = true;
      return this;
    }
  }
}
