using System;
using System.Linq.Expressions;

namespace Bardock.Utils.Extensions
{
    /// <summary>
    /// Evaluates expression nodes (if possible) in order to reduce them to constants.
    /// Example:
    ///     input:  x => new DateTime(2000,1,1).Year == 2000 || x.ID == 2
    ///     output: x => true || x.ID == 2
    /// </summary>
    public class ReduceConstantsExpressionVisitor : ExpressionVisitor
    {
        public override Expression Visit(Expression node)
        {
            try
            {
                var value = Expression.Lambda(node, null).Compile().DynamicInvoke();
                return Expression.Constant(value, node.Type);
            }
            catch (Exception)
            {
                return base.Visit(node);
            }
        }
    }

    public static class ReduceConstantsExpressionExtensions
    {
        /// <summary>
        /// Evaluates expression nodes (if possible) in order to reduce them to constants.
        /// Example:
        ///     input:  x => new DateTime(2000,1,1).Year == 2000 || x.ID == 2
        ///     output: x => true || x.ID == 2
        /// </summary>
        public static Expression<Func<A1, R>> ReduceConstants<A1, R>(
            this Expression<Func<A1, R>> expr)
        {
            return Expression.Lambda<Func<A1, R>>(
                new ReduceConstantsExpressionVisitor().Visit(expr.Body),
                expr.Parameters);
        }

        /// <summary>
        /// Evaluates expression nodes (if possible) in order to reduce them to constants.
        /// Example:
        ///     input:  x => new DateTime(2000,1,1).Year == 2000 || x.ID == 2
        ///     output: x => true || x.ID == 2
        /// </summary>
        public static Expression<Func<A1, A2, R>> ReduceConstants<A1, A2, R>(
            this Expression<Func<A1, A2, R>> expr)
        {
            return Expression.Lambda<Func<A1, A2, R>>(
                new ReduceConstantsExpressionVisitor().Visit(expr.Body),
                expr.Parameters);
        }

        /// <summary>
        /// Evaluates expression nodes (if possible) in order to reduce them to constants.
        /// Example:
        ///     input:  x => new DateTime(2000,1,1).Year == 2000 || x.ID == 2
        ///     output: x => true || x.ID == 2
        /// </summary>
        public static Expression<Func<A1, A2, A3, R>> ReduceConstants<A1, A2, A3, R>(
            this Expression<Func<A1, A2, A3, R>> expr)
        {
            return Expression.Lambda<Func<A1, A2, A3, R>>(
                new ReduceConstantsExpressionVisitor().Visit(expr.Body),
                expr.Parameters);
        }
    }
}