using Orm.Entities;
using System.Linq;

namespace Orm.Utils
{
  public static class SqlUtils
  {
    public static string BuildParameters(Table table) => string.Join(", ", table.Columns.Select(c => $"@{c.Alias}"));
    public static string BuildUpdateParameters(Table table) => string.Join(", ", table.Columns.Select(c => $"{c.Alias} = @{c.Alias}"));
  }
}
