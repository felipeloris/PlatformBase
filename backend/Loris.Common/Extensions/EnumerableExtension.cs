using System.Collections.Generic;

namespace Loris.Common.Extensions
{
    public static class EnumerableExtension
    {
        public static void AddRange<T>(this IList<T> collection, IEnumerable<T> items)
        {
            foreach (var item in items)
            {
                collection.Add(item);
            }
        }
    }
}
