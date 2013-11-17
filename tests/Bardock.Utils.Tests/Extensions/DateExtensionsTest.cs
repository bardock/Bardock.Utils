using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Bardock.Utils.Extensions;

namespace Bardock.Utils.Tests.Extensions
{
    public class DateExtensionsTest
    {
        private static DateTime DATE_1 = new DateTime(2013, 1, 5, 12, 45, 6, 123);

        [Fact]
        public void NormalizedFormat()
        {
            var r = DATE_1.NormalizedFormat();
            Assert.Equal("2013-01-05 12:45:06", r);
        }

        [Fact]
        public void NormalizedDateFormat()
        {
            var r = DATE_1.NormalizedDateFormat();
            Assert.Equal("2013-01-05", r);
        }

        [Fact]
        public void CompactFormat()
        {
            var r = DATE_1.CompactFormat();
            Assert.Equal("20130105124506", r);
        }

        [Fact]
        public void CompactDateFormat()
        {
            var r = DATE_1.CompactDateFormat();
            Assert.Equal("20130105", r);
        }
    }
}
