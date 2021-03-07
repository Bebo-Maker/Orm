using NUnit.Framework;
using Orm.Entities;
using System.Linq;

namespace Orm.Tests
{
  public class QueryTests
  {
    private const string Table = "TestTable";
    private const string BigDataTable = "TestTableBigData";

    private Database _db;

    [SetUp]
    public void Setup()
    {
      _db = new Database(@"Server=localhost\SQLEXPRESS;Database=TestDB;Trusted_Connection=True;");
    }

    [Test]
    public void QueryTest()
    {
      var results = _db.Query<TestData>(b => b.Where(t => t.Id > 1).OrderBy(a => a.Id));

      Assert.IsFalse(results.Any(a => a.Id < 2));
    }
  }
}
