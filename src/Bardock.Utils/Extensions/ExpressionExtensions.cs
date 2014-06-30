using System;
using System.Linq;
using System.Linq.Expressions;

namespace Bardock.Utils.Extensions
{
    public static class ExpressionExtensions
    {
        private class PartialApplyVisitor<A1> : ExpressionVisitor
        {
            private readonly LambdaExpression _expr;
            private readonly A1 _arg1;
            public PartialApplyVisitor(LambdaExpression expr, A1 arg1)
            {
                _expr = expr;
                _arg1 = arg1;
            }
            public Expression Visit()
            {
                return this.Visit(_expr.Body);
            }
            protected override Expression VisitParameter(ParameterExpression node)
            {
                if (node.Equals(_expr.Parameters.First()))
                {
                    return Expression.Constant(_arg1);
                }
                return base.VisitParameter(node);
            }
        }

        public static Expression<Func<R>> PartialApply<A1, R>(
            this Expression<Func<A1, R>> expr, A1 arg1)
        {
            return Expression.Lambda<Func<R>>(
                new PartialApplyVisitor<A1>(expr, arg1).Visit());
        }

        public static Expression<Func<A2, R>> PartialApply<A1, A2, R>(
            this Expression<Func<A1, A2, R>> expr, A1 arg1)
        {
            return Expression.Lambda<Func<A2, R>>(
                new PartialApplyVisitor<A1>(expr, arg1).Visit(), expr.Parameters.Skip(1));
        }

        public static Expression<Func<A2, A3, R>> PartialApply<A1, A2, A3, R>(
            this Expression<Func<A1, A2, A3, R>> expr, A1 arg1)
        {
            return Expression.Lambda<Func<A2, A3, R>>(
                new PartialApplyVisitor<A1>(expr, arg1).Visit(), expr.Parameters.Skip(2));
        }

        public static Expression<Action> PartialApply<A1>(
            this Expression<Action<A1>> expr, A1 arg1)
        {
            return Expression.Lambda<Action>(
                new PartialApplyVisitor<A1>(expr, arg1).Visit());
        }

        public static Expression<Action<A2>> PartialApply<A1, A2>(
            this Expression<Action<A1, A2>> expr, A1 arg1)
        {
            return Expression.Lambda<Action<A2>>(
                new PartialApplyVisitor<A1>(expr, arg1).Visit(), expr.Parameters.Skip(1));
        }

        public static Expression<Action<A2, A3>> PartialApply<A1, A2, A3>(
            this Expression<Action<A1, A2, A3>> expr, A1 arg1)
        {
            return Expression.Lambda<Action<A2, A3>>(
                new PartialApplyVisitor<A1>(expr, arg1).Visit(), expr.Parameters.Skip(2));
        }
    }
}
