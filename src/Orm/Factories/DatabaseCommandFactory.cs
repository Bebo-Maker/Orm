using Orm.Entities;
using Orm.Extensions;
using System;
using System.Data;
using System.Data.Common;

namespace Orm.Factories
{
  internal class DatabaseCommandFactory
  {
    private readonly ITableFactory _factory;
    private readonly IDatabaseProvider _provider;

    public DatabaseCommandFactory(ITableFactory factory, IDatabaseProvider provider)
    {
      _factory = factory;
      _provider = provider;
    }
    
    public DbConnection TryCreateAsyncConnection()
    {
      if (_provider.CreateConnection() is DbConnection connection)
        return connection;

      throw new InvalidOperationException($"{typeof(IDbConnection).Name} has to be {typeof(DbConnection).Name}");
    }

    public static IDbCommand CreateCommand(IDbConnection connection, string sqlStatement)
    {
      var command = connection.CreateCommand();
      command.CommandText = sqlStatement;

      return command;
    }

    public IDbCommand CreateCommandWithParameters<T>(IDbConnection connection, string sqlStatement, T entity)
    {
      var command = CreateCommand(connection, sqlStatement);
      var columns = _factory.GetOrCreateTable<T>().Columns;
      AttachParams(command, columns, entity);
      return command;
    }

    public static DbCommand TryCreateAsyncComand(IDbConnection connection, string sqlStatement)
    {
      var cmd = connection.CreateCommand();
      if (cmd is not DbCommand command)
        throw new InvalidOperationException($"{typeof(IDbCommand).Name} has to be {typeof(DbCommand).Name}");

      command.CommandText = sqlStatement;

      return command;
    }

    public DbCommand TryCreateAsyncCommandWithParameters<T>(IDbConnection connection, string sqlStatement, T entity)
    {
      var command = TryCreateAsyncComand(connection, sqlStatement);
      var columns = _factory.GetOrCreateTable<T>().Columns;
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
