using System;
using System.Globalization;

namespace Bardock.Utils.Web.Mvc.HtmlTags
{
    public class ValueSerializer
    {
        public static string Serialize(object value, string format = null)
        {
            if (value == null)
            {
                return string.Empty;
            }
            if (String.IsNullOrEmpty(format))
            {
                return Convert.ToString(value, CultureInfo.CurrentCulture);
            }
            else
            {
                return String.Format(CultureInfo.CurrentCulture, format, value);
            }
        }
    }
}
