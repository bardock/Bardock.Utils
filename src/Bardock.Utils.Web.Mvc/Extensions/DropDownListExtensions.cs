using System.Runtime.CompilerServices;
using System.Linq.Expressions;
using System.Linq;
using System.Web.Mvc.Html;
using System.Web.Mvc;
using System.Collections.Generic;
using System;
using Bardock.Utils.Globalization;
using Bardock.Utils.Web.Mvc.Helpers;
using bardock = Bardock.Utils.Linq.Expressions;

namespace Bardock.Utils.Web.Mvc.Extensions
{
	public static class DropDownListExtensions
	{
		public class EnumOption
		{
			public string Name { get; set; }
            public int Value { get; set; }

			public static IEnumerable<EnumOption> BuildList(Type enumType)
			{
				return Enum.GetValues(enumType).Cast<int>().Select(x => new EnumOption {
                    Name = Resources.Current.GetValue(Enum.ToObject(enumType, x).ToString()),
					Value = x
				});
			}
		}

        public static MvcHtmlString DropDownList2For<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, IEnumerable<SelectListItem> selectList, string optionLabel = null, IDictionary<string, object> htmlAttributes = null, bool disabled = false)
		{
			return htmlHelper.DropDownListFor(expression, selectList, optionLabel, HtmlAttributeHelper.BuildAttrs(htmlAttributes, disabled: disabled));
		}

        public static MvcHtmlString DropDownListForEnum<TModel>(this HtmlHelper<TModel> helper, Expression<System.Func<TModel, int?>> nameExpression, Type enumType, string defaultLabel = null, int? defaultValue = null, IDictionary<string, object> htmlAttributes = null, bool disabled = false)
		{
			if ((enumType.IsEnum == false)) {
				throw new ArgumentException("enumType must be an enumerated type");
			}

            var name = bardock.ExpressionHelper.GetExpressionText(nameExpression);
            var options = EnumOption.BuildList(enumType);

			int? selectedValue = defaultValue;
			if ((helper.ViewData.Model != null)) {
				var modelValue = nameExpression.Compile().Invoke(helper.ViewData.Model);
				if ((modelValue != null && options.Any(x => x.Value == modelValue))) {
					selectedValue = modelValue;
				}
			}

			var selectList = Bardock.Utils.Web.Mvc.Helpers.SelectListHelper.Create(options, x => x.Value, x => x.Name, selectedValue);

			return helper.DropDownList(name, selectList, defaultLabel, HtmlAttributeHelper.BuildAttrs(htmlAttributes, disabled: disabled));
		}
	}
}