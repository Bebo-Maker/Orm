namespace Orm.Querying.Builders
{
  public class SelectQueryBuilder<T> : QueryBuilder<T>
  {
    public SelectQueryBuilder(ISqlTranslator translator) : base(translator)
    { 
      _sb.Append("SELECT ");

      var columns = Table.Columns;
      for (int i = 0; i < columns.Length - 1; i++)
        _sb.Append(columns[i].Alias).Append(", ");

      _sb.Append(columns[^1].Alias);

      AppendFrom();
    }
  }
}
