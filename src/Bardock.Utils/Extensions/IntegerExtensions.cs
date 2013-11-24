using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace Bardock.Utils.Extensions
{
    public static class IntegerExtensions
    {
        /// <summary>
        /// Returns string representation with thousand separators
        /// </summary>
        public static string Format(this int n)
        {
            return n.ToString("#,0");
        }

        /// <summary>
        /// Returns string representation with thousand separators
        /// </summary>
        public static string Format(this long n)
        {
            return n.ToString("#,0");
        }
    }
}
