using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoMoq;

namespace Bardock.Utils.UnitTest.Samples.Fixtures.Customizations
{
    internal class DefaultCustomization : CompositeCustomization
    {
        public DefaultCustomization()
            : base(
                new DataContextCustomization(),
                new AutoMoqCustomization())
        {
        }
    }
}