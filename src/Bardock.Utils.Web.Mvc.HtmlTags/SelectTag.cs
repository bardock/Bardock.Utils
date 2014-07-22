using System;
using System.Collections.Generic;
using System.Linq;
using Bardock.Utils.Web.Mvc.HtmlTags.Extensions;
using HtmlTags;

namespace Bardock.Utils.Web.Mvc.HtmlTags
{
    public class SelectTag : HtmlTag
    {
        public List<HtmlTag> Options { get; protected set; }
        public Dictionary<string, HtmlTag> OptionGroups { get; protected set; }

        public SelectTag()
            : base("select")
        {
            Options = new List<HtmlTag>();
            OptionGroups = new Dictionary<string, HtmlTag>();
        }

        protected virtual HtmlTag BuildOption(string display, object value, Action<HtmlTag> configure = null)
        {
            var option = new HtmlTag("option").Text(display).Val(value);
            if (configure != null)
                configure(option);
            return option;
        }

        protected virtual HtmlTag BuildOptionGroup(string display, Action<HtmlTag> configure = null)
        {
            var optgroup = new HtmlTag("optgroup").Attr("label", display);
            if (configure != null)
                configure(optgroup);
            return optgroup;
        }

        protected virtual SelectTag AddOption(
            Action<HtmlTag> onSingleOption,
            Action<HtmlTag, HtmlTag> onOptionGroup,
            string display,
            object value,
            Action<HtmlTag> configure = null,
            object groupBy = null)
        {
            var option = BuildOption(display, value, configure);
            this.Options.Add(option);

            if (groupBy == null)
            {
                onSingleOption(option);
                return this;
            }
            else
            {
                var groupByStr = groupBy.ToString();

                if (!this.OptionGroups.ContainsKey(groupByStr))
                {
                    this.OptionGroups[groupByStr] = BuildOptionGroup(groupByStr);
                    this.Append(this.OptionGroups[groupByStr]);
                }

                onOptionGroup(this.OptionGroups[groupByStr], option);
                return this;
            }
        }

        public virtual SelectTag PrependOption(
            string display,
            object value,
            Action<HtmlTag> configure = null,
            object groupBy = null)
        {
            return AddOption(
                onSingleOption: option => this.Prepend(option),
                onOptionGroup: (optgroup, option) => optgroup.Prepend(option),
                display: display,
                value: value,
                configure: configure,
                groupBy: groupBy);
        }

        public virtual SelectTag AddOption(
            string display,
            object value,
            Action<HtmlTag> configure = null,
            object groupBy = null)
        {
            return AddOption(
                onSingleOption: option => this.Append(option),
                onOptionGroup: (optgroup, option) => optgroup.Append(option),
                display: display,
                value: value,
                configure: configure,
                groupBy: groupBy);
        }

        public virtual SelectTag AddDefaultOption(
            string display = "",
            Action<HtmlTag> configure = null)
        {
            return this.PrependOption(display, null, configure);
        }

        public virtual SelectTag AddOptions<TItem>(
            OptionsList<TItem> options,
            object defaultValue = null)
        {
            object selectedVal = null;
            foreach (var item in options)
            {
                this.AddOption(item.Display, item.Value, item.Configure, item.GroupBy);

                if (options.IsSelected != null && item.IsSelected)
                    selectedVal = item.Value;
            }
            if (selectedVal == null)
                selectedVal = defaultValue;

            if (selectedVal != null)
                this.SelectValue(selectedVal);

            return this;
        }

        public virtual SelectTag SelectValue(
            object value,
            string format = null)
        {
            this.Options
                .Where(x => x.Selected())
                .ToList()
                .ForEach(x => x.Selected(false));

            this.Options
                .Where(x => x.ValueIsEqual(value, format)).ToList()
                .ForEach(x => x.Selected(true));

            return this;
        }
    }
}
