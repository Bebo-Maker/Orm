using Orm.Entities;
using Orm.Extensions;
using Orm.Reflection;
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

    public static IDbCommand CreateCommandWithParameters<T>(IDbConnection connection, string sqlStatement, T entity)
    {
      var command = CreateCommand(connection, sqlStatement);
      var columns = TableFactory.GetOrCreateTableDefinition<T>().Columns;
      AttachParams(command, columns, entity);
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

    public static DbCommand TryCreateAsyncCommandWithParameters<T>(IDbConnection connection, string sqlStatement, T entity)
    {
      var command = TryCreateAsyncComand(connection, sqlStatement);
      var columns = TableFactory.GetOrCreateTableDefinition<T>().Columns;
      AttachParams(command, columns, entity);
      return command;
    }

    private static void AttachParams<T>(IDbCommand command, Column[] columns, T entity)
    {
      foreach (var column in columns)
        command.AddParameter($"@{column.Alias}", column.GetValue(entity));
    }
  }
}
