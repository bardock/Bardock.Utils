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
        /// <summary>
        /// Formats date and time to a normalized format: "yyyy-MM-dd HH:mm:ss"
        /// </summary>
        public static string NormalizedFormat(this DateTime d)
        {
            return d.ToString("yyyy-MM-dd HH:mm:ss");
        }

        /// <summary>
        /// Formats date to a normalized format: "yyyy-MM-dd"
        /// </summary>
        public static string NormalizedDateFormat(this DateTime d)
        {
            return d.ToString("yyyy-MM-dd");
        }

        /// <summary>
        /// Formats date and time to format: "yyyy/MM/dd HH:mm:ss"
        /// </summary>
        public static string Format(this DateTime d)
        {
            return d.ToString();
        }

        /// <summary>
        /// Formats date to a date format: "yyyy/MM/dd"
        /// </summary>
        public static string DateFormat(this DateTime d)
        {
            return d.ToShortDateString();
        }

        /// <summary>
        /// Formats date to a default date format: "dd/MM/yyyy"
        /// </summary>
        public static string DefaultDateFormat(this DateTime d)
        {
            return d.ToString("dd/MM/yyyy");
        }

        /// <summary>
        /// Formats date and time to a compact format: "yyyyMMddHHmmss"
        /// </summary>
        public static string CompactFormat(this DateTime d)
        {
            return d.ToString("yyyyMMddhhmmss");
        }

        /// <summary>
        /// Formats date to a compact format: "yyyyMMddHHmmss"
        /// </summary>
        public static string CompactDateFormat(this DateTime d)
        {
            return d.ToString("yyyyMMdd");
        }

        /// <summary>
        /// Formats date and time to ISO format.
        /// It will depend on date's kind. Examples: 
        ///     Unespecified    = 2013-01-05T12:45:06.1230000
        ///     Utc             = 2013-01-05T12:45:06.1230000Z
        ///     Local           = 2013-01-05T12:45:06.1230000-03:00
        /// </summary>
        public static string ToIsoFormat(this DateTime d)
        {
            switch (d.Kind)
            {
                case DateTimeKind.Unspecified:
                case DateTimeKind.Utc:
                    return d.ToString("o", System.Globalization.CultureInfo.InvariantCulture);

                case DateTimeKind.Local:
                    return new DateTimeOffset(d, TimeZoneInfo.Local.GetUtcOffset(d)).ToString("o");

                default:
                    throw new NotImplementedException(string.Format("DateTimeKind: {0}", d.Kind));
            }
        }

        /// <summary>
        /// Clones the date. Optionally overrides with specified parameters.
        /// </summary>
        public static DateTime Clone(
            this DateTime d, int? year = null, int? month = null, int? day = null, 
            int? hour = null, int? minute = null, int? second = null, int? millisecond = null, DateTimeKind? kind = null)
        {
            return new DateTime(
                year ?? d.Year, month ?? d.Month, day ?? d.Day, 
                hour ?? d.Hour, minute ?? d.Minute, second ?? d.Second, millisecond ?? d.Millisecond, kind ?? d.Kind);
        }

        /// <summary>
        /// Create a new date from the first moment of the day.
        /// </summary>
        public static DateTime ToDayStart(this DateTime d)
        {
            return d.Clone(hour: 0, minute: 0, second: 0, millisecond: 0);
        }

        /// <summary>
        /// Create a new date from the last moment of the day.
        /// </summary>
        public static DateTime ToDayEnd(this DateTime d)
        {
            return d.ToDayStart().AddDays(1).AddMilliseconds(-1);
        }

        /// <summary>
        /// Create a new date from the first moment of the month.
        /// </summary>
        public static DateTime ToMonthStart(this DateTime d)
        {
            return d.AddDays(1 - d.Day).ToDayStart();
        }

        /// <summary>
        /// Create a new date from the last moment of the month.
        /// </summary>
        public static DateTime ToMonthEnd(this DateTime d)
        {
            return d.ToMonthStart().AddMonths(1).AddMilliseconds(-1);
        }

        /// <summary>
        /// Create a new date from the first moment of the year.
        /// </summary>
        public static DateTime ToYearStart(this System.DateTime d)
        {
            return d.AddMonths(1 - d.Month).ToMonthStart();
        }

        /// <summary>
        /// Create a new date from the last moment of the year.
        /// </summary>
        public static DateTime ToYearEnd(this System.DateTime d)
        {
            return d.ToYearStart().AddYears(1).AddDays(-1);
        }

        /// <summary>
        /// Obtains the date and time format pattern.
        /// </summary>
        public static string CurrentFormat()
        {
            return System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.FullDateTimePattern;
        }

        /// <summary>
        /// Obtains the date format pattern.
        /// </summary>
        public static string CurrentDateFormat()
        {
            return System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern;
        }
    }
}