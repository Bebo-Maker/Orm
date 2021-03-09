using NUnit.Framework;
using Orm.Entities;
using System;
using System.Threading.Tasks;

namespace Orm.Tests
{
  public class DeleteTests : DbTest
  {
    private const int ID = 84334819;

    [Test]
    public void DeleteTest()
    {
      Connection.Insert(Create());

      int affected = Connection.Delete<TestData>(a => a.Where(t => t.Id == ID));

      Assert.IsTrue(affected == 1);
    }

    [Test]
    public async Task DeleteAsyncTest()
    {
      await Connection.InsertAsync(Create());

      int affected = await Connection.DeleteAsync<TestData>(a => a.Where(t => t.Id == ID));

      Assert.IsTrue(affected == 1);
    }

    private static TestData Create()
    {
      return new TestData
      {
        Id = ID,
        Datetime = DateTime.Now,
        LongggText = "LongText",
        Number = 912723,
        Name = "MyName",
      };
    }
  }
}
