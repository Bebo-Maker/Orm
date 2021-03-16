using BenchmarkDotNet.Attributes;
using Orm.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Orm.Benchmarks
{
  public class BigDataQueryBenchmarks
  {
    private const string SelectAllFromTestTable = "SELECT [Name],[Id],[Datetime],[Number],[LongText] FROM[TestDB].[dbo].[TestTableBigData]";

    private readonly IDatabase _db;

    public BigDataQueryBenchmarks()
    {
      _db = new Database(Connection.Provider);
    }

    [Benchmark]
    public List<TestData> QueryProperties() => _db.Select<TestData>(SelectAllFromTestTable);

    [Benchmark]
    public List<ConstructorTestData> QueryConstructor() => _db.Select<ConstructorTestData>(SelectAllFromTestTable);

    [Benchmark]
    public async Task<List<TestData>> QueryPropertiesAsync() => await _db.SelectAsync<TestData>(SelectAllFromTestTable);

    [Benchmark]
    public async Task<List<ConstructorTestData>> QueryConstructorAsync() => await _db.SelectAsync<ConstructorTestData>(SelectAllFromTestTable);
  }
}
