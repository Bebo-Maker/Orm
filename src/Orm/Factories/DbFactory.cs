using System;
using System.Data;
using System.Data.Common;

namespace Orm.Factories
{
  public class DbFactory
  {
    public static IDbCommand CreateCommand(IDbConnection connection, string sqlStatement)
    {
      var command = connection.CreateCommand();
      command.CommandText = sqlStatement;
      return command;
    }

    public static DbCommand TryCreateAsyncComand(IDbConnection connection, string sqlStatement)
    {
      var cmd = connection.CreateCommand();
      if (cmd is not DbCommand command)
        throw new InvalidOperationException($"{cmd} has to be DbCommand");

      command.CommandText = sqlStatement;
      return command;
    }
  }
}
