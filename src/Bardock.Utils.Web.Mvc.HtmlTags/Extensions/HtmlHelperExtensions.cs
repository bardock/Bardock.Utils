using System;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using Bardock.Utils.Types;
using HtmlTags;

namespace Bardock.Utils.Web.Mvc.HtmlTags.Extensions
{
    public static class HtmlHelperExtensions
    {
        /// <summary>
        /// Creates a new HtmlTagHelper for rendering HTML controls in a view
        /// </summary>
        public static HtmlTagHelper Tags(this HtmlHelper helper)
        {
            return new HtmlTagHelper(helper);
        }

        /// <summary>
        /// Creates a new HtmlTagHelper for rendering HTML controls in a strongly typed view
        /// </summary>
        public static HtmlTagHelper<TModel> Tags<TModel>(this HtmlHelper<TModel> helper)
        {
            return new HtmlTagHelper<TModel>(helper);
        }


        public static OptionsList<EnumOption<TEnum, int>> GetOptionsListFor<TModel, TEnum>(
            this HtmlHelper<TModel> helper,
            Expression<System.Func<TModel, TEnum[]>> nameExpression,
            Func<EnumOption<TEnum, int>, string> display = null,
            Expression<Action<EnumOption<TEnum, int>, HtmlTag>> configure = null) where TEnum : struct, IConvertible
        {
            var selectedValues = nameExpression.Compile().Invoke(helper.ViewData.Model);
            var selectedValuesInt = selectedValues == null ? null : selectedValues.Cast<int>();
            return OptionsList.CreateForEnum<TEnum>(
                selectedValues: selectedValuesInt,
                display: display,
                configure: configure);
        }
    }
}
