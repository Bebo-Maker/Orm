using Orm.Reflection;
using System.Linq;

namespace Orm
{
  public static class PropertyAccessor<T>
  {
    public static readonly FastPropertyInfo[] Properties = typeof(T).GetProperties().Select(p => new FastPropertyInfo(p)).ToArray();
  }
}
