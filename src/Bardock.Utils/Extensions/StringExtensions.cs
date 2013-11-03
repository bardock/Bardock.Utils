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

        public static string PadLeft(this string input, char pad, int length)
        {
            var sb = new StringBuilder();
            for (var i = 0; i < length - input.Length; i++)
                sb.Append(pad);
            sb.Append(input);
            return sb.ToString();
        }

        public static bool Contains(this string source, string toCheck, StringComparison comp)
        {
            return source.IndexOf(toCheck, comp) >= 0;
        }
	}

}