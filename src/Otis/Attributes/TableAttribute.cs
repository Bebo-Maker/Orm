using System;

namespace Otis.Attributes
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
