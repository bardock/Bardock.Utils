using Bardock.Utils.Extensions;
using System.Text;
using Xunit;

namespace Bardock.Utils.Tests.Extensions
{
    public class StringNormalizeExtensionsTest
    {
        [Fact]
        public void SEONormalize()
        {
            var r = "asÑdáfg lore♦mp♥  qwÉrt- ,.".SEONormalize();
            Assert.Equal("asndafg-loremp-qwert", r);
        }

        [Fact]
        public void SEONormalize_MaxLength()
        {
            var r = "asÑdáfg lore♦mp♥  qwÉrt- ,.".SEONormalize(maxLength: 8);
            Assert.Equal("asndafg", r);
        }

        [Fact]
        public void SEONormalize_Empty()
        {
            var r = "".SEONormalize();
            Assert.Equal("", r);
        }

        [Fact]
        public void SEONormalize_WhiteSpace()
        {
            var r = "  ".SEONormalize();
            Assert.Equal("", r);
        }
    }
}
