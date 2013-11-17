using System;
using System.Text;

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
		/// Retrieves a new string with specified length at maximum using last characters.
		/// </summary>
        public static string CutEnd(this string str, int length)
		{
			return str.Substring(str.Length < length ? 0 : str.Length - length, length);
		}

        /// <summary>
        /// Determines if string contains another string using specified StringComparison
        /// </summary>
        public static bool Contains(this string source, string toCheck, StringComparison comp)
        {
            return source.IndexOf(toCheck, comp) >= 0;
        }
	}
}