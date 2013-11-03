using System;
using System.Web.Mvc;

namespace Bardock.Utils.Web.Mvc.Extensions
{
	public static class WebViewPageExtensions
	{
        public static bool IsController(this WebViewPage view, string controllerName)
		{
			return string.Equals(view.ViewContext.RouteData.Values["controller"].ToString(), controllerName, StringComparison.InvariantCultureIgnoreCase);
		}
	}
}