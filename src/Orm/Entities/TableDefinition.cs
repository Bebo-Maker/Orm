namespace Orm.Entities
{
  public class TableDefinition
  {
    public string Name { get; }

    public ColumnDefinition[] Columns { get; }

    public TableDefinition(string name, ColumnDefinition[] columns)
    {
      Name = name;
      Columns = columns;
    }
  }
}
