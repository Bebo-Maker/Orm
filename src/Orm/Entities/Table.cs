using System.Linq;

namespace Orm.Entities
{
  public class Table
  {
    public string Name { get; }
    public Column[] Columns { get; }

    public Table(string name, Column[] columns)
    {
      Name = name;
      Columns = columns;
    }

    public void SetValueOfColumn(string name, object instance, object value) 
      => Columns.FirstOrDefault(c => c.Alias == name)?.SetValue(instance, value);
  }
}
