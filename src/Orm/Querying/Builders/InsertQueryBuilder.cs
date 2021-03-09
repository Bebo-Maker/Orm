using Orm.Utils;
using System.Linq;

namespace Orm.Querying.Builders
{
  public class InsertQueryBuilder<T> : QueryBuilder<T>
  {
    public InsertQueryBuilder()
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
