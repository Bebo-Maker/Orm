using Orm.Reflection;

namespace Orm.Entities
{
  public class Column
  {
    public string Name { get; }
    public string Alias { get; }
    public SetValueDelegate SetValue { get; }

    public Column(string name, string alias, SetValueDelegate setValue)
    {
      Name = name;
      Alias = alias;
      SetValue = setValue;
    }
  }
}
