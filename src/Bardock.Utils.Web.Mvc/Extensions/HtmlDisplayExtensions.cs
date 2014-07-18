using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using bardock = Bardock.Utils.Linq.Expressions;

namespace Bardock.Utils.Web.Mvc.Extensions
{
	public static class HtmlDisplayExtensions
	{
        public static string DisplayMemberName<TModel>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, object>> expression)
		{
			return Bardock.Utils.Linq.Expressions.ExpressionHelper.GetMemberName(expression);
		}

        public static string DisplayLiteral<TModel>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, object>> expression)
		{
            return bardock.ExpressionHelper.GetExpressionText(expression);
		}

        public static string DisplayForEnum<TEnum, TModel>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TEnum>> expression, string defaultName = null) where TEnum : struct
		{
			if (typeof(TEnum).IsEnum == false) 
            {
				throw new ArgumentException("enumType must be an enumerated type");
			}

			var namesMapping = Enum.GetValues(typeof(TEnum))
                .Cast<int>()
                .Select(x => (TEnum)Enum.ToObject(typeof(TEnum), x))
                .ToDictionary(x => x, x => Bardock.Utils.Globalization.Resources.Current.GetValue(x.ToString()));

			return helper.DisplayForEnum(expression, namesMapping, defaultName);
		}

        public static string DisplayForEnum<TModel, TEnum>(
            this HtmlHelper<TModel> helper, 
            Expression<Func<TModel, TEnum>> expression, 
            Dictionary<TEnum, string> namesMapping, 
            string defaultName = null) 
            where TEnum : struct
		{
			if (typeof(TEnum).IsEnum == false) 
            {
				throw new ArgumentException("Type must be an enumerated type");
			}

			if (helper.ViewData.Model != null) 
            {
				var value = expression.Compile().Invoke(helper.ViewData.Model);
				TEnum enumObj = (TEnum)Enum.ToObject(typeof(TEnum), value);
				if (namesMapping.ContainsKey(enumObj)) 
                {
					return namesMapping[enumObj];
				}
			}
			return defaultName;
		}

	}

}