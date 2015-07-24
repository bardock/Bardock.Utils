using Bardock.Utils.Extensions;
using Bardock.Utils.Types;
using FluentAssertions;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.Xunit2;
using System;
using System.Linq.Expressions;
using Xunit;

namespace Bardock.Utils.Tests.Extensions
{
    public class LambdaExpressionExtensionsTest
    {
        [Fact]
        public void ToLambda_FromLambdaExpression_ShouldBeTheSame()
        {
            //Setup
            var expected = Lambda.Expr((DateTime x) => x.TimeOfDay);

            //Exercise
            var actual = expected.ToLambda();

            //Verify
            actual.Should().BeSameAs(expected);
        }

        private abstract class InlineLambdaExpressionAttribute : InlineAutoDataAttribute
        {
            protected InlineLambdaExpressionAttribute(LambdaExpression expr)
            {
                AutoDataAttribute.Fixture.Register(() => expr);
            }
        }

        private class SingleMemberExpressionWithOneParamAttribute : InlineLambdaExpressionAttribute
        {
            public SingleMemberExpressionWithOneParamAttribute()
                : base(Lambda.Expr((DateTime x) => x.TimeOfDay))
            { }
        }

        private class NestedMemberExpressionWithOneParamAttribute : InlineLambdaExpressionAttribute
        {
            public NestedMemberExpressionWithOneParamAttribute()
                : base(Lambda.Expr((DateTime x) => x.TimeOfDay.Minutes))
            { }
        }

        private class SingleMethodExpressionWithTwoParamsAttribute : InlineLambdaExpressionAttribute
        {
            public SingleMethodExpressionWithTwoParamsAttribute()
                : base(Lambda.Expr((DateTime x, string format) => x.ToString(format)))
            { }
        }

        private class NestedMethodExpressionWithoutParamAttribute : InlineLambdaExpressionAttribute
        {
            public NestedMethodExpressionWithoutParamAttribute()
                : base(Lambda.Expr(() => 1.ToString().ToLower()))
            { }
        }

        [Theory]
        [SingleMemberExpressionWithOneParam]
        [NestedMemberExpressionWithOneParam]
        [SingleMethodExpressionWithTwoParams]
        [NestedMethodExpressionWithoutParam]
        public void ToLambda_FromExpression_ShouldSucced(
            LambdaExpression expected)
        {
            //Exercise
            var actual = expected.Body.ToLambda();

            //Verify
            actual.ShouldBeEquivalentTo(expected);
        }
    }
}