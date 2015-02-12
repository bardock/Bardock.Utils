using System;
using System.Globalization;
using System.Text;
using System.Linq;

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
        /// Returns a value indicating whether the specified System.String object occurs
        /// within this string using specified StringComparison.
        /// </summary>
        public static bool Contains(this string source, string toCheck, StringComparison comp)
        {
            return source.IndexOf(toCheck, comp) >= 0;
        }

        /// <summary>
        /// Returns a value indicating whether the specified System.String object occurs
        /// within this string using specified CultureInfo.CurrentCulture.CompareInfo.
        /// </summary>
        public static bool IsLike(
            this string str,
            string query,
            bool IgnoreDiacritics = true,
            bool IgnoreCase = true)
        {
            return CultureInfo.CurrentCulture.CompareInfo.IndexOf(str,
                query,
                (IgnoreDiacritics ? CompareOptions.IgnoreNonSpace : CompareOptions.None) | (IgnoreCase ? CompareOptions.IgnoreCase : CompareOptions.None)) != -1;
        }
	}
}