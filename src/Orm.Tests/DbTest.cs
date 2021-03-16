using NUnit.Framework;
using Orm.Entities;

namespace Orm.Tests
{
  public class DbTest
  {
    protected IDatabase Db { get; private set; }

    [SetUp]
    public void Setup()
    {
      Db = new Database(Connection.Provider);
    }
  }
}
