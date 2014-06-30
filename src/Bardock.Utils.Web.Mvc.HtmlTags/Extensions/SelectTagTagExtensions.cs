using Bardock.Utils.Web.Mvc.HtmlTags.Extensions;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Web.Mvc.Html;
using System.Web.Mvc;
using HtmlTags;
using System.Linq.Expressions;

namespace Bardock.Utils.Web.Mvc.HtmlTags.Extensions
{
    public static class SelectTagTagExtensions
    {
        public static HtmlTag BuildOption(string display, object value)
        {
            return new HtmlTag("option").Text(display).Value(value);
        }

        public static TSelectTag AddOption<TSelectTag>(
            this TSelectTag tag,
            string display, 
            object value) where TSelectTag : SelectTag
        {
            var option = BuildOption(display, value);
            return (TSelectTag)tag.Append(option);
        }
    }
}
