using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using Bardock.Utils.Web.Mvc.Extensions;
using Bardock.Utils.Web.Mvc.Helpers;
using Bardock.Utils.Web.Mvc.HtmlTags.Extensions;
using HtmlTags;

namespace Bardock.Utils.Web.Mvc.HtmlTags
{
    /// <summary>
    /// Represents support for rendering HTML controls in a strongly typed view
    /// </summary>
    public class HtmlTagHelper<TModel> : HtmlTagHelper
    {
        protected new HtmlHelper<TModel> _htmlHelper;

        public new virtual HtmlHelper<TModel> HtmlHelper { get { return _htmlHelper; } }

        public HtmlTagHelper()
            : this(HtmlHelperFactory.CreateInstance<TModel>())
        { }

        public HtmlTagHelper(TModel model)
            : this(HtmlHelperFactory.CreateInstance<TModel>(model))
        { }

        public HtmlTagHelper(HtmlHelper<TModel> htmlHelper) 
        {
            this._htmlHelper = htmlHelper;
        }

        public virtual HtmlTag HtmlTagFor<TProp>(
            Expression<Func<TModel, TProp>> expression,
            string tag)
        {
            return new HtmlTag(tag).InitFor(expression, _htmlHelper);
        }

        public virtual HtmlTag InputFor<TProp>(
            Expression<Func<TModel, TProp>> expression,
            string type)
        {
            return HtmlTagFor(expression, "input").Type(type);
        }

        public virtual HtmlTag InputFor<TProp>(
            Expression<Func<TModel, TProp>> expression,
            InputType type)
        {
            return InputFor(expression, System.Web.Mvc.HtmlHelper.GetInputTypeString(type));
        }

        public virtual HtmlTag TextAreaFor<TProp>(
            Expression<Func<TModel, TProp>> expression)
        {
            return HtmlTagFor(expression, "textarea");
        }

        public virtual HtmlTag TextBoxFor<TProp>(
            Expression<Func<TModel, TProp>> expression)
        {
            return InputFor(expression, InputType.Text);
        }

        public virtual HtmlTag PasswordFor<TProp>(
            Expression<Func<TModel, TProp>> expression)
        {
            return InputFor(expression, InputType.Password);
        }

        public virtual HtmlTag HiddenFor<TProp>(
            Expression<Func<TModel, TProp>> expression)
        {
            return InputFor(expression, InputType.Hidden);
        }

        public virtual HtmlTag FileFor<TProp>(
            Expression<Func<TModel, TProp>> expression)
        {
            return InputFor(expression, "file").Val(null);
        }

        public virtual HtmlTag RadioFor<TProp>(
            Expression<Func<TModel, TProp>> expression,
            bool isChecked = false)
        {
            return InputFor(expression, InputType.Radio).Checked(isChecked);
        }

        public virtual HtmlTag CheckBoxFor<TProp>(
            Expression<Func<TModel, TProp>> expression,
            bool isChecked = false)
        {
            return InputFor(expression, InputType.CheckBox).Checked(isChecked);
        }

        protected virtual HtmlTag CheckBoxBoolFor<TProp>(
            Expression<Func<TModel, TProp>> expression,
            bool isChecked = false)
        {
            return CheckBoxFor(expression, isChecked: isChecked).Val("true");
        }

        public virtual HtmlTag CheckBoxFor(
            Expression<Func<TModel, bool>> expression)
        {
            var isChecked = this._htmlHelper.GetModelValueFor(expression);
            return CheckBoxBoolFor(expression, isChecked: isChecked);
        }

        public virtual HtmlTag CheckBoxFor(
            Expression<Func<TModel, bool?>> expression)
        {
            var isChecked = this._htmlHelper.GetModelValueFor(expression) ?? false;
            return CheckBoxBoolFor(expression, isChecked: isChecked);
        }

        public virtual SelectTag SelectFor<TProp>(
            Expression<Func<TModel, TProp>> expression)
        {
            return (SelectTag)new SelectTag()
                .InitFor(expression, _htmlHelper)
                .RemoveAttr("data-val-number")
                .RemoveAttr("value");
        }

        public virtual SelectTag SelectFor<TProp, TItem>(
            Expression<Func<TModel, TProp>> expression,
            OptionsList<TItem> options,
            object defaultValue = null)
        {
            var value = _htmlHelper.GetValueFor(expression);
            if (options.IsSelected() == null && value != null)
                options.SelectedValue(value);

            return this.SelectFor(expression)
                .AddOptions(options, defaultValue);
        }
        
        public virtual CheckBoxListTag CheckBoxListFor<TProp>(
            Expression<Func<TModel, TProp>> expression,
            string cssClass = CheckBoxListTag.DEFAULT_CSS_CLASS)
        {
            var name = _htmlHelper.NameFor(expression).ToString();
            return new CheckBoxListTag(name, cssClass);
        }

        public virtual CheckBoxListTag CheckBoxListFor<TPropItem, TItem>(
            Expression<Func<TModel, IEnumerable<TPropItem>>> expression,
            OptionsList<TItem> options,
            IEnumerable defaultValues = null,
            string cssClass = CheckBoxListTag.DEFAULT_CSS_CLASS)
        {
            var values = (IEnumerable)_htmlHelper.GetValueFor(expression);
            if (options.IsSelected() == null && values != null)
                options.SelectedValues(values);

            return this.CheckBoxListFor(expression, cssClass)
                .AddOptions(options, defaultValues);
        }
    }
}
