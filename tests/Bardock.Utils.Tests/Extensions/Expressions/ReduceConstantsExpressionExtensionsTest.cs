using System;
using System.Linq.Expressions;
using Bardock.Utils.Extensions;
using Xunit;

namespace Bardock.Utils.Tests.Extensions
{
    public class ReduceConstantsExpressionExtensionsTest
    {
        [Fact]
        public void ReduceConstants_AllConstants()
        {
            Expression<Func<string, bool>> exp = (x => new DateTime(2000, 1, 1).Year == 2000);
            var r = exp.ReduceConstants();
            Assert.Equal("x => True", r.ToString());
        }

        [Fact]
        public void ReduceConstants_WithVariable()
        {
            Expression<Func<string, bool>> exp = (x => new DateTime(2000, 1, 1).Year == 2000 || x == "asd");
            var r = exp.ReduceConstants();
            Assert.Equal("x => (True OrElse (x == \"asd\"))", r.ToString());
        }

        [Fact]
        public void ReduceConstants_AllVariables()
        {
            Expression<Func<string, bool>> exp = (x => x == "asd");
            var r = exp.ReduceConstants();
            Assert.Equal("x => (x == \"asd\")", r.ToString());
        }
    }
}
