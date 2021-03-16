using NUnit.Framework;
using Otis.Entities;
using System;
using System.Threading.Tasks;

namespace Otis.Tests
{
  public class DeleteTests : DbTest
  {
    private const int ID = 84334819;

    [Test]
    public void DeleteTest()
    {
      Db.Insert(Create());

      int affected = Db.Delete<TestData>(a => a.Where(t => t.Id == ID));

      Assert.IsTrue(affected == 1);
    }

    [Test]
    public async Task DeleteAsyncTest()
    {
      await Db.InsertAsync(Create());

      int affected = await Db.DeleteAsync<TestData>(a => a.Where(t => t.Id == ID));

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
