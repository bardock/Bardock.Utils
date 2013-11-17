using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Bardock.Utils.Extensions;
using System.Text.RegularExpressions;

namespace Bardock.Utils.Tests.Extensions
{
    public class DateExtensionsTest
    {
        private static DateTime DATE1 = new DateTime(2013, 1, 5, 12, 45, 6, 123);

        [Fact]
        public void NormalizedFormat()
        {
            var r = DATE1.NormalizedFormat();
            Assert.Equal("2013-01-05 12:45:06", r);
        }

        [Fact]
        public void NormalizedDateFormat()
        {
            var r = DATE1.NormalizedDateFormat();
            Assert.Equal("2013-01-05", r);
        }

        [Fact]
        public void CompactFormat()
        {
            var r = DATE1.CompactFormat();
            Assert.Equal("20130105124506", r);
        }

        [Fact]
        public void CompactDateFormat()
        {
            var r = DATE1.CompactDateFormat();
            Assert.Equal("20130105", r);
        }

        [Fact]
        public void ToIsoFormat_Unespecified()
        {
            var r = DATE1.Clone(kind: DateTimeKind.Unspecified).ToIsoFormat();
            Assert.Equal("2013-01-05T12:45:06.1230000", r);
        }

        [Fact]
        public void ToIsoFormat_Utc()
        {
            var r = DATE1.Clone(kind: DateTimeKind.Utc).ToIsoFormat();
            Assert.Equal("2013-01-05T12:45:06.1230000Z", r);
        }

        [Fact]
        public void ToIsoFormat_Local()
        {
            var date = DATE1.Clone(kind: DateTimeKind.Local);
            var r = date.ToIsoFormat();

            var offset = TimeZoneInfo.Local.GetUtcOffset(date);
            Assert.Equal(string.Format("2013-01-05T12:45:06.1230000{0:+00;-00}:{1:00}", offset.Hours, offset.Minutes), r);
        }

        [Fact]
        public void ToDayStart()
        {
            var r = DATE1.ToDayStart();
            Assert.Equal(new DateTime(2013, 01, 05), r);
        }

        [Fact]
        public void ToDayEnd()
        {
            var r = DATE1.ToDayEnd();
            Assert.Equal(new DateTime(2013, 01, 05, 23, 59, 59, 999), r);
        }
    }
}
