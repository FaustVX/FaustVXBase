using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FaustVXBase.Helpers
{
    public static class Utilities
    {
        public static bool IsBetween<T>(this T value, T min, T max)
            where T : IComparable<T>
        {
            return value.CompareTo(min) >= 0 && value.CompareTo(max) <= 0;
        }

        public static T Max<T>(T first, T second)
            where T : IComparable<T>
        {
            return (first.CompareTo(second) >= 0) ? first : second;
        }

        public static T Min<T>(T first, T second)
            where T : IComparable<T>
        {
            return (first.CompareTo(second) <= 0) ? first : second;
        }

        public static T Max<T>(params T[] values)
            where T : IComparable<T>
        {
            Array.Sort(values);
            return values.FirstOrDefault();
        }

        public static T Min<T>(params T[] values)
            where T : IComparable<T>
        {
            Array.Sort(values);
            return values.LastOrDefault();
        }
    }
}
