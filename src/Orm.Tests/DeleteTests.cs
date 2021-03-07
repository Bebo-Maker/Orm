using NUnit.Framework;
using Orm.Entities;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Orm.Tests
{
  public class DeleteTests
  {
    private IDbConnection _connection;

    [SetUp]
    public void Setup()
    {
      _connection = new SqlConnection(@"Server=localhost\SQLEXPRESS;Database=TestDB;Trusted_Connection=True;");
      _connection.Open();
    }

    [Test]
    public void DeleteTest()
    {
      int affected = _connection.Delete<TestData>(a => a.Where(t => t.Id == 1));

      Assert.IsTrue(affected == 1);
    }

    [Test]
    public async Task DeleteAsyncTest()
    {
      int affected = await _connection.DeleteAsync<TestData>(a => a.Where(t => t.Id == 1));

      Assert.IsTrue(affected == 1);
    }
  }
}
