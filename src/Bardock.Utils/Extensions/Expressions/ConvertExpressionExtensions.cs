using System.Linq.Expressions;

namespace Bardock.Utils.Extensions
{
    public static class ConvertExpressionExtensions
    {
        public static Expression RemoveConvert(this Expression expr)
        {
            while (expr.NodeType == ExpressionType.Convert || expr.NodeType == ExpressionType.ConvertChecked)
            {
                expr = ((UnaryExpression)expr).Operand;
            }
            return expr;
        }

        public static Expression RemoveConvert(this LambdaExpression expr)
        {
            return expr.Body.RemoveConvert();
        }
    }
}