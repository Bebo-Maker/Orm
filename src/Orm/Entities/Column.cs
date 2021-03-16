using Orm.Reflection;

namespace Orm.Entities
{
  public class Column
  {
    public bool IsPrimaryKey { get; }
    public string Name { get; }
    public string Alias { get; }
    public GetValueDelegate GetValue { get; }
    public SetValueDelegate SetValue { get; }

    public Column(bool isPrimaryKey, string name, string alias, GetValueDelegate getValue, SetValueDelegate setValue)
    {
      IsPrimaryKey = isPrimaryKey;
      Name = name;
      Alias = alias;
      GetValue = getValue;
      SetValue = setValue;
    }
  }
}
