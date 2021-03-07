using BenchmarkDotNet.Attributes;
using Orm.Entities;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Orm.Benchmarks
{
  public class QueryBenchmarks
  {
    private const string SelectAllFromTestTable = "SELECT [Name],[Id],[Datetime],[Number],[LongText] FROM[TestDB].[dbo].[TestTable]";

    private readonly IDbConnection _conn;

    public QueryBenchmarks()
    {
      _conn = new SqlConnection(@"Server=localhost\SQLEXPRESS;Database=TestDB;Trusted_Connection=True;");
      _conn.Open();
    }

    [Benchmark]
    public List<TestData> QueryProperties() => _conn.Query<TestData>(SelectAllFromTestTable);

    [Benchmark]
    public List<ConstructorTestData> QueryConstructor() => _conn.Query<ConstructorTestData>(SelectAllFromTestTable);


    [Benchmark]
    public async Task<List<TestData>> QueryPropertiesAsync() => await _conn.QueryAsync<TestData>(SelectAllFromTestTable);

    [Benchmark]
    public async Task<List<ConstructorTestData>> QueryConstructorAsync() => await _conn.QueryAsync<ConstructorTestData>(SelectAllFromTestTable);
  }
}
