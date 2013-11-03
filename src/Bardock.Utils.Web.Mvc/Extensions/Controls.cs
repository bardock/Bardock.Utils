using System;
using System.Web;
using System.Web.Mvc;
using System.Runtime.CompilerServices;
using System.Linq.Expressions;

namespace Bardock.Utils.Web.Mvc.Extensions
{
	public static class Controls
	{
        //public static MvcHtmlString DatePicker(HtmlHelper htmlHelper, string name, System.DateTime? value = null, string inputClass = null, bool appendCalendarMode = true, bool disabled = false)
        //{
        //    return htmlHelper.Partial("~/Views/Shared/_DatePicker.vbhtml", new Models.Shared.DatePicker {
        //        InputClass = inputClass,
        //        AppendCalendarMode = appendCalendarMode,
        //        Disabled = disabled,
        //        //Build a custom textbox to avoid date format
        //        //TODO: Create a custom textbox helper
        //        TextBoxBuilder = (IDictionary<string, object> htmlAttributes) => { return new MvcHtmlString(string.Format("<input type=\"text\" name=\"{0}\" value=\"{1}\" {2}>", name, value.IfNotNull(x => x.DateFormat()), string.Join(" ", AttributeHelper.BuildAttrs(htmlAttributes, disabled: disabled).Select(x => string.Format("{0}=\"{1}\"", x.Key, x.Value))))); }
        //    });
        //}
        //public static MvcHtmlString DatePickerFor<TModel>(HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, System.DateTime?>> expression, System.DateTime? defaultValue = null, string inputClass = null, bool appendCalendarMode = true, bool disabled = false)
        //{
        //    System.DateTime? value = defaultValue;
        //    if ((htmlHelper.ViewData.Model != null)) {
        //        value = expression.Compile().Invoke(htmlHelper.ViewData.Model);
        //    }

        //    return htmlHelper.Partial("~/Views/Shared/_DatePicker.vbhtml", new Models.Shared.DatePicker {
        //        InputClass = inputClass,
        //        AppendCalendarMode = appendCalendarMode,
        //        Disabled = disabled,
        //        TextBoxBuilder = (IDictionary<string, object> htmlAttributes) =>
        //        {
        //            if ((expression.Body.NodeType == ExpressionType.Convert)) {
        //                return htmlHelper.TextBox2For(MyExpressionHelper.RemoveNullableConvert(expression), htmlAttributes: htmlAttributes, value: value.IfNotNull(x => x.DateFormat()), disabled: disabled);
        //            } else {
        //                return htmlHelper.TextBox2For(expression, htmlAttributes: htmlAttributes, value: value.IfNotNull(x => x.DateFormat()), disabled: disabled);
        //            }
        //        }
        //    });
        //}

        //[Extension()]
        //public MvcHtmlString Select2For<TModel, TValueProp, TTextProp>(HtmlHelper<TModel> htmlHelper, Expression<System.Func<TModel, TValueProp>> valueExpression, Expression<System.Func<TModel, TTextProp>> textExpression, string action, string controller, int items = 10, bool allowClear = true, string placeHolder = null, IDictionary<string, object> htmlAttributes = null, bool disabled = false,
        //string containerClass = "select2_content", string unconstrainedElement = null, string customFilterCallback = null)
        //{
        //    //Get text from expression
        //    string text = string.Empty;
        //    try {
        //        text = textExpression.Compile().Invoke(htmlHelper.ViewData.Model).ToString();
        //    } catch (Exception ex) {
        //        //Ignore
        //    }

        //    if ((disabled)) {
        //        return htmlHelper.TextBox2("-", text, disabled: true);

        //    } else {
        //        htmlAttributes = htmlAttributes ?? new Dictionary<string, object>();
        //        htmlAttributes("data-provide") = "select2";
        //        htmlAttributes("data-items") = items;
        //        htmlAttributes("data-select2-container-class") = containerClass;
        //        htmlAttributes("data-source-action") = action;
        //        htmlAttributes("data-source-controller") = controller;
        //        htmlAttributes("data-text") = text;

        //        if ((!string.IsNullOrWhiteSpace(placeHolder))) {
        //            htmlAttributes("data-placeholder") = placeHolder;
        //        }

        //        if ((allowClear)) {
        //            htmlAttributes("data-allow-clear") = allowClear;
        //        }

        //        if ((!string.IsNullOrWhiteSpace(unconstrainedElement))) {
        //            htmlAttributes("data-unconstrained-element") = unconstrainedElement;
        //        }

        //        if ((!string.IsNullOrWhiteSpace(customFilterCallback))) {
        //            htmlAttributes("data-custom-filter-callback") = customFilterCallback;
        //        }

        //        var hidden = htmlHelper.HiddenFor(valueExpression, htmlAttributes);
        //        var fakeContainer = "<div class=\"select2-container select2-fake\"><a class=\"select2-choice\"></a></div>";

        //        var htmlBuilder = new Text.StringBuilder();
        //        htmlBuilder.AppendLine(fakeContainer).AppendLine(hidden.ToString());
        //        return new MvcHtmlString(htmlBuilder.ToString());
        //    }

        //}

	}
}