using System;
using System.Linq.Expressions;

namespace Bardock.Utils.Extensions
{
    /// <summary>
    /// Reduces constant logical evaluations.
    /// Example:
    ///     input:  x => x == "a" || true
    ///     output: x => true
    /// </summary>
    public class ReduceEvaluationsExpressionVisitor : ExpressionVisitor
    {
        protected bool anyReduced;

        /// <summary>
        /// Recusively visits expression nodes until no one could be replaced
        /// </summary>
        public Expression ReduceAll(Expression expr)
        {
            do
            {
                anyReduced = false;
                expr = this.Visit(expr);
            }
            while (anyReduced);

            return expr;
        }

        /// <summary>
        /// Checks if expression node can be replaced by a constant (true or false).
        /// </summary>
        public override Expression Visit(Expression node)
        {
            var binExpr = node as BinaryExpression;
            if (binExpr != null)
            {
                if (binExpr.NodeType == ExpressionType.OrElse
                    && (binExpr.Left.IsConstant(true) || binExpr.Right.IsConstant(true)))
                {
                    anyReduced = true;
                    return Expression.Constant(true);
                }
                if (binExpr.NodeType == ExpressionType.AndAlso
                    && (binExpr.Left.IsConstant(false) || binExpr.Right.IsConstant(false)))
                {
                    anyReduced = true;
                    return Expression.Constant(false);
                }
            }
            return base.Visit(node);
        }
    }

    public static class ReduceEvaluationsExpressionExtensions
    {
        /// <summary>
        /// Reduces constant logical evaluations.
        /// Example:
        ///     input:  x => x == "a" || true
        ///     output: x => true
        /// </summary>
        public static Expression<Func<A1, bool>> ReduceEvaluations<A1>(
            this Expression<Func<A1, bool>> expr)
        {
            return Expression.Lambda<Func<A1, bool>>(
                new ReduceEvaluationsExpressionVisitor().ReduceAll(expr.Body),
                expr.Parameters);
        }

        /// <summary>
        /// Reduces constant logical evaluations.
        /// Example:
        ///     input:  x => x == "a" || true
        ///     output: x => true
        /// </summary>
        public static Expression<Func<A1, A2, bool>> ReduceEvaluations<A1, A2>(
            this Expression<Func<A1, A2, bool>> expr)
        {
            return Expression.Lambda<Func<A1, A2, bool>>(
                new ReduceEvaluationsExpressionVisitor().ReduceAll(expr.Body),
                expr.Parameters);
        }

        /// <summary>
        /// Reduces constant logical evaluations.
        /// Example:
        ///     input:  x => x == "a" || true
        ///     output: x => true
        /// </summary>
        public static Expression<Func<A1, A2, A3, bool>> ReduceEvaluations<A1, A2, A3>(
            this Expression<Func<A1, A2, A3, bool>> expr)
        {
            return Expression.Lambda<Func<A1, A2, A3, bool>>(
                new ReduceEvaluationsExpressionVisitor().ReduceAll(expr.Body),
                expr.Parameters);
        }
    }
}