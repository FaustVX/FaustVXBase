using System;

namespace FaustVXBase.Helpers
{
    public static class DateTimeUtilities
    {
        public static DateTime January(this int day, int? year = null) => new DateTime(year ?? DateTime.Now.Year, 1, day);

        public static DateTime February(this int day, int? year = null) => new DateTime(year ?? DateTime.Now.Year, 2, day);

        public static DateTime March(this int day, int? year = null) => new DateTime(year ?? DateTime.Now.Year, 3, day);

        public static DateTime April(this int day, int? year = null) => new DateTime(year ?? DateTime.Now.Year, 4, day);

        public static DateTime May(this int day, int? year = null) => new DateTime(year ?? DateTime.Now.Year, 5, day);

        public static DateTime June(this int day, int? year = null) => new DateTime(year ?? DateTime.Now.Year, 6, day);

        public static DateTime July(this int day, int? year = null) => new DateTime(year ?? DateTime.Now.Year, 7, day);

        public static DateTime August(this int day, int? year = null) => new DateTime(year ?? DateTime.Now.Year, 8, day);

        public static DateTime September(this int day, int? year = null) => new DateTime(year ?? DateTime.Now.Year, 9, day);

        public static DateTime October(this int day, int? year = null) => new DateTime(year ?? DateTime.Now.Year, 10, day);

        public static DateTime November(this int day, int? year = null) => new DateTime(year ?? DateTime.Now.Year, 11, day);

        public static DateTime December(this int day, int? year = null) => new DateTime(year ?? DateTime.Now.Year, 12, day);


        public static DateTime MillisecondsAgo(this int milliseconds) => DateTime.Now.AddMilliseconds(-milliseconds);

        public static DateTime SecondsAgo(this int seconds) => DateTime.Now.AddSeconds(-seconds);

        public static DateTime MinutesAgo(this int minutes) => DateTime.Now.AddMinutes(-minutes);

        public static DateTime HoursAgo(this int hours) => DateTime.Now.AddHours(-hours);

        public static DateTime DaysAgo(this int days) => DateTime.Now.AddDays(-days);

        public static DateTime WeeksAgo(this int weeks) => DateTime.Now.AddDays(-(weeks * 7));

        public static DateTime MounthsAgo(this int mounths) => DateTime.Now.AddMonths(-mounths);

        public static DateTime YearsAgo(this int years) => DateTime.Now.AddYears(-years);


        public static DateTime MillisecondsFromNow(this int milliseconds) => DateTime.Now.AddMilliseconds(milliseconds);

        public static DateTime SecondsFromNow(this int seconds) => DateTime.Now.AddSeconds(seconds);

        public static DateTime MinutesFromNow(this int minutes) => DateTime.Now.AddMinutes(minutes);

        public static DateTime HoursFromNow(this int hours) => DateTime.Now.AddHours(hours);

        public static DateTime DaysFromNow(this int days) => DateTime.Now.AddDays(days);

        public static DateTime WeeksFromNow(this int weeks) => DateTime.Now.AddDays((weeks * 7));

        public static DateTime MounthsFromNow(this int mounths) => DateTime.Now.AddMonths(mounths);

        public static DateTime YearsFromNow(this int years) => DateTime.Now.AddYears(years);


        public static bool IsWithin(this DateTime date, Period period) => period.IsWrap(date);
    }
}
