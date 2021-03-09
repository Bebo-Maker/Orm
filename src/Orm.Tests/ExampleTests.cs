using NUnit.Framework;
using Orm.Entities;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Orm.Tests
{
  public class Tests : DbTest
  {
    private const string Table = "TestTable";
    private const string BigDataTable = "TestTableBigData";

    [Test]
    public void PropertyInjectionTest()
    {
      var results = Connection.Query<TestData>($"SELECT * FROM {Table}").ToList();

      Assert.IsTrue(results.Count > 0);
    }

    [Test]
    public async Task PropertyInjectionAsyncTest()
    {
      var results = await Connection.QueryAsync<TestData>($"SELECT * FROM {Table}");

      Assert.IsTrue(results.Count > 0);
    }

    [Test]
    public void ConstructorInjectionTest()
    {
      var results = Connection.Query<ConstructorTestData>($"SELECT * FROM {Table}").ToList();

      Assert.IsTrue(results.Count > 0);
    }

    [Test]
    public async Task ConstructorInjectionAsyncTest()
    {
      var results = await Connection.QueryAsync<ConstructorTestData>($"SELECT * FROM {Table}");

      Assert.IsTrue(results.Count > 0);
    }

    [Test]
    public void PropertyInjectionBigDataTest()
    {
      var results = Connection.Query<TestData>($"SELECT * FROM {BigDataTable}").ToList();

      Assert.IsTrue(results.Count > 0);
    }

    [Test]
    public async Task PropertyInjectionAsyncBigDataTest()
    {
      var results = await Connection.QueryAsync<TestData>($"SELECT * FROM {BigDataTable}");

      Assert.IsTrue(results.Count > 0);
    }

    [Test]
    public void ConstructorInjectionBigDataTest()
    {
      var results = Connection.Query<ConstructorTestData>($"SELECT * FROM {BigDataTable}").ToList();

      Assert.IsTrue(results.Count > 0);
    }

    [Test]
    public async Task ConstructorInjectionAsyncBigDataTest()
    {
      var results = await Connection.QueryAsync<ConstructorTestData>($"SELECT * FROM {BigDataTable}");

      Assert.IsTrue(results.Count > 0);
    }

    [TearDown]
    public void TearDown() => Connection.Dispose();
  }
}