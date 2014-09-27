using System;
using System.Collections.Generic;
using System.Linq;

namespace FaustVXBase.Helpers
{
    public static class ListsUtilities
    {
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> list) => list == null || !list.Any();

        public static bool IsNotNullAndNotEmpty<T>(this IEnumerable<T> list) => list != null && list.Any();

        public static IEnumerable<T> EmptyIfNull<T>(this IEnumerable<T> list) => list ?? Enumerable.Empty<T>();

        public static IEnumerable<T> ForEach<T>(this IEnumerable<T> list, Func<T, int, T> func)
        {
            int i = 0;
            foreach (var item in list)
                yield return func(item, i++);
        }

        public static void ForEach<T>(this IEnumerable<T> list, Action<T, int> action)
        {
            ForEach(list, (item, index) =>
            {
                action(item, index);
                return item;
            }).ToArray();
        }


    }
}
