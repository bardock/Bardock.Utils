using System;
using System.Linq;
using System.Collections;
using System.Configuration;
using System.IO;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Routing;
using Bardock.Utils.Collections;

namespace Bardock.Utils.Web
{
    public static class UriHelper
	{
        private static HttpRequest Request
        {
			get { return HttpContext.Current.Request; }
		}

		/// <summary>
		/// Use default scheme's port if current request is remote
		/// Firewall port redirection workaround
		/// More info: http://stackoverflow.com/questions/7674850/get-original-url-without-non-standard-port-c
		/// </summary>
        private static int GetPort()
		{
			int port = Request.Url.Port;

			bool useDefaultPort = false;
            
			bool.TryParse(ConfigurationManager.AppSettings["mvc:UseDefaultPortForRemoteRequests"], out useDefaultPort);

			if (!Request.IsLocal && useDefaultPort)
				port = string.Equals(Request.Url.Scheme, "http", StringComparison.InvariantCultureIgnoreCase) ? 80 : 443;
            
			return port;
		}

        public static Uri GetBaseUri()
		{
			UriBuilder realmUri = new UriBuilder {
				Scheme = Request.Url.Scheme,
				Host = Request.Url.Host,
				Port = GetPort(),
				Path = Request.ApplicationPath,
				Query = null,
				Fragment = null
			};
			return realmUri.Uri;
		}

        public static Uri Absolute(
            string relative,
            bool removeAppPath = false, 
            bool escapeDataParts = false)
		{
            if (removeAppPath)
                relative = RemoveAppPath(relative);

            if (escapeDataParts)
                relative = EscapeDataParts(relative);

			return new Uri(Path.Combine(GetBaseUri().ToString(), relative.Trim('/')));
        }

        public static Uri Absolute(string relative)
        {
            return new Uri(UriHelper.GetBaseUri(), relative.Trim(Coll.Array('/')));
        }

        public static string EscapeDataParts(string path)
        {
            var parts = path.Split('/').Select(x => Uri.EscapeDataString(x)).ToArray();
            return string.Join("/", parts);
        }

        public static string RemoveAppPath(string relative)
		{
			return Regex.Replace(relative, "^/"+Request.ApplicationPath.Trim('/'), string.Empty);
		}

        public static bool IsLocal(Uri uri)
		{
			return uri != null && string.Equals(GetBaseUri().Host, uri.Host);
        }

        public static bool ComparePath(Uri url1, Uri url2)
        {
            return string.Equals(url1.AbsolutePath, url2.AbsolutePath, StringComparison.InvariantCultureIgnoreCase);
        }

        public static bool ComparePath(Uri url1, string url2)
        {
            return ComparePath(url1, new Uri(url2));
        }

        public static bool ComparePath(string url1, string url2)
        {
            return ComparePath(new Uri(url1), url2);
        }

		/// <summary>
		/// Returns request url.
		/// It uses default scheme's port if current request is remote.
		/// </summary>
        public static Uri AbsoluteUrl(this System.Web.HttpRequest request)
		{
			return Absolute(request.Url.PathAndQuery);
		}

        /// <summary>
        /// Builds a RouteValueDictionary supporting IEnumerables
        /// </summary>
        public static RouteValueDictionary ConvertRouteValueCollections(object routeValues)
        {
            var originalValues = new RouteValueDictionary(routeValues);
            var finalValues = new RouteValueDictionary();
            foreach (var key in originalValues.Keys)
            {
                var value = originalValues[key];
                if (value is IEnumerable && !(value is string))
                {
                    int i = 0;
                    foreach (object val in (IEnumerable)value)
                    {
                        finalValues[string.Format("{0}[{1}]", key, i)] = val;
                        i++;
                    }
                }
                else if (value is bool?)
                {
                    finalValues[key] = value ?? "null";
                }
                else
                {
                    finalValues[key] = value;
                }
            }
            return finalValues;
        }
	}

}