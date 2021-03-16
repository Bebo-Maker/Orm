using BenchmarkDotNet.Attributes;
using Orm.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Orm.Benchmarks
{
  public class QueryBenchmarks
  {
    private const string SelectAllFromTestTable = "SELECT [Name],[Id],[Datetime],[Number],[LongText] FROM[TestDB].[dbo].[TestTable]";

    private readonly IDatabase _db;

    public QueryBenchmarks()
    {
      _db = new Database(Connection.Provider);
    }

    [Benchmark]
    public List<TestData> QueryProperties() => _db.Select<TestData>(SelectAllFromTestTable).ToList();

    [Benchmark]
    public List<ConstructorTestData> QueryConstructor() => _db.Select<ConstructorTestData>(SelectAllFromTestTable).ToList();


    [Benchmark]
    public async Task<List<TestData>> QueryPropertiesAsync()
    {
      var result = await _db.SelectAsync<TestData>(SelectAllFromTestTable).ConfigureAwait(false);
      return result.ToList();
    }

    [Benchmark]
    public async Task<List<ConstructorTestData>> QueryConstructorAsync()
    {
      var result = await _db.SelectAsync<ConstructorTestData>(SelectAllFromTestTable).ConfigureAwait(false);
      return result.ToList();
    }
  }
}
