using System;

namespace Otis.Attributes
{
  [AttributeUsage(AttributeTargets.Property)]
  public class ColumnAttribute : Attribute
  {
    public string Alias { get; }

    public ColumnAttribute(string alias)
    {
      Alias = alias;
    }
  }
}
