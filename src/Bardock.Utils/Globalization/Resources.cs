using System;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Threading;

namespace Bardock.Utils.Globalization
{
    /// <summary>
    /// Provides a wrapper for a Resource instance.
    /// </summary>
    public class Resources
	{
        private static object _resouces;

        public static void Register(object r)
        {
            _resouces = r;
        }

        /// <summary>
        /// Obtains resource value by its name.
        /// </summary>
        public static string GetValue(string resourceName)
        {
            var prop = _resouces.GetType().GetProperty(resourceName);

            if (prop == null)
                return resourceName;

            return prop.GetValue(null).ToString();
        }
	}
}