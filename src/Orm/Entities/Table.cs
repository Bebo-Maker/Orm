using Orm.Reflection;
using System.Collections.Generic;

namespace Orm.Entities
{
  public class Table
  {
    public string Name { get; }

    public string[] ColumnNames { get; }

    public Dictionary<string, FastPropertyInfo> Columns { get; }

    public Table(string name, string[] columnNames, Dictionary<string, FastPropertyInfo> columns)
    {
      Name = name;
      ColumnNames = columnNames;
      Columns = columns;
    }
  }
}
