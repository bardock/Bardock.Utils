using System.IO;
using System.Runtime.CompilerServices;
using System.Web;
using System.Web.Routing;

namespace Bardock.Utils.Web.Extensions
{
    public static class RouteCollectionExtensions
	{
        /// <summary>
        /// Parses specified url and returns a RouteData instance. 
        /// Then, for example, you can do: routeData.Values["controller"]
        /// </summary>
        public static RouteData GetRouteData(this RouteCollection routeCollection, string url, string queryString = null)
		{
            var request = new HttpRequest(null, url, queryString);
            var response = new HttpResponse(new StringWriter());
            var httpContext = new HttpContext(request, response);
            return RouteTable.Routes.GetRouteData(new HttpContextWrapper(httpContext));
		}

	}

}