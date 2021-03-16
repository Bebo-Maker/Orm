using Otis.Factories;
using Otis.Utils;

namespace Otis.Querying.Builders
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
