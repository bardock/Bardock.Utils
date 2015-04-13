using System.Linq.Expressions;
using System.Reflection;

namespace Bardock.Utils.Extensions
{
    public static class MemberExpressionExtensions
    {
        public static MemberExpression ToExpression(this PropertyInfo prop, string parameterName = "x")
        {
            ParameterExpression parameter = Expression.Parameter(prop.DeclaringType, "x");
            return Expression.Property(parameter, prop);
        }

        public static LambdaExpression ToLambdaExpression(this PropertyInfo prop, string parameterName = "x")
        {
            var expr = prop.ToExpression(parameterName: parameterName);
            return Expression.Lambda(expr, (ParameterExpression)expr.Expression);
        }
    }
}