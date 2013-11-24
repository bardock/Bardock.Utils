using System;
using System.Web;

namespace Bardock.Utils.Web.Extensions
{
    public static class HttpRequestExtensions
    {
        public static bool IsAjax(this HttpRequest request)
        {
            return (request["X-Requested-With"] == "XMLHttpRequest") 
                || ((request.Headers != null) && (request.Headers["X-Requested-With"] == "XMLHttpRequest"));
        }

        public static bool IsPost(this HttpRequest req)
        {
            return string.Equals(req.HttpMethod, "POST", StringComparison.InvariantCultureIgnoreCase);
        }

        public static bool IsGet(this HttpRequest req)
        {
            return string.Equals(req.HttpMethod, "GET", StringComparison.InvariantCultureIgnoreCase);
        }

        public static bool IsDelete(this HttpRequest req)
        {
            return string.Equals(req.HttpMethod, "DELETE", StringComparison.InvariantCultureIgnoreCase);
        }

        public static bool IsPut(this HttpRequest req)
        {
            return string.Equals(req.HttpMethod, "PUT", StringComparison.InvariantCultureIgnoreCase);
        }

        public static bool IsPost(this HttpRequestBase req)
        {
            return string.Equals(req.HttpMethod, "POST", StringComparison.InvariantCultureIgnoreCase);
        }

        public static bool IsGet(this HttpRequestBase req)
        {
            return string.Equals(req.HttpMethod, "GET", StringComparison.InvariantCultureIgnoreCase);
        }

        public static bool IsDelete(this HttpRequestBase req)
        {
            return string.Equals(req.HttpMethod, "DELETE", StringComparison.InvariantCultureIgnoreCase);
        }

        public static bool IsPut(this HttpRequestBase req)
        {
            return string.Equals(req.HttpMethod, "PUT", StringComparison.InvariantCultureIgnoreCase);
        }
	}

}