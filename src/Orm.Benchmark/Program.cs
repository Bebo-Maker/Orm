using BenchmarkDotNet.Running;

namespace Orm.Benchmarks
{
  class Program
  {
    static void Main(string[] args)
    {
      var summary = BenchmarkRunner.Run<QueryBenchmarks>();
    }
  }
}
