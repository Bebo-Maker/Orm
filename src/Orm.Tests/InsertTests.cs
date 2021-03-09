using NUnit.Framework;
using Orm.Entities;
using System;
using System.Threading.Tasks;

namespace Orm.Tests
{
  public class InsertTests : DbTest
  {
    private const int ID = 103523;

    [Test]
    public void InsertTest()
    {
      Connection.Delete<TestData>(a => a.Where(a => a.Id == ID));

      var data = Create();
      var result = Connection.Insert(data);

      Assert.IsTrue(result == 1);
    }

    [Test]
    public async Task InsertAsyncTest()
    {
      await Connection.DeleteAsync<TestData>(a => a.Where(a => a.Id == ID));

      var data = Create();
      var result = await Connection.InsertAsync(data);

      Assert.IsTrue(result == 1);
    }

    private static TestData Create()
    {
      return new TestData
      {
        Datetime = DateTime.Now,
        Id = ID,
        LongggText = "LongText",
        Number = 912723,
        Name = "MyName",
      };
    }
  }
}
