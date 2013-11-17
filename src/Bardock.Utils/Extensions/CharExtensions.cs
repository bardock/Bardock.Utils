using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bardock.Utils.Extensions
{
    public static class CharExtensions
    {
        public static char ToLower(this char c)
        {
            if (c < 'A' || c > 'Z')
                return c;
            return (char)(c | 32);
        }
    }
}
