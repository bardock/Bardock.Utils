using System;
using System.Linq;
using System.Linq.Expressions;

namespace Bardock.Utils.Extensions
{
    public class PartialApplyExpressionVisitor<A1> : ExpressionVisitor
    {
        private readonly LambdaExpression _expr;
        private readonly A1 _arg1;
        private ParameterExpression _paramToApply;

        public PartialApplyExpressionVisitor(LambdaExpression expr, A1 arg1, ParameterExpression paramToApply = null)
        {
            this._expr = expr;
            this._arg1 = arg1;
            this._paramToApply = paramToApply ?? expr.Parameters.First();
        }

        public Expression Visit()
        {
            return this.Visit(_expr.Body);
        }

        protected override Expression VisitParameter(ParameterExpression node)
        {
            if (node.Equals(this._paramToApply))
            {
                return Expression.Constant(_arg1, node.Type);
            }
            return base.VisitParameter(node);
        }
    }

    public static class PartialApplyExpressionExtensions
    {

        #region PartialApply_Func

        public static Expression<Func<R>> PartialApply<A1, R>(
            this Expression<Func<A1, R>> expr, A1 arg1)
        {
            return Expression.Lambda<Func<R>>(
                new PartialApplyExpressionVisitor<A1>(expr, arg1).Visit());
        }

        public static Expression<Func<A2, R>> PartialApply<A1, A2, R>(
            this Expression<Func<A1, A2, R>> expr, A1 arg1)
        {
            return Expression.Lambda<Func<A2, R>>(
                new PartialApplyExpressionVisitor<A1>(expr, arg1).Visit(), 
                expr.Parameters.Skip(1));
        }

        public static Expression<Func<A2, A3, R>> PartialApply<A1, A2, A3, R>(
            this Expression<Func<A1, A2, A3, R>> expr, A1 arg1)
        {
            return Expression.Lambda<Func<A2, A3, R>>(
                new PartialApplyExpressionVisitor<A1>(expr, arg1).Visit(), 
                expr.Parameters.Skip(1));
        }

        #region PartialApply_Func_LastParam

        public static Expression<Func<A1, R>> PartialApply<A1, A2, R>(
            this Expression<Func<A1, A2, R>> expr, A2 arg2)
        {
            return Expression.Lambda<Func<A1, R>>(
                new PartialApplyExpressionVisitor<A2>(expr, arg2, expr.Parameters.Last()).Visit(),
                expr.Parameters.Take(1));
        }

        public static Expression<Func<A1, A2, R>> PartialApply<A1, A2, A3, R>(
            this Expression<Func<A1, A2, A3, R>> expr, A3 arg3)
        {
            return Expression.Lambda<Func<A1, A2, R>>(
                new PartialApplyExpressionVisitor<A3>(expr, arg3, expr.Parameters.Last()).Visit(),
                expr.Parameters.Take(2));
        }

        #endregion

        #endregion

        #region PartialApply_Action

        public static Expression<Action> PartialApply<A1>(
            this Expression<Action<A1>> expr, A1 arg1)
        {
            return Expression.Lambda<Action>(
                new PartialApplyExpressionVisitor<A1>(expr, arg1).Visit());
        }

        public static Expression<Action<A2>> PartialApply<A1, A2>(
            this Expression<Action<A1, A2>> expr, A1 arg1)
        {
            return Expression.Lambda<Action<A2>>(
                new PartialApplyExpressionVisitor<A1>(expr, arg1).Visit(), 
                expr.Parameters.Skip(1));
        }

        public static Expression<Action<A2, A3>> PartialApply<A1, A2, A3>(
            this Expression<Action<A1, A2, A3>> expr, A1 arg1)
        {
            return Expression.Lambda<Action<A2, A3>>(
                new PartialApplyExpressionVisitor<A1>(expr, arg1).Visit(), 
                expr.Parameters.Skip(1));
        }

        #region PartialApply_Action_LastParam

        public static Expression<Action<A1>> PartialApplyLast<A1, A2>(
            this Expression<Action<A1, A2>> expr, A2 arg2)
        {
            return Expression.Lambda<Action<A1>>(
                new PartialApplyExpressionVisitor<A2>(expr, arg2, expr.Parameters.Last()).Visit(), 
                expr.Parameters.Take(1));
        }

        public static Expression<Action<A1, A2>> PartialApplyLast<A1, A2, A3>(
            this Expression<Action<A1, A2, A3>> expr, A3 arg3)
        {
            return Expression.Lambda<Action<A1, A2>>(
                new PartialApplyExpressionVisitor<A3>(expr, arg3, expr.Parameters.Last()).Visit(), 
                expr.Parameters.Take(2));
        }

        #endregion

        #endregion
    }
}
