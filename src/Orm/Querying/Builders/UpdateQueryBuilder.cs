using Orm.Utils;

namespace Orm.Querying.Builders
{
  public class UpdateQueryBuilder<T> : QueryBuilder<T>
  {
    public UpdateQueryBuilder()
    {
      _sb.Append("UPDATE ")
         .Append(Table.Name)
         .Append(" SET ")
         .Append(SqlUtils.BuildUpdateParameters(Table));
    }
  }
}
