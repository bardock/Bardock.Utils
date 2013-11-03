using System;
using System.Collections;
using System.Configuration;
using System.IO;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Bardock.Utils.Web.Mvc.Extensions
{
    public static class UrlHelperExtensions
	{

        public static Uri Base(this UrlHelper url)
		{
            return UriHelper.GetBaseUri();
		}

        public static Uri Base(this System.Web.Http.Routing.UrlHelper url)
		{
            return UriHelper.GetBaseUri();
		}

        public static Uri Absolute(this System.Web.Http.Routing.UrlHelper url)
		{
			return url.Absolute(string.Empty);
		}

        public static Uri Absolute(this System.Web.Mvc.UrlHelper url, string relative)
		{
            return UriHelper.Absolute(relative);
		}

        public static Uri Absolute(this System.Web.Mvc.UrlHelper url)
		{
			return url.Absolute(string.Empty);
		}

        public static Uri Absolute(this System.Web.Http.Routing.UrlHelper url, string relative)
		{
            return UriHelper.Absolute(relative);
		}

        public static string Api(this UrlHelper helper, string controllerName, string action = null, object values = null)
		{
			RouteValueDictionary routeValues = new RouteValueDictionary {
				{ "httproute", "" },
                { "controller", controllerName }
			};
			if (values != null) {
				foreach (var prop in values.GetType().GetProperties()) {
					routeValues.Add(prop.Name, prop.GetValue(values));
				}
			}
			if ((action != null)) {
				routeValues.Add("action", action);
				return helper.RouteUrl("DefaultApiAction", routeValues);
			} else {
				return helper.RouteUrl("DefaultApi", routeValues);
			}
		}

        public static Uri PreviousLocal(this UrlHelper urlHelper)
		{
			Uri urlRef = HttpContext.Current.Request.UrlReferrer;
            if (UriHelper.IsLocal(urlRef))
				return urlRef;
			else
				return null;
		}

        public static bool ComparePath(this UrlHelper urlHelper, Uri url1, Uri url2)
		{
			return string.Equals(url1.AbsolutePath, url2.AbsolutePath, StringComparison.InvariantCultureIgnoreCase);
		}

        public static bool ComparePath(this UrlHelper urlHelper, Uri url1, string url2)
		{
			return urlHelper.ComparePath(url1, new Uri(url2));
		}

        public static bool ComparePath(this UrlHelper urlHelper, string url1, string url2)
		{
			return urlHelper.ComparePath(new Uri(url1), url2);
		}

        /// <summary>
        /// Returns relative base url ("/[AppVirtualPath]")
        /// </summary>
        public static string Content(this UrlHelper helper)
        {
            return helper.Content("~");
        }

        /// <summary>
        /// Generates a fully qualified URL to an action method. Supports IEnumerable as a route value.
        /// </summary>
        public static string Action2(this UrlHelper helper, string actionName, string controller)
        {
            return helper.Action(actionName, controller);
        }

        /// <summary>
        /// Generates a fully qualified URL to an action method. Supports IEnumerable as a route value.
        /// </summary>
        public static string Action2(this UrlHelper helper, string actionName, string controller, object routeValues)
        {
            return helper.Action(actionName, controller, UriHelper.ConvertRouteValueCollections(routeValues));
        }

		/// <summary>
        /// Generates a fully qualified URL to an action method. Supports IEnumerable as a route value.
		/// </summary>
        public static string Action2(this UrlHelper helper, string actionName, object routeValues)
        {
            return helper.Action(actionName, UriHelper.ConvertRouteValueCollections(routeValues));
		}

        /// <summary>
        /// Generates a fully qualified URL to an action method.
        /// </summary>
        public static string Action2(this UrlHelper helper, string actionName)
        {
            return helper.Action(actionName);
        }

        public static string ActionLiteralID(this UrlHelper helper, string actionName, string controllerName, string literalId)
		{
			return string.Format("{0}/{1}", helper.Action(actionName, controllerName, new { id = "" }), literalId);
		}

        public static string Protocoless(this UrlHelper helper, string url)
        {
            System.Uri uri = new Uri(url);
            return uri.Host + uri.PathAndQuery;
        }

        /// <summary>
        /// It prevents from inheriting route values from current request
        /// </summary>
        public static UrlHelper ToEmptyRouteData(this UrlHelper helper)
        {
            return new UrlHelper(new RequestContext(helper.RequestContext.HttpContext, new RouteData()));
        }
	}

}