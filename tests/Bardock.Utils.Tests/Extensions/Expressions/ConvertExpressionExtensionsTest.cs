using Bardock.Utils.Extensions;
using Bardock.Utils.Types;
using FluentAssertions;
using Xunit;

namespace Bardock.Utils.Tests.Extensions
{
    public class ConvertExpressionExtensionsTest
    {
        //TODO
        //[Fact]
        public void RemoveConvert_CastingExpression_ShouldReturnTrue()
        {
            //Setup
            var expr = Lambda.Expr<object>(() => true);

            //Exercise
            var actual = expr.RemoveConvert();
            
            //Verify
            actual.Should().NotBeNull();
        }
    }
}