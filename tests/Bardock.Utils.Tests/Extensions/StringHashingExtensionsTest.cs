using Bardock.Utils.Extensions;
using System.Text;
using Xunit;

namespace Bardock.Utils.Tests.Extensions
{
    public class StringHashingExtensionsTest
    {
        [Fact]
        public void GetHashString()
        {
            var r = "asÑdáfg".GetHashString();
            Assert.Equal("bb05f78369b6d3938d117f45a37b8983", r);
        }

        [Fact]
        public void GetHashString_UTF32()
        {
            var r = "asÑdáfg".GetHashString(Encoding.UTF32);
            Assert.Equal("0f9d186ce2db1e4237b38fb1bcb94395", r);
        }
    }
}
