using Newtonsoft.Json;
using System.Web.Mvc;

namespace Bardock.Utils.Web.Mvc.Extensions
{
	public static class JsonExtensions
	{
        public static MvcHtmlString Json<TModel>(this HtmlHelper<TModel> helper, object instance)
		{
			return new MvcHtmlString(JsonConvert.SerializeObject(instance));
		}
	}
}