using System;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using HtmlTags;

namespace Bardock.Utils.Web.Mvc.HtmlTags.Extensions
{
    public static class HtmlTagExtensions
    {
        public static MvcHtmlString ToHtml(this HtmlTag tag)
        {
            return new MvcHtmlString(tag.ToString());
        }

        public static THtmlTag Attrib<THtmlTag>(this THtmlTag tag, string attribute, object value) where THtmlTag : HtmlTag
        {
            return (THtmlTag)tag.Attr(attribute, value);
        }

        public static string Val(this HtmlTag tag)
        {
            return tag.Attr("value");
        }

        public static THtmlTag Val<THtmlTag>(this THtmlTag tag, object value, string format = null) where THtmlTag : HtmlTag
        {
            return tag.Attrib("value", ValueSerializer.Serialize(value, format));
        }

        public static bool ValueIsEqual(this HtmlTag tag, object value, string format = null)
        {
            return tag.Val().Equals(ValueSerializer.Serialize(value, format));
        }

        public static THtmlTag Type<THtmlTag>(this THtmlTag tag, string type) where THtmlTag : HtmlTag
        {
            return tag.Attrib("type", type);
        }

        public static THtmlTag Type<THtmlTag>(this THtmlTag tag, InputType type) where THtmlTag : HtmlTag
        {
            return tag.Type(HtmlHelper.GetInputTypeString(type));
        }

        public static bool BoolAttr<THtmlTag>(this THtmlTag tag, string name) where THtmlTag : HtmlTag
        {
            return tag.HasAttr(name);
        }

        public static THtmlTag BoolAttr<THtmlTag>(this THtmlTag tag, string name, bool value) where THtmlTag : HtmlTag
        {
            return value
                ? (THtmlTag)tag.BooleanAttr(name)
                : (THtmlTag)tag.RemoveAttr(name);
        }

        public static bool Checked<THtmlTag>(this THtmlTag tag) where THtmlTag : HtmlTag
        {
            return tag.BoolAttr("checked");
        }

        public static THtmlTag Checked<THtmlTag>(this THtmlTag tag, bool value) where THtmlTag : HtmlTag
        {
            return tag.BoolAttr("checked", value);
        }

        public static bool Disabled<THtmlTag>(this THtmlTag tag) where THtmlTag : HtmlTag
        {
            return tag.BoolAttr("disabled");
        }

        public static THtmlTag Disabled<THtmlTag>(this THtmlTag tag, bool value) where THtmlTag : HtmlTag
        {
            return tag.BoolAttr("disabled", value);
        }

        public static bool ReadOnly<THtmlTag>(this THtmlTag tag) where THtmlTag : HtmlTag
        {
            return tag.BoolAttr("readonly");
        }

        public static THtmlTag ReadOnly<THtmlTag>(this THtmlTag tag, bool value) where THtmlTag : HtmlTag
        {
            return tag.BoolAttr("readonly", value);
        }

        public static bool Selected<THtmlTag>(this THtmlTag tag) where THtmlTag : HtmlTag
        {
            return tag.BoolAttr("selected");
        }

        public static THtmlTag Selected<THtmlTag>(this THtmlTag tag, bool value) where THtmlTag : HtmlTag
        {
            return tag.BoolAttr("selected", value);
        }

        public static THtmlTag Prepend<THtmlTag>(this THtmlTag parent, HtmlTag child) where THtmlTag : HtmlTag
        {
            parent.InsertFirst(child);
            return parent;
        }

        public static THtmlTag Label<THtmlTag>(this THtmlTag tag, string text) where THtmlTag : HtmlTag
        {
            return (THtmlTag)new HtmlTag("label").Append(tag).Text(text);
        }

        public static THtmlTag InitFor<THtmlTag>(
            this THtmlTag tag,
            string name,
            HtmlHelper htmlHelper) where THtmlTag : HtmlTag
        {
            return tag
                .Attrib("id", htmlHelper.Id(name))
                .Attrib("name", name)
                .ValueFor(name, htmlHelper);
        }

        public static THtmlTag InitFor<THtmlTag, TModel, TProp>(
            this THtmlTag tag,
            Expression<Func<TModel, TProp>> expression,
            HtmlHelper<TModel> htmlHelper) where THtmlTag : HtmlTag
        {
            return tag
                .IdFor(expression, htmlHelper)
                .NameFor(expression, htmlHelper)
                .ValueFor(expression, htmlHelper)
                .ValidationFor(expression, htmlHelper);
        }

        public static THtmlTag NameFor<THtmlTag, TModel, TProp>(
            this THtmlTag tag, 
            Expression<Func<TModel, TProp>> expression,
            HtmlHelper<TModel> htmlHelper) where THtmlTag : HtmlTag
        {
            var name = htmlHelper.NameFor(expression).ToString();
            return tag.Attrib("name", name);
        }

        public static THtmlTag IdFor<THtmlTag, TModel, TProp>(
            this THtmlTag tag,
            Expression<Func<TModel, TProp>> expression,
            HtmlHelper<TModel> htmlHelper) where THtmlTag : HtmlTag
        {
            var id = htmlHelper.IdFor(expression).ToString();
            return tag.Attrib("id", id);
        }

        public static THtmlTag ValueFor<THtmlTag, TModel, TProp>(
            this THtmlTag tag,
            Expression<Func<TModel, TProp>> expression,
            HtmlHelper<TModel> htmlHelper) where THtmlTag : HtmlTag
        {
            var value = htmlHelper.ValueFor(expression);
            return tag.Val(value);
        }

        public static THtmlTag ValueFor<THtmlTag, TModel, TProp>(
            this THtmlTag tag,
            Expression<Func<TModel, TProp>> expression,
            string format,
            HtmlHelper<TModel> htmlHelper) where THtmlTag : HtmlTag
        {
            var value = htmlHelper.ValueFor(expression, format);
            return tag.Val(value);
        }

        public static THtmlTag ValueFor<THtmlTag>(
            this THtmlTag tag,
            string name,
            HtmlHelper htmlHelper) where THtmlTag : HtmlTag
        {
            var value = htmlHelper.Value(name);
            return tag.Val(value);
        }

        public static THtmlTag ValueFor<THtmlTag>(
            this THtmlTag tag,
            string name,
            string format,
            HtmlHelper htmlHelper) where THtmlTag : HtmlTag
        {
            var value = htmlHelper.Value(name, format);
            return tag.Val(value);
        }

        public static THtmlTag ValidationFor<THtmlTag, TModel, TProp>(
            this THtmlTag tag,
            Expression<Func<TModel, TProp>> expression,
            HtmlHelper<TModel> htmlHelper) where THtmlTag : HtmlTag
        {
            var name = htmlHelper.NameFor(expression).ToString();
            ModelMetadata metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
            var rules = htmlHelper.GetUnobtrusiveValidationAttributes(name, metadata);
            return rules.Aggregate(tag, (current, rule) => current.Attrib(rule.Key, rule.Value));
        }
    }
}
