using System;
using System.Linq;
using System.Web.Http.Routing;

namespace Bardock.Utils.Web.WebApi.Extensions
{
    public static class UrlHelperExtensions
	{
        public static Uri Base(this UrlHelper url)
		{
            return UriHelper.GetBaseUri();
		}

        public static Uri Absolute(this UrlHelper url)
		{
			return url.Absolute(string.Empty);
		}

        public static Uri Absolute(this UrlHelper url, string relative, bool escapeDataParts = false)
        {
            return UriHelper.Absolute(relative, escapeDataParts: escapeDataParts);
		}

        public static Uri Absolute(string relative, bool escapeDataParts = false)
        {
            return UriHelper.Absolute(relative, escapeDataParts: escapeDataParts);
        }
	}

}