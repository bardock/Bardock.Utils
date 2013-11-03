using Bardock.Utils.Web.Mvc.Helpers;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Web.Mvc;

namespace Bardock.Utils.Web.Mvc.Extensions
{
	public static class CheckboxListForExtensions
	{
		public static MvcHtmlString CheckBoxListFor<TModel, TEnum>(this HtmlHelper helper, Expression<System.Func<TModel, TEnum[]>> nameExpression) where TEnum : struct
		{
			if ((typeof(TEnum).IsEnum == false)) {
				throw new ArgumentException("TEnum must be an enumerated type");
			}

			var name = MyExpressionHelper.GetExpressionText(nameExpression);
			var selectedValues = nameExpression.Compile().Invoke((TModel)helper.ViewData.Model);

			var output = new StringBuilder();
			output.Append("<div class=\"checkboxList\">");

			foreach (var value in Enum.GetValues(typeof(TEnum)).Cast<int>().ToArray()) {
                var enumObj = (TEnum)Enum.ToObject(typeof(TEnum), value);
				output.Append(
                    string.Format("<label><input type=\"checkbox\" name=\"{0}\" value=\"{1}\"{2}>{3}</label>", 
                        name, 
                        value, 
                        selectedValues != null && selectedValues.Contains(enumObj) 
                            ? " checked=\"checked\"" 
                            : "", 
                            Bardock.Utils.Globalization.Resources.GetValue(enumObj.ToString())
                    )
                );
			}

			output.Append("</div>");

			return new MvcHtmlString(output.ToString());
		}
	}
}