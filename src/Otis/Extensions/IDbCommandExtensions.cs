using System;
using System.Data;

namespace Otis.Extensions
{
  internal static class IDbCommandExtensions
  {
    public static void AddParameter(this IDbCommand command, string name, object value)
    {
      var parameter = command.CreateParameter();
      parameter.ParameterName = name;
      parameter.Value = value is null ? DBNull.Value : value;
      command.Parameters.Add(parameter);
    }
  }
}
