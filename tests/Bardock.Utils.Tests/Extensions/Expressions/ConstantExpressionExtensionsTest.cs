using Bardock.Utils.Extensions;
using FluentAssertions;
using System;
using System.Linq.Expressions;
using Xunit;

namespace Bardock.Utils.Tests.Extensions
{
    public class ConstantExpressionExtensionsTest
    {
        private Expression<Func<TResult>> Expr<TResult>(Expression<Func<TResult>> f)
        {
            return f;
        }

        private Expression<Func<TArg, TResult>> Expr<TArg, TResult>(Expression<Func<TArg, TResult>> f)
        {
            return f;
        }

        [Fact]
        public void IsConstant_UseConstantBoolExpression_ShouldReturnTrue()
        {
            //Setup
            var expr = Expr(() => true);

            //Exercise
            var actual = expr.Body.IsConstant(true);

            //Verify
            actual.Should().BeTrue();
        }

        [Fact]
        public void IsConstant_UseConstantStringExpression_ShouldReturnTrue()
        {
            //Setup
            var expr = Expr(() => "true");

            //Exercise
            var actual = expr.Body.IsConstant("true");

            //Verify
            actual.Should().BeTrue();
        }

        [Fact]
        public void IsConstant_UseConstantNullExpression_ShouldReturnTrue()
        {
            //Setup
            var expr = Expr<string>(() => null);

            //Exercise
            var actual = expr.Body.IsConstant(null);

            //Verify
            actual.Should().BeTrue();
        }

        [Fact]
        public void IsConstant_UseConstantStringExpressionAndPassDifferentValue_ShouldReturnFalse()
        {
            //Setup
            var expr = Expr(() => "true");

            //Exercise
            var actual = expr.Body.IsConstant("true ");

            //Verify
            actual.Should().BeFalse();
        }

        [Fact]
        public void IsConstant_UseConstantBoolExpressionAndPassStringValue_ShouldReturnFalse()
        {
            //Setup
            var expr = Expr(() => true);

            //Exercise
            var actual = expr.Body.IsConstant(true.ToString());

            //Verify
            actual.Should().BeFalse();
        }

        [Fact]
        public void IsConstant_UseVariableStringExpression_ShouldReturnFalse()
        {
            //Setup
            var expr = Expr((object x) => x.ToString());

            //Exercise
            var actual = expr.Body.IsConstant("");

            //Verify
            actual.Should().BeFalse();
        }
    }
}