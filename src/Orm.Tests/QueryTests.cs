using NUnit.Framework;
using Orm.Entities;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace Orm.Tests
{
  public class QueryTests
  {
    private IDbConnection _connection;

    [SetUp]
    public void Setup()
    {
      _connection = new SqlConnection(@"Server=localhost\SQLEXPRESS;Database=TestDB;Trusted_Connection=True;");
      _connection.Open();
    }

    [Test]
    public void QueryTest()
    {
      var results = _connection.Query<TestData>(b => b.Where(t => t.Id > 1).OrderBy(a => a.Id));

      Assert.IsFalse(results.Any(a => a.Id < 2));
    }

    [Test]
    public void QueryDistinctTest()
    {
      var results = _connection.QueryDistinct<TestData>();

      Assert.IsTrue(results.Count > 0);
    }

    [TearDown]
    public void TearDown() => _connection.Dispose();
  }
}
