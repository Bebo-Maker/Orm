using NUnit.Framework;
using Otis.Entities;

namespace Otis.Tests
{
  public class DbTest
  {
    protected IDatabase Db { get; private set; }

    [SetUp]
    public void Setup()
    {
      Db = Infrastructure.Database;
    }
  }
}
