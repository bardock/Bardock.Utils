using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Bardock.Utils.Extensions;
using Bardock.Utils.Web.Mvc.HtmlTags.Extensions;
using HtmlTags;

namespace Bardock.Utils.Web.Mvc.HtmlTags
{
    public class CheckBoxListTag : HtmlTag
    {
        public const string DEFAULT_CSS_CLASS = "checkboxList";

        public string Name { get; protected set; }

        public CheckBoxListTag(string name, string cssClass = DEFAULT_CSS_CLASS)
            : base("div")
        {
            this.Name = name;

            if (!string.IsNullOrWhiteSpace(cssClass))
                this.AddClass(cssClass);
        }

        protected virtual HtmlTag BuildOption(
            string display, 
            object value, 
            bool isChecked = false,
            Action<HtmlTag> configure = null)
        {
            var option = new HtmlTag("input")
                .Attr("name", this.Name)
                .Type(InputType.CheckBox)
                .Val(value)
                .Checked(isChecked)
                .Label(display);

            if (configure != null)
                configure(option);

            return option;
        }

        public virtual IEnumerable<HtmlTag> Inputs
        {
            get { return this.Children.Select(label => label.FirstChild()); }
        }

        public virtual CheckBoxListTag PrependOption(
            string display,
            object value,
            bool isChecked = false,
            Action<HtmlTag> configure = null)
        {
            var option = BuildOption(display, value, isChecked, configure);
            return this.Prepend(option);
        }

        public virtual CheckBoxListTag AddOption(
            string display,
            object value,
            bool isChecked = false,
            Action<HtmlTag> configure = null)
        {
            var option = BuildOption(display, value, isChecked, configure);
            return (CheckBoxListTag)this.Append(option);
        }

        public virtual CheckBoxListTag AddOptions<TItem>(
            OptionsList<TItem> options,
            IEnumerable defaultValues = null)
        {
            var anyIsChecked = false;
            foreach (var item in options.Items)
            {
                var val = options.Value(item);
                var configure = options.Configure == null ? null : options.Configure.PartialApply(item).Compile();
                var isChecked = (options.IsSelected != null && options.IsSelected(item));
                anyIsChecked = anyIsChecked || isChecked;

                this.AddOption(options.Display(item), val, isChecked, configure);
            }
            if (!anyIsChecked && defaultValues != null)
                this.CheckValues(defaultValues);

            return this;
        }

        public virtual CheckBoxListTag CheckValue(
            object value,
            string format = null)
        {
            this.Inputs
                .Where(x => x.ValueIsEqual(value, format)).ToList()
                .ForEach(x => x.Checked(true));
            return this;
        }

        public virtual CheckBoxListTag CheckValues(
            IEnumerable values,
            string format = null)
        {
            foreach (var value in values)
            {
                this.CheckValue(value, format);
            }
            return this;
        }

        public virtual CheckBoxListTag CheckValues(
            params object[] values)
        {
            return this.CheckValues(values, format: null);
        }
    }
}
