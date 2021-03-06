﻿using System;
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

        protected virtual LabeledTag BuildOption(
            string display, 
            object value, 
            bool isChecked = false,
            Action<LabeledTag> configure = null)
        {
            var option = new LabeledTag(
                display, 
                innerTag: new HtmlTag("input")
                    .Attr("name", this.Name)
                    .Type(InputType.CheckBox)
                    .Val(value)
                    .Checked(isChecked));

            if (configure != null)
                configure(option);

            return option;
        }

        public virtual IEnumerable<LabeledTag> Options
        {
            get { return this.Children.OfType<LabeledTag>(); }
        }

        public virtual IEnumerable<HtmlTag> Inputs
        {
            get { return this.Options.Select(label => label.InnerTag); }
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
            Action<LabeledTag> configure = null)
        {
            var option = BuildOption(display, value, isChecked, configure);
            return (CheckBoxListTag)this.Append(option);
        }

        public virtual CheckBoxListTag AddOptions<TItem>(
            OptionsList<TItem> options,
            IEnumerable defaultValues = null)
        {
            var anyIsChecked = false;
            foreach (var item in options)
            {
                anyIsChecked = anyIsChecked || item.IsSelected;
                this.AddOption(item.Display, item.Value, item.IsSelected, item.Configure);
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
