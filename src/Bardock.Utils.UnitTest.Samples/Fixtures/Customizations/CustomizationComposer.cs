using Ploeh.AutoFixture;
using Ploeh.AutoFixture.Dsl;

namespace Bardock.Utils.UnitTest.Samples.Fixtures.Customizations
{
    public abstract class CustomizationComposer<T> : ICustomization
    {
        public void Customize(IFixture fixture)
        {
            var data = this.Configure(fixture.Build<T>()).Create();
            fixture.Register(() => data);
        }

        protected abstract IPostprocessComposer<T> Configure(ICustomizationComposer<T> c);
    }
}