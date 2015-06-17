using System;
using System.Linq;
using System.Linq.Expressions;

namespace Bardock.Utils.Extensions
{
    /// <summary>
    /// Replaces Equals methods by '==' operator
    /// </summary>
    public class ReplaceEqualsMethodByOperatorExpressionVisitor : ExpressionVisitor
    {
        protected override Expression VisitMethodCall(MethodCallExpression node)
        {
            if (node.Method.Name == "Equals" && node.Method.GetParameters().Count() == 1)
            {
                var left = node.Object;
                var right = node.Arguments.First().RemoveConvert();
                return Expression.Equal(left, right);
            }
            return base.VisitMethodCall(node);
        }
    }

    public static class ReplaceEqualsMethodByOperatorExpressionExtensions
    {
        /// <summary>
        /// Replaces Equals methods by '==' operator
        /// </summary>
        public static Expression<Func<A1, R>> ReplaceEqualsMethodByOperator<A1, R>(
            this Expression<Func<A1, R>> expr)
        {
            return Expression.Lambda<Func<A1, R>>(
                new ReplaceEqualsMethodByOperatorExpressionVisitor().Visit(expr.Body),
                expr.Parameters);
        }

        /// <summary>
        /// Replaces Equals methods by '==' operator
        /// </summary>
        public static Expression<Func<A1, A2, R>> ReplaceEqualsMethodByOperator<A1, A2, R>(
            this Expression<Func<A1, A2, R>> expr)
        {
            return Expression.Lambda<Func<A1, A2, R>>(
                new ReplaceEqualsMethodByOperatorExpressionVisitor().Visit(expr.Body),
                expr.Parameters);
        }

        /// <summary>
        /// Replaces Equals methods by '==' operator
        /// </summary>
        public static Expression<Func<A1, A2, A3, R>> ReplaceEqualsMethodByOperator<A1, A2, A3, R>(
            this Expression<Func<A1, A2, A3, R>> expr)
        {
            return Expression.Lambda<Func<A1, A2, A3, R>>(
                new ReplaceEqualsMethodByOperatorExpressionVisitor().Visit(expr.Body),
                expr.Parameters);
        }
    }
}