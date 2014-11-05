using System;
using System.Linq.Expressions;
using System.Web.Mvc;

namespace Bardock.Utils.Web.Mvc.Extensions
{
	public static class HtmlHelperExtensions
    {
        public static TProp GetModelValueFor<TModel, TProp>(
            this HtmlHelper<TModel> helper,
            Expression<Func<TModel, TProp>> expression)
        {
            if (helper.ViewData.Model == null)
                return default(TProp);
            return expression.Compile().Invoke(helper.ViewData.Model);
        }

        public static object GetModelStateRawValue(this HtmlHelper helper, string key, Type destinationType = null)
        {
            ModelState modelState;
            if (helper.ViewContext.ViewData.ModelState.TryGetValue(key, out modelState))
            {
                if (modelState.Value != null)
                {
                    if (destinationType != null)
                        return modelState.Value.ConvertTo(destinationType, culture: null);
                    else
                        return modelState.Value.AttemptedValue;
                }
            }
            return null;
        }

        internal static object GetAttemptedValueFor(this HtmlHelper helper, string name, Type destinationType = null)
        {
            string fullName = helper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(name);
            return helper.GetModelStateRawValue(fullName, destinationType);
        }

        internal static object GetAttemptedValueFor<TModel, TProp>(
            this HtmlHelper helper,
            Expression<Func<TModel, TProp>> expression)
        {
            var name = ExpressionHelper.GetExpressionText(expression);
            return helper.GetAttemptedValueFor(name, expression.ReturnType);
        }

        public static object GetValueForModel(this HtmlHelper helper)
        {
            return helper.GetValueFor(string.Empty);
        }

        public static object GetValueFor(this HtmlHelper helper, string name, Type destinationType = null)
        {
            var attemptedValue = helper.GetAttemptedValueFor(name, destinationType);
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
            ModelMetadata metadata = ModelMetadata.FromLambdaExpression(expression, helper.ViewData);
            return metadata.Model;
        }
	}
}