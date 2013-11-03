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
        public static string ToInvariantString(this double @this)
        {
            return @this.ToString(CultureInfo.InvariantCulture);
        }
    }
}
