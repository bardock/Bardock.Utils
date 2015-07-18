using System.Linq.Expressions;

namespace Bardock.Utils.Extensions
{
    public static class LambdaExpressionExtensions
    {
        /// <summary>
        /// Converts an Expression to a LambdaExpression by first constructing a delegate type.
        /// </summary>
        /// <param name="expr">An System.Linq.Expressions.Expression to set the System.Linq.Expressions.LambdaExpression.Body property equal to</param>
        public static LambdaExpression ToLambda(this Expression expr)
        {
            if (expr is LambdaExpression)
                return (LambdaExpression)expr;

            var parameters = expr.GetParameterExpressions();
            return Expression.Lambda(expr, parameters);
        }
    }
}