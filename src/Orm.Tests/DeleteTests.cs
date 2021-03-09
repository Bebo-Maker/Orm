using NUnit.Framework;
using Orm.Entities;
using System.Threading.Tasks;

namespace Orm.Tests
{
  public class DeleteTests : DbTest
  {
    [Test]
    public void DeleteTest()
    {
      int affected = Connection.Delete<TestData>(a => a.Where(t => t.Id == 1));

      Assert.IsTrue(affected == 1);
    }

    [Test]
    public async Task DeleteAsyncTest()
    {
      int affected = await Connection.DeleteAsync<TestData>(a => a.Where(t => t.Id == 1));

      Assert.IsTrue(affected == 1);
    }
  }
}
