using System.Collections.Generic;
using System.Linq.Expressions;

namespace Bardock.Utils.Extensions
{
    public static class ParameterExpressionExtensions
    {
        /// <summary>
        /// Gets the sequence of ParameterExpressions that are present in the specified Expression tree.
        /// </summary>
        public static IEnumerable<ParameterExpression> GetParameterExpressions(this Expression expr)
        {
            var visitor = new GetParametersExpressionVisitor();
            visitor.Visit(expr);
            return visitor.Parameters;
        }

        internal class GetParametersExpressionVisitor : ExpressionVisitor
        {
            private readonly List<ParameterExpression> _parameters = new List<ParameterExpression>();

            internal IEnumerable<ParameterExpression> Parameters
            {
                get { return _parameters; }
            }

            protected override Expression VisitParameter(ParameterExpression node)
            {
                _parameters.Add(node);
                return base.VisitParameter(node);
            }
        }
    }
}