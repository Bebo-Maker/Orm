using NUnit.Framework;
using Orm.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Orm.Tests
{
  public class Tests
  {
    private const string Table = "TestTable";

    private Database _db;

    [SetUp]
    public void Setup()
    {
      _db = Database.Connect(@"Server=localhost\SQLEXPRESS;Database=TestDB;Trusted_Connection=True;");
    }

    [Test]
    public void PropertyInjectionTest()
    {
      var results = _db.Query<TestData>($"SELECT * FROM {Table}").ToList();

      Assert.IsTrue(results.Count > 0);
    }

    [Test]
    public async Task PropertyInjectionAsyncTest()
    {
      var results = await _db.QueryAsync<TestData>($"SELECT * FROM {Table}");

      Assert.IsTrue(results.Count > 0);
    }

    [Test]
    public void ConstructorInjectionTest()
    {
      var results = _db.Query<ConstructorTestData>($"SELECT * FROM {Table}").ToList();

      Assert.IsTrue(results.Count > 0);
    }

    [Test]
    public async Task ConstructorInjectionAsyncTest()
    {
      var results = await _db.QueryAsync<ConstructorTestData>($"SELECT * FROM {Table}");

      Assert.IsTrue(results.Count > 0);
    }
  }
}