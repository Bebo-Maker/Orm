using System;

namespace Orm.ObjectCreator
{
  public class ActivatorObjectCreator : IObjectCreator
  {
    public T Create<T>(Type type) => (T)Activator.CreateInstance(type);

    public T Create<T>(Type type, object[] parameters) => (T)Activator.CreateInstance(type, parameters);
  }
}
