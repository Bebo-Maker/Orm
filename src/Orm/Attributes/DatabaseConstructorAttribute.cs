using System;

namespace Orm.Attributes
{
  [AttributeUsage(AttributeTargets.Constructor)]
  public sealed class DatabaseConstructorAttribute : Attribute
  {
  }
}
