namespace Orm.Querying.Builders
{
  public class DeleteQueryBuilder<T> : QueryBuilder<T>
  {
    public DeleteQueryBuilder()
    {
      _sb.Append("DELETE");
      AppendFrom();
    }
  }
}
