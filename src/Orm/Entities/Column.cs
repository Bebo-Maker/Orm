using Orm.Reflection;

namespace Orm.Entities
{
  public class Column
  {
    public string Name { get; }
    public string Alias { get; }
    public GetValueDelegate GetValue { get; }
    public SetValueDelegate SetValue { get; }

    public Column(string name, string alias, GetValueDelegate getValue, SetValueDelegate setValue)
    {
      Name = name;
      Alias = alias;
      GetValue = getValue;
      SetValue = setValue;
    }
  }
}
