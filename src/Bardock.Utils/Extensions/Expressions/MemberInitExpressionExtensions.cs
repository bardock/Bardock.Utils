using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Bardock.Utils.Extensions.Expressions
{
    public static class MemberInitExpressionExtensions
    {
        /// <summary>
        /// Combines two or more expressions that call a constructor and initialize one or more members (MemberInitExpressions)
        /// </summary>
        /// <typeparam name="TSource">Expressions parameter type</typeparam>
        /// <typeparam name="TDestination1">Return type of first expression</typeparam>
        /// <typeparam name="TDestination2">
        /// Return type of second and next expressions.
        /// TDestination2 must implement TDestination1 and it will be the combined expression return type</typeparam>
        /// <param name="memberInitExpression">First expression</param>
        /// <param name="memberInitExpressions">Other expressions to combine</param>
        /// <returns>A MemberInitExpression with members from all specified expressions</returns>
        public static Expression<Func<TSource, TDestination2>> AddMembers<TSource, TDestination1, TDestination2>(
            this Expression<Func<TSource, TDestination1>> memberInitExpression,
            params Expression<Func<TSource, TDestination2>>[] memberInitExpressions)
            where TDestination2 : TDestination1
        {
            var param = Expression.Parameter(typeof(TSource), "x");

            var emptyConstructor = typeof(TDestination2).GetConstructor(Type.EmptyTypes);
            if (emptyConstructor == null)
                throw new ArgumentException("TDestination2 must have an empty constructor");

            return Expression.Lambda<Func<TSource, TDestination2>>(
                body: Expression.MemberInit(
                        newExpression: Expression.New(emptyConstructor),
                        bindings: new[] { memberInitExpression }
                            .ToMemberAssignments(param)
                            .Concat(memberInitExpressions.ToMemberAssignments(param))
                            .GroupBy(x => x.Member)
                            .Select(x => x.Last())),
                parameters: param);
        }

        /// <summary>
        /// Converts MemberInitExpressions to MemberAssignments objects
        /// </summary>
        private static IEnumerable<MemberAssignment> ToMemberAssignments<TSource, TDestination>(
            this IEnumerable<Expression<Func<TSource, TDestination>>> memberInitExpressions, ParameterExpression param)
        {
            return from x in memberInitExpressions
                   let replace = new ParameterReplaceVisitor(x.Parameters[0], param)
                   from binding in ((MemberInitExpression)x.Body).Bindings.OfType<MemberAssignment>()
                   select Expression.Bind(binding.Member, replace.VisitAndConvert(binding.Expression, "Combine"));
        }

        private class ParameterReplaceVisitor : System.Linq.Expressions.ExpressionVisitor
        {
            private readonly ParameterExpression from, to;

            public ParameterReplaceVisitor(ParameterExpression from, ParameterExpression to)
            {
                this.from = from;
                this.to = to;
            }

            protected override Expression VisitParameter(ParameterExpression node)
            {
                return node == from ? to : base.VisitParameter(node);
            }
        }
    }
}