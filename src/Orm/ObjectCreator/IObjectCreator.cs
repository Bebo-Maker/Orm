using System;

namespace Orm.ObjectCreator
{
  public interface IObjectCreator
  {
    T Create<T>(Type type);
    T Create<T>(Type type, object[] parameters);
  }
}
