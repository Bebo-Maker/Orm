using NUnit.Framework;
using System.Data;
using System.Data.SqlClient;

namespace Orm.Tests
{
  public class DbTest
  {
    protected IDbConnection Connection { get; private set; }

    [SetUp]
    public void Setup()
    {
      Connection = new SqlConnection(@"Server=localhost\SQLEXPRESS;Database=TestDB;Trusted_Connection=True;");
      Connection.Open();
    }
  }
}
