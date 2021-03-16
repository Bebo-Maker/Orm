using NUnit.Framework;
using Otis.Entities;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Otis.Tests
{
  public class UpdateTests : DbTest
  {
    private const int ID = 1276945904;
    private const string NAME_BEFORE = "Alex";
    private const string NAME_AFTER = "Jack";

    [Test]
    public void UpdateTest()
    {
      Db.Delete<TestData>(q => q.Where(a => a.Id == ID));
      Db.Insert(Create());
      var entity = Db.Select<TestData>(q => q.Where(a => a.Id == ID)).FirstOrDefault();
      entity.Name = NAME_AFTER;

      Db.Update(entity, q => q.Where(d => d.Id == entity.Id));

      var entityAfter = Db.Select<TestData>(q => q.Where(a => a.Id == ID)).FirstOrDefault();
      Assert.That(entityAfter.Name == NAME_AFTER);
    }

    [Test]
    public async Task UpdateAsyncTest()
    {
      await Db.DeleteAsync<TestData>(q => q.Where(a => a.Id == ID));
      await Db.InsertAsync(Create());
      var entities = await Db.SelectAsync<TestData>(q => q.Where(a => a.Id == ID));
      var entity = entities.FirstOrDefault();
      entity.Name = NAME_AFTER;

      await Db.UpdateAsync(entity, q => q.Where(d => d.Id == entity.Id));

      var entitiesAfter = await Db.SelectAsync<TestData>(q => q.Where(a => a.Id == ID));
      Assert.That(entitiesAfter.First().Name == NAME_AFTER);
    }

    private static TestData Create()
    {
      return new TestData
      {
        Id = ID,
        Datetime = DateTime.Now,
        LongggText = "asdkjaskdjiklasjdlasdklaskldjkladjfg klsjlf",
        Name = NAME_BEFORE,
        Number = 406790904
      };
    }
  }
}
