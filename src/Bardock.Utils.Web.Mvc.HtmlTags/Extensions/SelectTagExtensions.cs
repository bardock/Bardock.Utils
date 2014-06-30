using Bardock.Utils.Extensions;
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
    public static class SelectTagExtensions
    {
        public static HtmlTag BuildOption(string display, object value, Action<HtmlTag> configure = null)
        {
            var option = new HtmlTag("option").Text(display).Val(value);
            if (configure != null)
                configure(option);
            return option;
        }

        public static TSelectTag PrependOption<TSelectTag>(
            this TSelectTag tag,
            string display,
            object value,
            Action<HtmlTag> configure = null) where TSelectTag : SelectTag
        {
            var option = BuildOption(display, value, configure);
            return tag.Prepend(option);
        }

        public static TSelectTag AddOption<TSelectTag>(
            this TSelectTag tag,
            string display,
            object value,
            Action<HtmlTag> configure = null) where TSelectTag : SelectTag
        {
            var option = BuildOption(display, value, configure);
            return (TSelectTag)tag.Append(option);
        }

        public static TSelectTag AddDefaultOption<TSelectTag>(
            this TSelectTag tag,
            string display = "",
            Action<HtmlTag> configure = null) where TSelectTag : SelectTag
        {
            return tag.PrependOption(display, null, configure);
        }

        public static TSelectTag AddOptions<TSelectTag, TItem>(
            this TSelectTag tag,
            OptionsList<TItem> options) where TSelectTag : SelectTag
        {
            object selectedVal = null;
            foreach (var item in options.Items)
            {
                var val = options.Value(item);
                tag.AddOption(options.Display(item), val, options.Configure.PartialApply(item).Compile());

                if (options.IsSelected != null && options.IsSelected(item))
                    selectedVal = val;
            }
            if (selectedVal != null)
                tag.SelectValue(selectedVal);

            return tag;
        }

        public static TSelectTag SelectValue<TSelectTag>(
            this TSelectTag tag, 
            object value, 
            string format = null) where TSelectTag : SelectTag
        {
            tag.Children
                .Where(x => x.Selected())
                .ToList()
                .ForEach(x => x.Selected(false));

            var toSelect = tag.Children.FirstOrDefault(x => x.ValueIsEqual(value, format));
            if (toSelect != null)
                toSelect.Selected(true);

            return tag;
        }
    }
}
