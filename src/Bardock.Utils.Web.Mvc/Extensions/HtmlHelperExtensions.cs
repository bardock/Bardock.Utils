using System;
using System.Linq.Expressions;
using System.Web.Mvc;

namespace Bardock.Utils.Web.Mvc.Extensions
{
	public static class HtmlHelperExtensions
    {
        public static object GetModelStateRawValue(this HtmlHelper helper, string key)
        {
            ModelState modelState;
            if (helper.ViewContext.ViewData.ModelState.TryGetValue(key, out modelState))
            {
                if (modelState.Value != null)
                {
                    return modelState.Value.RawValue;
                }
            }
            return null;
        }

        internal static object GetAttemptedValueFor(this HtmlHelper helper, string name)
        {
            string fullName = helper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(name);
            return helper.GetModelStateRawValue(fullName);
        }

        internal static object GetAttemptedValueFor<TModel, TProp>(
            this HtmlHelper helper,
            Expression<Func<TModel, TProp>> expression)
        {
            var name = ExpressionHelper.GetExpressionText(expression);
            return helper.GetAttemptedValueFor(name);
        }

        public static object GetValueForModel(this HtmlHelper helper)
        {
            return helper.GetValueFor(string.Empty);
        }

        public static object GetValueFor(this HtmlHelper helper, string name)
        {
            var attemptedValue = helper.GetAttemptedValueFor(name);
            if(attemptedValue != null)
                return attemptedValue;
            
            if (string.IsNullOrEmpty(name))
            {
                // Use the current model
                ModelMetadata metadata = ModelMetadata.FromStringExpression(String.Empty, helper.ViewContext.ViewData);
                return metadata.Model;
            }

            return helper.ViewContext.ViewData.Eval(name);
        }

        public static object GetValueFor<TModel, TProp>(
            this HtmlHelper<TModel> helper,
            Expression<Func<TModel, TProp>> expression)
        {
            var attemptedValue = helper.GetAttemptedValueFor(expression);
            if (attemptedValue != null)
                return attemptedValue;

            ModelMetadata metadata = ModelMetadata.FromLambdaExpression(expression, helper.ViewData);
            return metadata.Model;
        }
	}
}