using BenchmarkDotNet.Attributes;
using Otis.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Otis.Benchmarks
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
    public List<TestData> QueryProperties() => _db.ExecuteReader<TestData>(SelectAllFromTestTable);

    [Benchmark]
    public List<ConstructorTestData> QueryConstructor() => _db.ExecuteReader<ConstructorTestData>(SelectAllFromTestTable);


    [Benchmark]
    public async Task<List<TestData>> QueryPropertiesAsync()
    {
      return await _db.ExecuteReaderAsync<TestData>(SelectAllFromTestTable).ConfigureAwait(false);
    }

    [Benchmark]
    public async Task<List<ConstructorTestData>> QueryConstructorAsync()
    {
      return await _db.ExecuteReaderAsync<ConstructorTestData>(SelectAllFromTestTable).ConfigureAwait(false);
    }
  }
}
