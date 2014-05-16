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
        private static Type _resourceType;

        public static void Register(Type resourceType)
        {
            _resourceType = resourceType;
        }

        /// <summary>
        /// Obtains resource value by its name.
        /// </summary>
        public static string GetValue(string resourceName)
        {
            var prop = _resourceType.GetProperty(resourceName);

            if (prop == null)
                return resourceName;

            return prop.GetValue(null).ToString();
        }
	}
}