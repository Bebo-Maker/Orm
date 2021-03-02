using Orm.Reflection;
using System.Collections.Generic;

namespace Orm.Entities
{
  public class TableDefinition
  {
    public string Name { get; }

    public Dictionary<string, FastPropertyInfo> Columns { get; }

    public TableDefinition(string name, Dictionary<string, FastPropertyInfo> columns)
    {
      Name = name;
      Columns = columns;
    }
  }
}
