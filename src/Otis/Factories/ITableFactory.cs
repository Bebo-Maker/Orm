using Otis.Entities;
using System;

namespace Otis.Factories
{
  internal interface ITableFactory
  {
    public Table GetOrCreateTable<T>();
    public Table GetOrCreateTable(Type type);
  }
}
