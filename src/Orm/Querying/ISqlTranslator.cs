namespace Orm.Querying
{
  public interface ISqlTranslator
  {
    string Where(string condition);
    string OrderBy(string columnName);
    string OrderByDescending(string columnName);
    string AppendColumn(string column);
  }
}
