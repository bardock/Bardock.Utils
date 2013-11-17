using System;
using System.Runtime.CompilerServices;

namespace Bardock.Utils.Extensions
{
    /// <summary>
    /// Provides helper methods for Dates.
    /// Custom Date and Time Format Strings: http://msdn.microsoft.com/en-us/library/8kb3ddd4.aspx
    /// </summary>
    public static class DateExtensions
    {
        public static string NormalizedFormat(this DateTime d)
        {
            return d.ToString("yyyy-MM-dd HH:mm:ss");
        }

        public static string NormalizedDateFormat(this DateTime d)
        {
            return d.ToString("yyyy-MM-dd");
        }

        public static string DefaultDateFormat(this DateTime d)
        {
            return d.ToString("dd/MM/yyyy");
        }

        public static string CompactFormat(this DateTime d)
        {
            return d.ToString("yyyyMMddhhmmss");
        }

        public static string CompactDateFormat(this DateTime d)
        {
            return d.ToString("yyyyMMdd");
        }

        public static DateTime ToDayStart(this DateTime d)
        {
            return new DateTime(d.Year, d.Month, d.Day);
        }

        public static DateTime ToMonthStart(this DateTime d)
        {
            return d.AddDays(1 - d.Day);
        }

        public static DateTime ToMonthEnd(this DateTime d)
        {
            return d.ToMonthStart().AddMonths(1).AddDays(-1);
        }

        public static string CurrentFormat()
        {
            return System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.FullDateTimePattern;
        }

        public static string CurrentDateFormat()
        {
            return System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern;
        }

        public static string ToIso (this DateTime d){
            DateTimeOffset dateOffset = new DateTimeOffset(d,TimeZoneInfo.Local.GetUtcOffset(d));
            return dateOffset.ToString("o");
        }
    }

}