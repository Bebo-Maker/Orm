using Otis.Factories;

namespace Otis.Querying.Builders
{
  internal class DeleteQueryBuilder<T> : QueryBuilder<T>
  {
    public DeleteQueryBuilder(ITableFactory factory) : base(factory)
    {
      _sb.Append("DELETE");
      AppendFrom();
    }
  }
}
