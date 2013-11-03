using System;
using System.Linq.Expressions;

namespace Bardock.Utils.Linq.Expressions
{
	public class ExpressionHelper
	{
		public static string GetMemberName<T>(Expression<System.Func<T, object>> expression)
		{
			if (expression.Body is MemberExpression) {
				return ((MemberExpression)expression.Body).Member.Name;
			} else {
				var op = (((UnaryExpression)expression.Body).Operand);
				return ((MemberExpression)op).Member.Name;
			}
		}

		/// <summary>
		/// Builds an Expression Tree from an expression string with properties, for example: "Prop1.Prop2.Prop3"
		/// </summary>
		public static LambdaExpression ParseProperties<TSource>(string expressionString)
		{
			string[] properties = expressionString.Split(new char[]{'.'}, StringSplitOptions.RemoveEmptyEntries);

			ParameterExpression parameter = Expression.Parameter(typeof(TSource), "posting");

			Expression exp = parameter;
			foreach (var prop in properties) {
				exp = Expression.Property(exp, prop);
			}

            return Expression.Lambda(exp, parameter);
		}
	}
}