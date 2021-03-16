using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Otis.Extensions
{
  public static class TaskExtensions
  {
    /// <inheritdoc cref="Enumerable.FirstOrDefault{TSource}(IEnumerable{TSource})" />
    public static async Task<T> FirstOrDefault<T>(this Task<IEnumerable<T>> task) 
      => (await task.ConfigureAwait(false)).FirstOrDefault();

    /// <inheritdoc cref="Enumerable.FirstOrDefault{TSource}(IEnumerable{TSource})" />
    public static async Task<T> FirstOrDefault<T>(this Task<List<T>> task)
      => (await task.ConfigureAwait(false)).FirstOrDefault();
  }
}
