using NUnit.Framework;
using Orm.Configuration;
using Orm.Entities;

namespace Orm.Tests
{
  public class TestDataMap : EntityMap<TestData>
  {
    public TestDataMap()
    {
      Alias("TestTable");

      Map(p => p.Id).AsPrimaryKey();
      Map(p => p.Datetime);
      Map(p => p.Number);
      Map(p => p.Name);
      Map(p => p.LongggText).WithAlias("LongText");
    }
  }

  public class MappingTest
  {
    private Database _db;

    [SetUp]
    public void Setup()
    {
      var maps = new EntityMap[]
      {
        new TestDataMap()
      };

      _db = new Database(Connection.Provider, new DatabaseConfiguration(maps));
    }

    [Test]
    public void SelectPersonTest()
    {
      var result = _db.Select<TestData>();

      Assert.That(result.Count > 0);
    }
  }
}
