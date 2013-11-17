using System.Runtime.CompilerServices;
using System.Globalization;
using Bardock.Utils.Globalization;

namespace Bardock.Utils.Extensions
{

    public static class DecimalFormatExtensions
	{
		/// <summary>
		/// Returns string representation with currency format without symbol
		/// </summary>
        public static string ToCurrencyString(this decimal d)
		{
			return d.ToString("#,0.00");
		}

		/// <summary>
		/// Returns string representation with currency format without symbol neither thousands separator
		/// </summary>
        public static string ToCurrencyInputString(this decimal d)
		{
			return d.ToString("0.00");
		}

		/// <summary>
		/// Returns string representation with invariant format
		/// </summary>
        public static string ToInvariantFormat(this decimal d)
		{
            return d.ToString(CultureInfo.InvariantCulture);
		}

        public static NumberFormatInfo CurrentFormatInfo
        {
			get { return CultureInfo.CurrentCulture.NumberFormat; }
		}

	}

}