using System;

namespace Orm.Attributes
{
  [AttributeUsage(AttributeTargets.Property)]
  public class DbColumnAttribute : Attribute
  {
    public string Alias { get; }

    public DbColumnAttribute(string alias)
    {
      Alias = alias;
    }
  }
}
