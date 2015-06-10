using Bardock.Utils.UnitTest.Samples.Fixtures.Helpers;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.Dsl;

namespace Bardock.Utils.UnitTest.Samples.Fixtures.Customizations
{
    public abstract class CustomizationComposer<T> : ICustomization
    {
        public void Customize(IFixture fixture)
        {
            fixture.Customize<T>(b => Configure(fixture, b), append: true);
        }

        protected abstract IPostprocessComposer<T> Configure(IFixture fixture, ICustomizationComposer<T> c);
    }
}