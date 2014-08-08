using System;
using System.Linq.Expressions;
using Bardock.Utils.Extensions;
using Xunit;

namespace Bardock.Utils.Tests.Extensions
{
    public class ReduceEvaluationsExpressionExtensionsTest
    {
        [Fact]
        public void ReduceEvaluations_OrElse_1level()
        {
            Expression<Func<string, bool>> exp = (x => (false && x == "a") || true);
            var r = exp.ReduceEvaluations();
            Assert.Equal("x => True", r.ToString());
        }

        [Fact]
        public void ReduceEvaluations_OrElse_2levels()
        {
            Expression<Func<string, bool>> exp = (x => (false && x == "a") || (x == "b" || true));
            var r = exp.ReduceEvaluations();
            Assert.Equal("x => True", r.ToString());
        }

        [Fact]
        public void ReduceEvaluations_OrElse_3levels()
        {
            Expression<Func<string, bool>> exp = (x => (false && x == "a") || (x == "b" || (true || x == "c")));
            var r = exp.ReduceEvaluations();
            Assert.Equal("x => True", r.ToString());
        }

        [Fact]
        public void ReduceEvaluations_AndAlso_1level()
        {
            Expression<Func<string, bool>> exp = (x => (true || x == "a") && false);
            var r = exp.ReduceEvaluations();
            Assert.Equal("x => False", r.ToString());
        }

        [Fact]
        public void ReduceEvaluations_AndAlso_2levels()
        {
            Expression<Func<string, bool>> exp = (x => (true || x == "a") && (x == "b" && false));
            var r = exp.ReduceEvaluations();
            Assert.Equal("x => False", r.ToString());
        }

        [Fact]
        public void ReduceEvaluations_AndAlso_3levels()
        {
            Expression<Func<string, bool>> exp = (x => (true || x == "a") && (x == "b" && (false && x == "c")));
            var r = exp.ReduceEvaluations();
            Assert.Equal("x => False", r.ToString());
        }

        [Fact]
        public void ReduceEvaluations_Variable_3levels()
        {
            Expression<Func<string, bool>> exp = (x => (true || x == "a") && (x == "b" && (x == "c" || x == "d")));
            var r = exp.ReduceEvaluations();
            Assert.Equal("x => (True AndAlso ((x == \"b\") AndAlso ((x == \"c\") OrElse (x == \"d\"))))", r.ToString());
        }
    }
}
