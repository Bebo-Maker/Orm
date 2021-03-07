using NUnit.Framework;
using Orm.Entities;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Orm.Tests
{
  public class Tests
  {
    private const string Table = "TestTable";
    private const string BigDataTable = "TestTableBigData";

    private IDbConnection _connection;

    [SetUp]
    public void Setup()
    {
      _connection = new SqlConnection(@"Server=localhost\SQLEXPRESS;Database=TestDB;Trusted_Connection=True;");
      _connection.Open();
    }

    [Test]
    public void PropertyInjectionTest()
    {
      var results = _connection.Query<TestData>($"SELECT * FROM {Table}").ToList();

      Assert.IsTrue(results.Count > 0);
    }

    [Test]
    public async Task PropertyInjectionAsyncTest()
    {
      var results = await _connection.QueryAsync<TestData>($"SELECT * FROM {Table}");

      Assert.IsTrue(results.Count > 0);
    }

    [Test]
    public void ConstructorInjectionTest()
    {
      var results = _connection.Query<ConstructorTestData>($"SELECT * FROM {Table}").ToList();

      Assert.IsTrue(results.Count > 0);
    }

    [Test]
    public async Task ConstructorInjectionAsyncTest()
    {
      var results = await _connection.QueryAsync<ConstructorTestData>($"SELECT * FROM {Table}");

      Assert.IsTrue(results.Count > 0);
    }

    [Test]
    public void PropertyInjectionBigDataTest()
    {
      var results = _connection.Query<TestData>($"SELECT * FROM {BigDataTable}").ToList();

      Assert.IsTrue(results.Count > 0);
    }

    [Test]
    public async Task PropertyInjectionAsyncBigDataTest()
    {
      var results = await _connection.QueryAsync<TestData>($"SELECT * FROM {BigDataTable}");

      Assert.IsTrue(results.Count > 0);
    }

    [Test]
    public void ConstructorInjectionBigDataTest()
    {
      var results = _connection.Query<ConstructorTestData>($"SELECT * FROM {BigDataTable}").ToList();

      Assert.IsTrue(results.Count > 0);
    }

    [Test]
    public async Task ConstructorInjectionAsyncBigDataTest()
    {
      var results = await _connection.QueryAsync<ConstructorTestData>($"SELECT * FROM {BigDataTable}");

      Assert.IsTrue(results.Count > 0);
    }

    [TearDown]
    public void TearDown() => _connection.Dispose();
  }
}