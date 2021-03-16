using Orm.Entities;
using System;

namespace Orm.Factories
{
  internal interface ITableFactory
  {
    public Table GetOrCreateTable<T>();
    public Table GetOrCreateTable(Type type);
  }
}
