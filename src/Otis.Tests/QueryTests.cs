using NUnit.Framework;
using Otis.Entities;
using System.Linq;

namespace Otis.Tests
{
  public class QueryTests : DbTest
  {
    [Test]
    public void QueryTest()
    {
      var results = Db.Select<TestData>(b => b.Where(t => t.Id > 1).OrderBy(a => a.Id));

      Assert.IsFalse(results.Any(a => a.Id < 2));
    }

    [Test]
    public void QueryDistinctTest()
    {
      var results = Db.SelectDistinct<TestData>();

      Assert.IsTrue(results.Count > 0);
    }
  }
}
