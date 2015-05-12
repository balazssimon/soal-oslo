using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OsloExtensions.Extensions
{
    public static class EnumerableExtensions
    {
        public static IEnumerator<T> GetEnumerator<T>(this T item)
        {
            yield return item;
        }

        public static IEnumerable<T> Enumerate<T>(IEnumerator<T> items)
        {
            while (items.MoveNext())
            {
                yield return items.Current;
            }
        }
    }
}
