using System;
using System.Linq.Expressions;
using Bardock.Utils.Extensions;
using Xunit;

namespace Bardock.Utils.Tests.Extensions
{
    public class ConstantExpressionExtensionsTest
    {
        [Fact]
        public void IsConstant_Bool()
        {
            Expression<Func<string, bool>> exp = (x => true);
            Assert.True(exp.IsConstant(true));
        }

        [Fact]
        public void IsConstant_String()
        {
            Expression<Func<string, string>> exp = (x => "asd");
            Assert.True(exp.IsConstant("asd"));
        }

        [Fact]
        public void IsConstant_DifferentValue()
        {
            Expression<Func<bool, string>> exp = (x => "true");
            Assert.False(exp.IsConstant("true "));
        }

        [Fact]
        public void IsConstant_DifferentType()
        {
            Expression<Func<bool, string>> exp = (x => "true");
            Assert.False(exp.IsConstant(true));
        }

        [Fact]
        public void IsConstant_Method()
        {
            Expression<Func<bool, string>> exp = (x => x.ToString());
            Assert.False(exp.IsConstant(""));
        }
    }
}
