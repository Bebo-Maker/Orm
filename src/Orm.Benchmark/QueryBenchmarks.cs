using BenchmarkDotNet.Attributes;
using Orm.Entities;
using System.Collections.Generic;

namespace Orm.Benchmarks
{
  public class QueryBenchmarks
  {
    private readonly Database _db;

    public QueryBenchmarks()
    {
      _db = Database.Connect(@"Server=localhost\SQLEXPRESS;Database=TestDB;Trusted_Connection=True;");
    }

    [Benchmark]
    public List<TestData> QueryProperties() => _db.Query<TestData>("SELECT [Name],[Id],[Datetime],[Number],[LongText] FROM[TestDB].[dbo].[TestTable]");

    [Benchmark]
    public List<ConstructorTestData> QueryConstructor() => _db.Query<ConstructorTestData>("SELECT [Name],[Id],[Datetime],[Number],[LongText] FROM[TestDB].[dbo].[TestTable]");
  }
}
