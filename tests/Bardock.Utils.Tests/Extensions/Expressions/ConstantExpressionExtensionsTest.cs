using Bardock.Utils.Extensions;
using Bardock.Utils.Types;
using FluentAssertions;
using Xunit;

namespace Bardock.Utils.Tests.Extensions
{
    public class ConstantExpressionExtensionsTest
    {
        [Fact]
        public void IsConstant_UseConstantBoolExpression_ShouldReturnTrue()
        {
            //Setup
            var expr = Lambda.Expr(() => true);

            //Exercise
            var actual = expr.Body.IsConstant(true);

            //Verify
            actual.Should().BeTrue();
        }

        [Fact]
        public void IsConstant_UseConstantStringExpression_ShouldReturnTrue()
        {
            //Setup
            var expr = Lambda.Expr(() => "true");

            //Exercise
            var actual = expr.Body.IsConstant("true");

            //Verify
            actual.Should().BeTrue();
        }

        [Fact]
        public void IsConstant_UseConstantNullExpression_ShouldReturnTrue()
        {
            //Setup
            var expr = Lambda.Expr<string>(() => null);

            //Exercise
            var actual = expr.Body.IsConstant(null);

            //Verify
            actual.Should().BeTrue();
        }

        [Fact]
        public void IsConstant_UseConstantStringExpressionAndPassDifferentValue_ShouldReturnFalse()
        {
            //Setup
            var expr = Lambda.Expr(() => "true");

            //Exercise
            var actual = expr.Body.IsConstant("true ");

            //Verify
            actual.Should().BeFalse();
        }

        [Fact]
        public void IsConstant_UseConstantBoolExpressionAndPassStringValue_ShouldReturnFalse()
        {
            //Setup
            var expr = Lambda.Expr(() => true);

            //Exercise
            var actual = expr.Body.IsConstant(true.ToString());

            //Verify
            actual.Should().BeFalse();
        }

        [Fact]
        public void IsConstant_UseVariableStringExpression_ShouldReturnFalse()
        {
            //Setup
            var expr = Lambda.Expr((object x) => x.ToString());

            //Exercise
            var actual = expr.Body.IsConstant("");

            //Verify
            actual.Should().BeFalse();
        }
    }
}