using System.Globalization;

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
        /// Returns string representation with currency format without symbol
        /// </summary>
        public static string ToAccurateString(this decimal d)
        {
            return d.ToString("#,0.00####");
        }

        /// <summary>
        /// Returns string representation with currency format without symbol neither thousands separator
        /// </summary>
        public static string ToAccurateInputString(this decimal d)
        {
            return d.ToString("0.00####");
        }

        /// <summary>
        /// Returns string representation with invariant culture (English)
        /// </summary>
        public static string ToInvariantString(this decimal d)
        {
            return d.ToString(CultureInfo.InvariantCulture);
        }

        public static NumberFormatInfo CurrentFormatInfo
        {
            get { return CultureInfo.CurrentCulture.NumberFormat; }
        }

        public static int AccurateDecimals
        {
            get { return 6; }
        }

        public static int MinDecimalsAfterSeparator
        {
            get { return 2; }
        }
    }
}