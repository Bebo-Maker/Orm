using Orm.Factories;
using Orm.Utils;

namespace Orm.Querying.Builders
{
  internal class UpdateQueryBuilder<T> : QueryBuilder<T>
  {
    public UpdateQueryBuilder(ITableFactory factory) : base(factory)
    {
      _sb.Append("UPDATE ")
         .Append(Table.Name)
         .Append(" SET ")
         .Append(SqlUtils.BuildUpdateParameters(Table));
    }
  }
}
