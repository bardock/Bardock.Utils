using System;
using System.Linq;
using System.Web.Mvc;

namespace Bardock.Utils.Web.Mvc.HtmlTags.Extensions
{
    public static class HtmlHelperExtensions
    {
        public static HtmlTagTModelHelper<TModel> Tag<TModel>(this HtmlHelper<TModel> helper)
        {
            return new HtmlTagTModelHelper<TModel>(helper);
        }
    }
}
