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
            bool selected = true,
            Action<HtmlTag> configure = null) where TSelectTag : SelectTag
        {
            tag.PrependOption(display, null, configure);
            if (selected)
                tag.SelectValue(null);
            return tag;
        }

        public static TSelectTag AddOption<TSelectTag, TItem>(
            this TSelectTag tag,
            TItem item,
            Func<TItem, string> display,
            Func<TItem, object> value,
            Func<TItem, bool> isSelected = null,
            Expression<Action<TItem, HtmlTag>> configure = null) where TSelectTag : SelectTag
        {
            var val = value(item);
            tag.AddOption(display(item), val, configure.PartialApply(item).Compile());

            if (isSelected != null && isSelected(item))
                tag.SelectValue(val);

            return tag;
        }

        public static TSelectTag AddOptions<TSelectTag, TItem>(
            this TSelectTag tag,
            IEnumerable<TItem> items,
            Func<TItem, string> display,
            Func<TItem, object> value,
            Func<TItem, bool> isSelected = null,
            Expression<Action<TItem, HtmlTag>> configure = null) where TSelectTag : SelectTag
        {
            foreach (var item in items)
            {
                tag.AddOption(item, display, value, isSelected, configure);
            }
            return tag;
        }

        public static TSelectTag AddOption<TSelectTag>(
            this TSelectTag tag,
            SelectListItem item,
            Expression<Action<SelectListItem, HtmlTag>> configure = null) where TSelectTag : SelectTag
        {
            return tag.AddOption(
                item, 
                display: x => x.Text, 
                value: x => x.Value,
                isSelected: x => x.Selected,
                configure: configure);
        }

        public static TSelectTag AddOptions<TSelectTag, TItem>(
            this TSelectTag tag,
            IEnumerable<SelectListItem> items,
            Expression<Action<SelectListItem, HtmlTag>> configure = null) where TSelectTag : SelectTag
        {
            foreach (var item in items)
            {
                tag.AddOption(item, configure);
            }
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
