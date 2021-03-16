﻿using Orm.Factories;
using Orm.Utils;
using System.Linq;

namespace Orm.Querying.Builders
{
  internal class InsertQueryBuilder<T> : QueryBuilder<T>
  {
    public InsertQueryBuilder(ITableFactory factory) : base(factory)
    {
      _sb.Append("INSERT INTO ")
         .Append(Table.Name)
         .Append(" (")
         .Append(string.Join(", ", Table.Columns.Select(a => a.Alias)))
         .Append(") VALUES (")
         .Append(SqlUtils.BuildParameters(Table))
         .Append(')');
    }
  }
}
