using System.Runtime.CompilerServices;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Collections.Generic;
using System;

namespace Bardock.Utils.Web.Mvc.Extensions
{
    public static class HtmlHelperTabBuilderExtensions
    {
        public static T Attr<T>(this T tagBuilder, string key, string value) where T : TagBuilder
        {
            tagBuilder.Attributes[key] = value;
            return tagBuilder;
        }

        public static T Attrs<T>(this T tagBuilder, IDictionary<string, object> htmlAttributes) where T : TagBuilder
        {
            tagBuilder.MergeAttributes(htmlAttributes);
            return tagBuilder;
        }

        public static T Attrs<T>(this T tagBuilder, object htmlAttributes) where T : TagBuilder
        {
            return tagBuilder.Attrs(HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
        }

        public static T Data<T>(this T tagBuilder, string key, string value) where T : TagBuilder
        {
            return tagBuilder.Attr("data-" + key, value);
        }

        public static T Validation<T>(this T tagBuilder, string key, string value) where T : TagBuilder
        {
            return tagBuilder.Data("val-" + key, value);
        }

        public static TagBuilder TagBuilder(this HtmlHelper htmlHelper, string tagName)
        {
            return new TagBuilder(tagName);
        }

        public static InputBuilder InputBuilder(this HtmlHelper htmlHelper, string fieldName, InputType inputType = InputType.Text)
        {
            return new InputBuilder(fieldName, inputType, htmlHelper);
        }

        public static InputBuilder InputBuilderFor<TModel, TValue>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TValue>> expression, InputType inputType = InputType.Text)
        {
            return new HtmlTagBuilderFor<TModel, TValue>(expression, inputType, htmlHelper);
        }

        public static InputBuilder RemoteValidation(this InputBuilder tagBuilder, string errorMessage, string action, string controller = null, string httpMethod = "GET")
        {
            UrlHelper urlHelper = new UrlHelper(System.Web.HttpContext.Current.Request.RequestContext);
            return tagBuilder
                .Validation("remote", errorMessage)
                .Validation("remote-additionalfields", "*." + tagBuilder.FieldName)
                .Validation("remote-type", httpMethod)
                .Validation("remote-url", urlHelper.Action(action, controller));
        }

        public static MvcHtmlString ToMvcHtmlString(this InputBuilder tagBuilder)
        {
            return new MvcHtmlString(tagBuilder.ToString(TagRenderMode.SelfClosing));
        }
	}

    public class InputBuilder : TagBuilder
    {
        public string FieldName { get; private set; }

        public InputType InputType { get; private set; }

        public HtmlHelper HtmlHelper { get; private set; }

        public InputBuilder(
            string fieldName, 
            InputType inputType,
            HtmlHelper htmlHelper
        )
            : base("input")
        {
            this.FieldName = fieldName;
            this.InputType = inputType;
            this.HtmlHelper = htmlHelper;

            string fullName = htmlHelper != null
                                ? htmlHelper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(fieldName)
                                : fieldName;

            this.MergeAttribute("type", HtmlHelper.GetInputTypeString(inputType));
            this.MergeAttribute("name", fullName, true);
            
            // If there are any errors for a named field, we add the css attribute.
            ModelState modelState;
            if (htmlHelper.ViewData.ModelState.TryGetValue(fullName, out modelState))
            {
                if (modelState.Errors.Count > 0)
                {
                    this.AddCssClass(HtmlHelper.ValidationInputCssClassName);
                }
            }
        }
    }

    public class HtmlTagBuilderFor<TModel, TValue> : InputBuilder
    {
        public Expression<Func<TModel, TValue>> Expression { get; private set; }

        public HtmlTagBuilderFor( 
            Expression<Func<TModel, TValue>> expression,
            InputType inputType,
            HtmlHelper<TModel> htmlHelper
        )
            : base(ExpressionHelper.GetExpressionText(expression), inputType, htmlHelper)
        {
            this.Expression = expression;

            ModelMetadata metadata = ModelMetadata.FromLambdaExpression(this.Expression, htmlHelper.ViewData);
            this.MergeAttributes(htmlHelper.GetUnobtrusiveValidationAttributes(this.FieldName, metadata));
        }
    }
}