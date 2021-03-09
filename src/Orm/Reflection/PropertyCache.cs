using System.Reflection;

namespace Orm.Reflection
{
  public static class PropertyCache<T>
  {
    public static readonly PropertyInfo[] Properties = typeof(T).GetProperties();
  }
}
