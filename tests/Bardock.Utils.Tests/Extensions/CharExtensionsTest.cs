using Bardock.Utils.Extensions;
using System.Text;
using Xunit;

namespace Bardock.Utils.Tests.Extensions
{
    public class CharExtensionsTest
    {
        [Fact]
        public void ToLower_A()
        {
            var r = 'A'.ToLower();
            Assert.Equal('a', r);
        }

        [Fact]
        public void ToLower_Ñ()
        {
            var r = 'Ñ'.ToLower();
            Assert.Equal('ñ', r);
        }

        [Fact]
        public void ToLower_Z()
        {
            var r = 'Z'.ToLower();
            Assert.Equal('z', r);
        }

        [Fact]
        public void ToLower_a()
        {
            var r = 'a'.ToLower();
            Assert.Equal('a', r);
        }

        [Fact]
        public void ToLower_Symbol()
        {
            var r = '!'.ToLower();
            Assert.Equal('!', r);
        }
    }
}
