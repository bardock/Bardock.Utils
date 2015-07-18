using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Bardock.Utils.Extensions
{
    public static class MemberExpressionExtensions
    {
        public static MemberExpression ToExpression(this PropertyInfo prop, string parameterName = "x")
        {
            if (prop == null)
                throw new ArgumentException("prop is null");

            var parameter = Expression.Parameter(prop.DeclaringType, "x");
            return Expression.Property(parameter, prop);
        }

        [Obsolete("Please use propInfo.ToExpression().ToLambda()")]
        public static LambdaExpression ToLambdaExpression(this PropertyInfo prop, string parameterName = "x")
        {
            var expr = prop.ToExpression(parameterName: parameterName);
            return Expression.Lambda(expr, (ParameterExpression)expr.Expression);
        }

        public static MemberExpression ToExpression(this FieldInfo field, string parameterName = "x")
        {
            if (field == null)
                throw new ArgumentException("field is null");

            var parameter = Expression.Parameter(field.DeclaringType, "x");
            return Expression.Field(parameter, field);
        }
    }
}