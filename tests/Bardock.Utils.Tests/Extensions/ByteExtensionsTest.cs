using Bardock.Utils.Extensions;
using System.Text;
using Xunit;

namespace Bardock.Utils.Tests.Extensions
{
    public class ByteExtensionsTest
    {
        [Fact]
        public void ToHexa()
        {
            var r = ((byte)123).ToHexa();
            Assert.Equal("7b", r);
        }

        [Fact]
        public void ToHexa_Array()
        {
            var r = (new byte[] {123, 1, 0, 211}).ToHexa();
            Assert.Equal("7b0100d3", r);
        }

        [Fact]
        public void Encode_Array()
        {
            var bytes = new byte[] { 123, 1, 0, 211 };
            var r = bytes.Encode(Encoding.UTF8);
            Assert.Equal(Encoding.UTF8.GetString(bytes), r);
        }
    }
}
