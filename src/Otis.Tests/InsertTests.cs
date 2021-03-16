using NUnit.Framework;
using Otis.Entities;
using System;
using System.Threading.Tasks;

namespace Otis.Tests
{
  public class InsertTests : DbTest
  {
    private const int ID = 103523;

    [Test]
    public void InsertTest()
    {
      Db.Delete<TestData>(a => a.Where(a => a.Id == ID));

      var data = Create();
      var result = Db.Insert(data);

      Assert.IsTrue(result == 1);
    }

    [Test]
    public async Task InsertAsyncTest()
    {
      await Db.DeleteAsync<TestData>(a => a.Where(a => a.Id == ID));

      var data = Create();
      var result = await Db.InsertAsync(data);

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
