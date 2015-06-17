using System;
using System.Globalization;

namespace Bardock.Utils.Extensions
{
    public static class StringExtensions
    {
        /// <summary>
        /// Retrieves a new string with specified length at maximum.
        /// </summary>
        public static string Cut(this string str, int length)
        {
            return str.Substring(0, str.Length < length ? str.Length : length);
        }

        /// <summary>
        /// Retrieves a new string with specified length at maximum using trailing characters.
        /// </summary>
        public static string CutEnd(this string str, int length)
        {
            return str.Substring(str.Length <= length ? 0 : str.Length - length);
        }

        /// <summary>
        /// Indicates whether the specified value occurs
        /// within the source string using specified StringComparison.
        /// </summary>
        public static bool Contains(this string source, string value, StringComparison comp)
        {
            return source.IndexOf(value, comp) >= 0;
        }

        /// <summary>
        /// Indicates whether the specified value occurs
        /// within the source string ignoring diacritics and casing (configurable).
        /// </summary>
        public static bool IsLike(
            this string source,
            string value,
            bool IgnoreDiacritics = true,
            bool IgnoreCase = true)
        {
            var compareOptions = (IgnoreDiacritics ? CompareOptions.IgnoreNonSpace : CompareOptions.None)
                | (IgnoreCase ? CompareOptions.IgnoreCase : CompareOptions.None);

            return CultureInfo.CurrentCulture.CompareInfo.IndexOf(source, value, compareOptions) != -1;
        }

        /// <summary>
        /// Indicates whether all specified values occur
        /// within the source string ignoring diacritics and casing (configurable).
        /// </summary>
        public static bool IsLike(
            this string source,
            string[] values,
            bool IgnoreDiacritics = true,
            bool IgnoreCase = true)
        {
            foreach (string q in values)
            {
                if (!source.IsLike(q, IgnoreDiacritics, IgnoreCase))
                    return false;
            }
            return true;
        }
    }
}