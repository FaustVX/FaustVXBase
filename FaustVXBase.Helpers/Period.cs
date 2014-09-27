using System;

namespace FaustVXBase.Helpers
{
    public class Period(DateTime start, DateTime end)
    {
        public DateTime Start { get; } = start;
        public DateTime End { get; } = end;
        public TimeSpan Length { get; } = end - start;

        public bool IsOverlap(Period otherPeriod, bool edgesOverlap = false)
        {
            if (edgesOverlap)
                return Start <= otherPeriod.End && End >= otherPeriod.Start;
            return Start < otherPeriod.End && End > otherPeriod.Start;
        }

        public bool IsWrap(DateTime date) => Start < date && End > date;
    }
}
