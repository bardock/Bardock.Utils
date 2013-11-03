using System;
using System.Linq.Expressions;
using System.Web.Mvc;

namespace Bardock.Utils.Web.Mvc.Helpers
{
	public class MyExpressionHelper
	{
		public static LambdaExpression RemoveConvert<TModel, TResult>(Expression<Func<TModel, TResult>> exp)
		{
			LambdaExpression lambdaExp = exp;
			while (lambdaExp.Body.NodeType == ExpressionType.Convert || lambdaExp.Body.NodeType == ExpressionType.ConvertChecked) {
				lambdaExp = Expression.Lambda(((UnaryExpression)lambdaExp.Body).Operand, lambdaExp.Parameters);
			}
			return lambdaExp;
		}

		public static LambdaExpression RemoveNullableConvert<TModel, TResult>(Expression<Func<TModel, TResult?>> exp) where TResult : struct
		{
			return Expression.Lambda(((UnaryExpression)exp.Body).Operand, exp.Parameters);
		}

        public static string GetExpressionText<TModel, TResult>(Expression<Func<TModel, TResult>> exp)
        {
            return ExpressionHelper.GetExpressionText(RemoveConvert(exp));
        }
	}
}