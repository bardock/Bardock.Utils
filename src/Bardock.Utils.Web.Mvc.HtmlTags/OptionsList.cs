using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using Bardock.Utils.Globalization;
using Bardock.Utils.Types;
using Bardock.Utils.Extensions;
using HtmlTags;

namespace Bardock.Utils.Web.Mvc.HtmlTags
{
    public static class OptionsList
    {
        public static OptionsList<TItem> Create<TItem>(
            IEnumerable<TItem> items,
            Func<TItem, string> display,
            Func<TItem, object> value)
        {
            return new OptionsList<TItem>(items, display, value);
        }

        public static OptionsList<SelectListItem> Create(IEnumerable<SelectListItem> items)
        {
            return Create(
                    items,
                    display: x => x.Text,
                    value: x => x.Value)
                .IsSelected(x => x.Selected);
        }

        public static OptionsList<EnumOption<TEnum, int>> CreateForEnum<TEnum>(
            Func<EnumOption<TEnum, int>, string> display = null) where TEnum : struct, IConvertible
        {
            return Create(
                items: EnumType.Create<TEnum>(),
                display: display ?? (x => Resources.Current.GetValue(x.Name)),
                value: x => x.Value);
        }
    }

    public class OptionsList<TItem> : IEnumerable<OptionItem>
    {
        public IEnumerable<TItem> Items { get; protected set; }

        public Func<TItem, string> Display { get; protected set; }

        public Func<TItem, object> Value { get; protected set; }

        protected List<OptionItem> _options = null;

        protected Func<TItem, bool> _isSelected;

        public Func<TItem, bool> IsSelected()
        {
            return this._isSelected;
        }

        public OptionsList<TItem> IsSelected(Func<TItem, bool> value)
        {
            this._isSelected = value;
            this.Reset();
            return this;
        }

        public OptionsList<TItem> SelectedItem(TItem item)
        {
            return this.SelectedValue(this.Value(item));
        }

        public OptionsList<TItem> SelectedValue(object value)
        {
            return this.IsSelected(
                x => ValueSerializer.Serialize(value) == ValueSerializer.Serialize(this.Value(x)));
        }

        public OptionsList<TItem> SelectedValues(IEnumerable values)
        {
            if(values == null)
            {
                return this.IsSelected(x => false);
            }
            else
            {
                var objValues = values.Cast<object>();
                return this.IsSelected(
                    x => objValues.Any(value => ValueSerializer.Serialize(value) == ValueSerializer.Serialize(this.Value(x))));
            }
        }

        protected Expression<Action<TItem, HtmlTag>> _configure;

        public Expression<Action<TItem, HtmlTag>> Configure()
        {
            return this._configure;
        }

        public OptionsList<TItem> Configure(Expression<Action<TItem, HtmlTag>> value)
        {
            this._configure = value;
            this.Reset();
            return this;
        }

        protected Func<TItem, object> _groupedBy;

        public Func<TItem, object> GroupedBy()
        {
            return this._groupedBy;
        }

        public OptionsList<TItem> GroupedBy(Func<TItem, object> value)
        {
            this._groupedBy = value;
            this.Reset();
            return this;
        }

        public OptionsList(
            IEnumerable<TItem> items,
            Func<TItem, string> display,
            Func<TItem, object> value,
            Func<TItem, bool> isSelected = null,
            Expression<Action<TItem, HtmlTag>> configure = null,
            Func<TItem, object> groupedBy = null)
        {
            if (items == null)
                throw new ArgumentException("items cannot be null");
            if (display == null)
                throw new ArgumentException("display cannot be null");
            if (value == null)
                throw new ArgumentException("value cannot be null");

            this.Items = items;
            this.Display = display;
            this.Value = value;
            this.IsSelected(isSelected);
            this.Configure(configure);
            this.GroupedBy(groupedBy);
        }

        protected void Reset()
        {
            this._options = null;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }


        public IEnumerator<OptionItem> GetEnumerator()
        {
            if (_options == null)
            {
                _options = this.Items
                    .Select(item => new OptionItem()
                    {
                        Display = this.Display(item),
                        Value = this.Value(item),
                        IsSelected = this.IsSelected() == null ? false : this.IsSelected()(item),
                        Configure = this.Configure() == null ? null : this.Configure().PartialApply(item).Compile(),
                        GroupBy = this.GroupedBy() == null ? null : this.GroupedBy()(item),
                    })
                    .ToList();
            }
            return _options.GetEnumerator();
        }
    }

    public class OptionItem
    {
        public string Display { get; set; }
        public object Value { get; set; }
        public bool IsSelected { get; set; }
        public Action<HtmlTag> Configure { get; set; }
        public object GroupBy { get; set; }
    }
}
