using BenchmarkDotNet.Running;

namespace Otis.Benchmarks
{
  class Program
  {
    static void Main(string[] args)
    {
      var summary = BenchmarkRunner.Run<BigDataQueryBenchmarks>();
    }
  }
}
