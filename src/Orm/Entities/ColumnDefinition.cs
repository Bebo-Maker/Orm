namespace Orm.Entities
{
  public class ColumnDefinition
  {
    public string Name { get; }

    public ColumnDefinition(string name)
    {
      Name = name;
    }
  }
}