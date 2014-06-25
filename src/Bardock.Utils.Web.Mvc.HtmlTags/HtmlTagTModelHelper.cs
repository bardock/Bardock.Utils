using Bardock.Utils.Web.Mvc.HtmlTags.Extensions;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Web.Mvc.Html;
using System.Web.Mvc;
using HtmlTags;
using System.Linq.Expressions;

namespace Bardock.Utils.Web.Mvc.HtmlTags
{
    public class HtmlTagTModelHelper<TModel>
    {
        private HtmlHelper<TModel> _htmlHelper;

        public HtmlHelper<TModel> HtmlHelper { get { return _htmlHelper; } }

        public HtmlTagTModelHelper()
            : this(HtmlHelperFactory.CreateInstance<TModel>())
        { }

        public HtmlTagTModelHelper(TModel model)
            : this(HtmlHelperFactory.CreateInstance<TModel>(model))
        { }

        public HtmlTagTModelHelper(HtmlHelper<TModel> htmlHelper) 
        {
            this._htmlHelper = htmlHelper;
        }

        public virtual HtmlTag HtmlTagFor<TProp>(
            Expression<Func<TModel, TProp>> expression,
            string tag)
        {
            return new HtmlTag(tag)
                .IdFor(expression, _htmlHelper)
                .NameFor(expression, _htmlHelper)
                .ValueFor(expression, _htmlHelper)
                .ValidationFor(expression, _htmlHelper);
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
    }
}
