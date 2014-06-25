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
    public static class HtmlTagExtensions
    {
        public static MvcHtmlString ToHtml(this HtmlTag tag)
        {
            return new MvcHtmlString(tag.ToString());
        }

        public static HtmlTag Value(this HtmlTag tag, object value)
        {
            return tag.Attr("value", value.ToString());
        }

        public static HtmlTag Type(this HtmlTag tag, string type)
        {
            return tag.Attr("type", type);
        }

        public static HtmlTag Type(this HtmlTag tag, InputType type)
        {
            return tag.Type(HtmlHelper.GetInputTypeString(type));
        }

        public static HtmlTag Checked(this HtmlTag tag, bool isChecked)
        {
            if (isChecked)
                tag.Attr("checked", "true");
            return tag;
        }

        public static HtmlTag NameFor<TModel, TProp>(
            this HtmlTag tag, 
            Expression<Func<TModel, TProp>> propExpression,
            HtmlHelper<TModel> htmlHelper)
        {
            var name = htmlHelper.NameFor(propExpression).ToString();
            return tag.Attr("name", name);
        }

        public static HtmlTag IdFor<TModel, TProp>(
            this HtmlTag tag,
            Expression<Func<TModel, TProp>> propExpression,
            HtmlHelper<TModel> htmlHelper)
        {
            var id = htmlHelper.IdFor(propExpression).ToString();
            return tag.Attr("id", id);
        }

        public static HtmlTag ValueFor<TModel, TProp>(
            this HtmlTag tag,
            Expression<Func<TModel, TProp>> propExpression,
            HtmlHelper<TModel> htmlHelper)
        {
            var value = htmlHelper.ValueFor(propExpression);
            return tag.Value(value);
        }

        public static HtmlTag ValueFor<TModel, TProp>(
            this HtmlTag tag,
            Expression<Func<TModel, TProp>> propExpression,
            string format,
            HtmlHelper<TModel> htmlHelper)
        {
            var value = htmlHelper.ValueFor(propExpression, format);
            return tag.Value(value);
        }

        public static HtmlTag ValidationFor<TModel, TProp>(
            this HtmlTag tag,
            Expression<Func<TModel, TProp>> propExpression,
            HtmlHelper<TModel> htmlHelper)
        {
            var name = htmlHelper.NameFor(propExpression).ToString();
            ModelMetadata metadata = ModelMetadata.FromLambdaExpression(propExpression, htmlHelper.ViewData);
            var rules = htmlHelper.GetUnobtrusiveValidationAttributes(name, metadata);
            return rules.Aggregate(tag, (current, rule) => current.Attr(rule.Key, rule.Value));
        }
    }
}
