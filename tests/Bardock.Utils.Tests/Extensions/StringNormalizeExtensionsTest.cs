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
            var r = "asÑdáfg loremp  qwert- ,.".SEONormalize();
            Assert.Equal("asndafg-loremp-qwert", r);
        }
    }
}
