using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using Bardock.Utils.Globalization;
using Bardock.Utils.Types;
using HtmlTags;

namespace Bardock.Utils.Web.Mvc.HtmlTags
{
    public static class OptionsList
    {
        public static OptionsList<TItem> Create<TItem>(
            IEnumerable<TItem> items,
            Func<TItem, string> display,
            Func<TItem, object> value,
            Func<TItem, bool> isSelected = null,
            Expression<Action<TItem, HtmlTag>> configure = null)
        {
            return new OptionsList<TItem>(items, display, value, isSelected, configure);
        }

        public static OptionsList<TItem> Create<TItem>(
            IEnumerable<TItem> items,
            Func<TItem, string> display,
            Func<TItem, object> value,
            object selectedValue,
            Expression<Action<TItem, HtmlTag>> configure = null)
        {
            return Create(
                items,
                display: display,
                value: value,
                isSelected: x => value(x) == null && selectedValue == null || value(x) != null && value(x).Equals(selectedValue),
                configure: configure);
        }

        public static OptionsList<TItem> Create<TItem>(
            IEnumerable<TItem> items,
            Func<TItem, string> display,
            Func<TItem, object> value,
            IEnumerable selectedValues,
            Expression<Action<TItem, HtmlTag>> configure = null)
        {
            return Create(
                items,
                display: display,
                value: value,
                isSelected: x => selectedValues != null && selectedValues.Cast<object>().Contains(value(x)),
                configure: configure);
        }

        public static OptionsList<SelectListItem> Create(
            IEnumerable<SelectListItem> items,
            Expression<Action<SelectListItem, HtmlTag>> configure = null)
        {
            return Create(
                items,
                display: x => x.Text,
                value: x => x.Value,
                isSelected: x => x.Selected,
                configure: configure);
        }

        public static OptionsList<EnumOption<TEnum, int>> CreateForEnum<TEnum>(
            Func<EnumOption<TEnum, int>, bool> isSelected = null,
            Func<EnumOption<TEnum, int>, string> display = null,
            Expression<Action<EnumOption<TEnum, int>, HtmlTag>> configure = null) where TEnum : struct, IConvertible
        {
            return Create(
                items: EnumType.Create<TEnum>(),
                display: display ?? (x => Resources.Current.GetValue(x.Name)),
                value: x => x.Value,
                isSelected: isSelected,
                configure: configure);
        }

        public static OptionsList<EnumOption<TEnum, int>> CreateForEnum<TEnum>(
            int? selectedValue,
            Func<EnumOption<TEnum, int>, string> display = null,
            Expression<Action<EnumOption<TEnum, int>, HtmlTag>> configure = null) where TEnum : struct, IConvertible
        {
            return CreateForEnum<TEnum>(
                isSelected: x => x.Value == selectedValue,
                display: display,
                configure: configure);
        }

        public static OptionsList<EnumOption<TEnum, int>> CreateForEnum<TEnum>(
            IEnumerable<int> selectedValues,
            Func<EnumOption<TEnum, int>, string> display = null,
            Expression<Action<EnumOption<TEnum, int>, HtmlTag>> configure = null) where TEnum : struct, IConvertible
        {
            return CreateForEnum<TEnum>(
                isSelected: x => selectedValues != null && selectedValues.Contains(x.Value),
                display: display,
                configure: configure);
        }
    }

    public class OptionsList<TItem>
    {
        public IEnumerable<TItem> Items { get; set; }
        public Func<TItem, string> Display { get; set; }
        public Func<TItem, object> Value { get; set; }
        public Func<TItem, bool> IsSelected { get; set; }
        public Expression<Action<TItem, HtmlTag>> Configure { get; set; }

        public OptionsList(
            IEnumerable<TItem> items,
            Func<TItem, string> display,
            Func<TItem, object> value,
            Func<TItem, bool> isSelected = null,
            Expression<Action<TItem, HtmlTag>> configure = null)
        {
            this.Items = items;
            this.Display = display;
            this.Value = value;
            this.IsSelected = isSelected;
            this.Configure = configure;
        }
    }
}
