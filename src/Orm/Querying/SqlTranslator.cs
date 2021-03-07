namespace Orm.Querying
{
  /// <summary>
  /// TODO define this from other assemblies to support other sql dialects
  /// </summary>
  public class SqlTranslator : ISqlTranslator
  {
    public string Where(string condition) => $"WHERE {condition} ";
    public string OrderBy(string columnName) => $"ORDER BY {columnName} ASC ";
    public string OrderByDescending(string columnName) => $"ORDER BY {columnName} DESC ";

    public string AppendColumn(string columnName) => $"[{columnName}], ";
  }
}
