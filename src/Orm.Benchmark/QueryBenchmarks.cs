using BenchmarkDotNet.Attributes;
using Orm.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Orm.Benchmarks
{
  public class QueryBenchmarks
  {
    private const string SelectAllFromTestTable = "SELECT [Name],[Id],[Datetime],[Number],[LongText] FROM[TestDB].[dbo].[TestTable]";

    private readonly Database _db;

    public QueryBenchmarks()
    {
      _db = new Database(@"Server=localhost\SQLEXPRESS;Database=TestDB;Trusted_Connection=True;");
    }

    [Benchmark]
    public List<TestData> QueryProperties() => _db.Query<TestData>(SelectAllFromTestTable);

    [Benchmark]
    public List<ConstructorTestData> QueryConstructor() => _db.Query<ConstructorTestData>(SelectAllFromTestTable);


    [Benchmark]
    public async Task<List<TestData>> QueryPropertiesAsync() => await _db.QueryAsync<TestData>(SelectAllFromTestTable);

    [Benchmark]
    public async Task<List<ConstructorTestData>> QueryConstructorAsync() => await _db.QueryAsync<ConstructorTestData>(SelectAllFromTestTable);
  }
}
