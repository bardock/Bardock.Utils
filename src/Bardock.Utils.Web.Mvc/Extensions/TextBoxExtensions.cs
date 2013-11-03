using Bardock.Utils.Web.Mvc.Helpers;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Web.Mvc.Html;
using System.Web.Mvc;

namespace Bardock.Utils.Web.Mvc.Extensions
{
	public static class TextBoxExtensions
	{
        public static MvcHtmlString TextBox2(
            this HtmlHelper htmlHelper, 
            string name, 
            object value = null, 
            IDictionary<string, object> htmlAttributes = null, 
            bool disabled = false)
		{
			return htmlHelper.TextBox(
                name, 
                value, 
                HtmlAttributeHelper.BuildAttrs(htmlAttributes, disabled: disabled));
		}

        public static MvcHtmlString TextBox2For<TModel, TProperty>(
            this HtmlHelper<TModel> htmlHelper, 
            Expression<System.Func<TModel, TProperty>> expression, 
            IDictionary<string, object> htmlAttributes = null, 
            object value = null, 
            bool disabled = false)
		{
			return htmlHelper.TextBoxFor(
                expression, 
                HtmlAttributeHelper.BuildAttrs(htmlAttributes, value: value, disabled: disabled));
		}

	}

}