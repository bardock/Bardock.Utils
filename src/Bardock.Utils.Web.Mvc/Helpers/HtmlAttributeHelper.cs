using System.Collections.Generic;

namespace Bardock.Utils.Web.Mvc.Helpers
{
	public static class HtmlAttributeHelper
	{
        public static IDictionary<string, object> BuildAttrs(IDictionary<string, object> htmlAttributes = null, object value = null, bool disabled = false)
		{
			htmlAttributes = htmlAttributes ?? new Dictionary<string, object>();

			if ((value != null)) {
				htmlAttributes["Value"] = value;
			}

			if ((disabled)) {
				htmlAttributes["disabled"] = "disabled";
				if ((htmlAttributes.ContainsKey("class"))) {
                    htmlAttributes["class"] = string.Format("{0} {1}", htmlAttributes["class"], "disabled");
				} else {
                    htmlAttributes["class"] = "disabled";
				}
			} else if ((htmlAttributes.ContainsKey("disabled"))) {
				htmlAttributes.Remove("disabled");
			}
			return htmlAttributes;
		}
	}
}