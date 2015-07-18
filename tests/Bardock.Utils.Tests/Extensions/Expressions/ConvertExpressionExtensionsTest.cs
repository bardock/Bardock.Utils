using Bardock.Utils.Extensions;
using Bardock.Utils.Types;
using FluentAssertions;
using System.Linq.Expressions;
using Xunit;

namespace Bardock.Utils.Tests.Extensions
{
    public class ConvertExpressionExtensionsTest
    {
        [Fact]
        public void RemoveConvert_FromConstantLambdaExpressionWithAutoCast_ShouldBeConstantExpression()
        {
            //Setup
            var expr = Lambda.Expr<object>(() => true);

            //Exercise
            var actual = expr.Body.RemoveConvert();
            
            //Verify
            actual.Should().NotBeNull().And.BeOfType<ConstantExpression>()
                .Which.Value.Should().Be(true);
        }

        [Fact]
        public void RemoveConvert_FromLambdaExpressionWithAndAlsoOperationAndAutoCast_ShouldBeAndAlsoBinaryExpression()
        {
            //Setup
            var expr = Lambda.Expr<string, object>(x => x.IsNormalized() && x.Length == 1);

            //Exercise
            var actual = expr.Body.RemoveConvert();

            //Verify
            actual.Should().NotBeNull().And.BeAssignableTo<BinaryExpression>()
                .Which.NodeType.Should().Be(ExpressionType.AndAlso);
        }
    }
}