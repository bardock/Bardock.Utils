using Bardock.Utils.Extensions;
using System;
using System.IO;
using System.Text;
using Xunit;

namespace Bardock.Utils.Tests.Extensions
{
    public class LongExtensionsTest
    {
        [Fact]
        public void ToHumanFileSize_512B()
        {
            var size = 512L.ToHumanFileSize();
            Assert.Equal("512 B", size);
        }

        [Fact]
        public void ToHumanFileSize_1KB()
        {
            var size = 1024L.ToHumanFileSize();
            Assert.Equal("1 KB", size);
        }

        [Fact]
        public void ToHumanFileSize_1MB()
        {
            var size = ((long)1024 * 1024).ToHumanFileSize();
            Assert.Equal("1 MB", size);
        }

        [Fact]
        public void ToHumanFileSize_1GB()
        {
            var size = ((long)1024 * 1024 * 1024).ToHumanFileSize();
            Assert.Equal("1 GB", size);
        }
    }
}