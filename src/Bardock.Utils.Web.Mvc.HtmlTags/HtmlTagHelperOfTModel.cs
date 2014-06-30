using System;
using System.Linq.Expressions;
using System.Web.Mvc;
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

        public virtual SelectTag SelectFor<TProp>(
            Expression<Func<TModel, TProp>> expression)
        {
            return new SelectTag().InitFor(expression, _htmlHelper);
        }
    }
}
