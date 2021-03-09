using NUnit.Framework;
using Orm.Entities;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace Orm.Tests
{
  public class QueryTests : DbTest
  {
    [Test]
    public void QueryTest()
    {
      var results = Connection.Query<TestData>(b => b.Where(t => t.Id > 1).OrderBy(a => a.Id));

      Assert.IsFalse(results.Any(a => a.Id < 2));
    }

    [Test]
    public void QueryDistinctTest()
    {
      var results = Connection.QueryDistinct<TestData>();

      Assert.IsTrue(results.Count > 0);
    }

    [TearDown]
    public void TearDown() => Connection.Dispose();
  }
}
