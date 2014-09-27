using System;
using System.Linq;

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
        
        public static SwitchHelper<T> Switch<T>(this T value, SwitchHelper<T>.SwitchBehavior behavior = SwitchHelper<T>.SwitchBehavior.OneCase) => new SwitchHelper<T>(value, behavior);
    }

    public class SwitchHelper<T>(T value, SwitchHelper<T>.SwitchBehavior behavior = SwitchHelper<T>.SwitchBehavior.OneCase)
    {
        public delegate void SwitchHelperDelegate(T value);
        public delegate void SwitchHelperSimpleDelegate();
        public delegate bool SwitchHelperPerdicate(T value);

        public enum SwitchBehavior { OneCase, AllCase}
        public SwitchBehavior Behavior { get; } = behavior;

        public T Value { get; } = value;
        private bool _executed = false;

        public SwitchHelper<T> Case<TType>(SwitchHelperDelegate action) => Case(v => v is TType, action);

        public SwitchHelper<T> Case<TType>(SwitchHelperSimpleDelegate action) => Case(v => v is TType, action);

        public SwitchHelper<T> Case(SwitchHelperPerdicate predicate, SwitchHelperDelegate action)
        {
            if ((Behavior == SwitchBehavior.AllCase || (Behavior == SwitchBehavior.OneCase && !_executed)) && predicate(Value))
            {
                _executed = true;
                action(Value);
            }
            return this;
        }

        public SwitchHelper<T> Case(SwitchHelperPerdicate predicate, SwitchHelperSimpleDelegate action)
        {
            if ((Behavior == SwitchBehavior.AllCase || (Behavior == SwitchBehavior.OneCase && !_executed)) && predicate(Value))
            {
                _executed = true;
                action();
            }
            return this;
        }
    }
}