using Newtonsoft.Json;
using System.Web.Mvc;

namespace Bardock.Utils.Web.Mvc.Extensions
{
	public static class JsonExtensions
	{
        public static MvcHtmlString Json<TModel>(this HtmlHelper<TModel> helper, object instance, bool camelCase = false)
		{
            var settings = new JsonSerializerSettings()
            {
                ContractResolver = camelCase
                                        ? new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver()
                                        : new Newtonsoft.Json.Serialization.DefaultContractResolver()
            };
			return new MvcHtmlString(JsonConvert.SerializeObject(instance, settings));
		}
	}
}