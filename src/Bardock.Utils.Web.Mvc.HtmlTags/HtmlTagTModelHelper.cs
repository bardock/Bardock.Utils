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
            Expression<Func<TModel, TProp>> propExpression,
            string tag)
        {
            return new HtmlTag(tag)
                .IdFor(propExpression, _htmlHelper)
                .NameFor(propExpression, _htmlHelper)
                .ValueFor(propExpression, _htmlHelper)
                .ValidationFor(propExpression, _htmlHelper);
        }

        public virtual HtmlTag InputFor<TProp>(
            Expression<Func<TModel, TProp>> propExpression,
            string type)
        {
            return HtmlTagFor(propExpression, "input").Type(type);
        }

        public virtual HtmlTag InputFor<TProp>(
            Expression<Func<TModel, TProp>> propExpression,
            InputType type)
        {
            return InputFor(propExpression, System.Web.Mvc.HtmlHelper.GetInputTypeString(type));
        }

        public virtual HtmlTag TextAreaFor<TProp>(
            Expression<Func<TModel, TProp>> propExpression)
        {
            return HtmlTagFor(propExpression, "textarea");
        }

        public virtual HtmlTag TextBoxFor<TProp>(
            Expression<Func<TModel, TProp>> propExpression)
        {
            return InputFor(propExpression, InputType.Text);
        }

        public virtual HtmlTag PasswordFor<TProp>(
            Expression<Func<TModel, TProp>> propExpression)
        {
            return InputFor(propExpression, InputType.Password);
        }

        public virtual HtmlTag HiddenFor<TProp>(
            Expression<Func<TModel, TProp>> propExpression)
        {
            return InputFor(propExpression, InputType.Hidden);
        }

        public virtual HtmlTag RadioFor<TProp>(
            Expression<Func<TModel, TProp>> propExpression)
        {
            return InputFor(propExpression, InputType.Radio);
        }

        public virtual HtmlTag CheckBoxFor<TProp>(
            Expression<Func<TModel, TProp>> propExpression,
            bool isChecked = false)
        {
            var tag = InputFor(propExpression, InputType.CheckBox);
            if (isChecked)
                tag.Attr("checked", "true");
            return tag;
        }
    }
}
