using NUnit.Framework;
using Otis.Configuration;
using Otis.Entities;
using System.Data.SqlClient;

namespace Otis.Tests
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
    private IDatabase _db;

    [SetUp]
    public void Setup()
    {
      _db = new DatabaseBuilder().WithConnection(Infrastructure.GetConnection)
                                 .WithMap(new TestDataMap())
                                 .Build();
    }

    [Test]
    public void SelectPersonTest()
    {
      var result = _db.Select<TestData>();

      Assert.That(result.Count > 0);
    }
  }
}
