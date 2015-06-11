using Bardock.Utils.UnitTest.Samples.SUT.Infra;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoMoq;

namespace Bardock.Utils.UnitTest.Samples.Fixtures.Customizations
{
    internal class DefaultCustomization : CompositeCustomization
    {
        public DefaultCustomization()
            : base(
                new IgnoreEntityKeysCustomization<DataContext>(),
                new IgnoreEntityNavigationPropsCustomization<DataContext>(),
                new DataContextCustomization(),
                new DataContextWrapperCustomization(),
                new AutoMoqCustomization())
        {
        }
    }
}