using System;
using System.Linq.Expressions;

namespace Bardock.Utils.Extensions
{
    public static class ConstantExpressionExtensions
    {
        /// <summary>
        /// Determines if expression is a constant with specified value
        /// </summary>
        public static bool IsConstant(
            this Expression expr,
            object value)
        {
            var constExpr = expr as ConstantExpression;
            return constExpr != null
                && (constExpr.Value == null && value == null
                    || constExpr.Value != null && constExpr.Value.Equals(value));
        }

        /// <summary>
        /// Determines if expression is a constant with specified value
        /// </summary>
        [Obsolete("Please use the lambda expression body to invoke extension method IsConstant")]
        public static bool IsConstant<R>(
            this Expression<Func<R>> expr,
            R value)
        {
            return expr.Body.IsConstant(value);
        }

        /// <summary>
        /// Determines if expression is a constant with specified value
        /// </summary>
        [Obsolete("Please use the lambda expression body to invoke extension method IsConstant")]
        public static bool IsConstant<A1, R>(
            this Expression<Func<A1, R>> expr,
            R value)
        {
            return expr.Body.IsConstant(value);
        }
    }
}