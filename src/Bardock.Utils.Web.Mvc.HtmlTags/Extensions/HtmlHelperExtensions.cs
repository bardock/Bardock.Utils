using System;
using System.Linq;
using System.Web.Mvc;

namespace Bardock.Utils.Web.Mvc.HtmlTags.Extensions
{
    public static class HtmlHelperExtensions
    {
        /// <summary>
        /// Creates a new HtmlTagHelper for rendering HTML controls in a view
        /// </summary>
        public static HtmlTagHelper Tags(this HtmlHelper helper)
        {
            return new HtmlTagHelper(helper);
        }

        /// <summary>
        /// Creates a new HtmlTagHelper for rendering HTML controls in a strongly typed view
        /// </summary>
        public static HtmlTagHelper<TModel> Tags<TModel>(this HtmlHelper<TModel> helper)
        {
            return new HtmlTagHelper<TModel>(helper);
        }
    }
}
