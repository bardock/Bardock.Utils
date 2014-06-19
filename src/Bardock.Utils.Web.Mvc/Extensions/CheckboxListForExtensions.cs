using Bardock.Utils.Web.Mvc.Helpers;
using bardock = Bardock.Utils.Linq.Expressions;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Web.Mvc;
using System.Collections.Generic;

namespace Bardock.Utils.Web.Mvc.Extensions
{
    public static class CheckboxListForExtensions
    {
        public static MvcHtmlString CheckBoxListFor<TModel, TValue>(
            this HtmlHelper<TModel> helper, 
            Expression<Func<TModel, TValue[]>> nameExpression, 
            IEnumerable<SelectListItem> list,
            Dictionary<string, Dictionary<string, object>> htmlAttributes = null)
        {
            var name = bardock.ExpressionHelper.GetExpressionText(nameExpression);
            var selectedValues = nameExpression.Compile().Invoke(helper.ViewData.Model);
            var selectedValueStrings = selectedValues == null ? null : selectedValues.Select(x => x.ToString()).ToArray();

            var output = new StringBuilder();
            output.Append("<div class=\"checkboxList\">");

            foreach (var i in list)
            {
                output.Append(
                    GetInputHtml(name, i.Value, i.Text, selectedValueStrings, htmlAttributes == null ? null : htmlAttributes[i.Value])
                );
            }

            output.Append("</div>");

            return new MvcHtmlString(output.ToString());
        }

        public static MvcHtmlString CheckBoxListFor<TModel, TEnum>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TEnum[]>> nameExpression, Dictionary<TEnum, string> labels = null) where TEnum : struct
        {
            if ((typeof(TEnum).IsEnum == false))
            {
                throw new ArgumentException("TEnum must be an enumerated type");
            }

            var name = bardock.ExpressionHelper.GetExpressionText(nameExpression);

            var items = GetSelectListFor(helper, nameExpression, labels);

            var output = new StringBuilder();
            output.Append("<div class=\"checkboxList\">");

            foreach (var item in items)
            {
                output.Append(GetInputHtml(name, item.Value, item.Text, item.Selected));
            }

            output.Append("</div>");

            return new MvcHtmlString(output.ToString());
        }

        public static IEnumerable<SelectListItem> GetSelectListFor<TModel, TEnum>(this HtmlHelper<TModel> helper, Expression<System.Func<TModel, TEnum[]>> nameExpression, Dictionary<TEnum, string> labels = null) where TEnum : struct
        {
            if ((typeof(TEnum).IsEnum == false))
            {
                throw new ArgumentException("TEnum must be an enumerated type");
            }

            var selectedValues = nameExpression.Compile().Invoke(helper.ViewData.Model);
            var selectedValuesInt = selectedValues == null ? null : selectedValues.Cast<int>().ToArray();

            foreach (var value in Enum.GetValues(typeof(TEnum)).Cast<int>().ToArray())
            {
                var enumObj = (TEnum)Enum.ToObject(typeof(TEnum), value);
                var label = labels != null && labels.ContainsKey(enumObj) ? labels[enumObj] : Bardock.Utils.Globalization.Resources.GetValue(enumObj.ToString());
                yield return new SelectListItem
                {
                    Value = enumObj.ToString(),
                    Text = label,
                    Selected = selectedValues != null && selectedValues.Contains(enumObj)
                };
            }
        }

        private static string GetInputHtml<T>(
            string name, 
            T value, 
            string label, 
            T[] selectedValues, 
            Dictionary<string, object> htmlAttributes = null)
        {
            return GetInputHtml(
                name, 
                value, 
                label, 
                selectedValues != null && selectedValues.Contains(value), 
                htmlAttributes);
        }

        private static string GetInputHtml<T>(
            string name, 
            T value, 
            string label, 
            bool selected, 
            Dictionary<string, object> htmlAttributes = null)
        {
            var inputTag = new TagBuilder("input")
                .Attr("type", "checkbox")
                .Attr("name", name)
                .Attr("value", value.ToString())
                .Attrs(htmlAttributes);

            if (selected)
            {
                inputTag.Attr("checked", "checked");
            }

            return string.Format(
                "<label>{0}{1}</label>",
                inputTag.ToString(),
                label
            );
        }
    }
}