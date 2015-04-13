using System.Globalization;

namespace Bardock.Utils.Extensions
{
    public static class DoubleExtensions
    {
        public static string ToInvariantString(this double d)
        {
            return d.ToString(CultureInfo.InvariantCulture);
        }

        public static string ToDecimalString(this double @this)
        {
            return @this.ToString("0,0.00", System.Globalization.CultureInfo.CurrentCulture.NumberFormat);
        }
    }
}