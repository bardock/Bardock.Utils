using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
