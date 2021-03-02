using System;

namespace Orm.Attributes
{
  [AttributeUsage(AttributeTargets.Class)]
  public class TableAttribute : Attribute
  {
    public string Alias { get; }

    public TableAttribute(string alias)
    {
      Alias = alias;
    }
  }
}
