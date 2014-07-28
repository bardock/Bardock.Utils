using System;
using System.Linq.Expressions;
using Bardock.Utils.Extensions;
using Xunit;

namespace Bardock.Utils.Tests.Extensions
{
    public class ExpressionExtensionsTest
    {
        [Fact]
        public void PartialApply_Func_1Args()
        {
            Expression<Func<string, string>> exp = (x => x + "_suffix");
            var r = exp.PartialApply("test");
            Assert.Equal("() => (\"test\" + \"_suffix\")", r.ToString());
        }

        [Fact]
        public void PartialApply_Func_2Args()
        {
            Expression<Func<string, int, string>> exp = ((x, y) => x + y.ToString() + "_suffix");
            var r = exp.PartialApply("test");
            Assert.Equal("y => ((\"test\" + y.ToString()) + \"_suffix\")", r.ToString());
        }

        [Fact]
        public void PartialApply_Func_3Args()
        {
            Expression<Func<string, int, DateTime, string>> exp = ((x, y, z) => x + y.ToString() + z.ToString() + "_suffix");
            var r = exp.PartialApply("test");
            Assert.Equal("(y, z) => (((\"test\" + y.ToString()) + z.ToString()) + \"_suffix\")", r.ToString());
        }

        [Fact]
        public void PartialApply_Func_2Args_LastParam()
        {
            Expression<Func<string, int, string>> exp = ((x, y) => x + y.ToString() + "_suffix");
            var r = exp.PartialApply(1);
            Assert.Equal("x => ((x + 1.ToString()) + \"_suffix\")", r.ToString());
        }

        [Fact]
        public void PartialApply_Func_3Args_LastParam()
        {
            Expression<Func<string, int, DateTime, string>> exp = ((x, y, z) => x + y.ToString() + z.ToString() + "_suffix");
            var date = new DateTime(2000,1,1);
            var r = exp.PartialApply(date);
            Assert.Equal("(x, y) => (((x + y.ToString()) + "+date+".ToString()) + \"_suffix\")", r.ToString());
        }
    }
}
