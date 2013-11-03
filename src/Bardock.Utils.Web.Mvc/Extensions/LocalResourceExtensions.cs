using System.Runtime.CompilerServices;
using System.Web.Mvc;

namespace Bardock.Utils.Web.Mvc.Extensions
{
	public static class LocalResourceExtensions
	{
		public static string LocalResources(this WebViewPage page, string key)
		{
			return page.ViewContext.HttpContext.GetLocalResourceObject(page.VirtualPath, key) as string;
		}
	}
}