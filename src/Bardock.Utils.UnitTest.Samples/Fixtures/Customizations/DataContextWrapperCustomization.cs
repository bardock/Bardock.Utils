using Bardock.Utils.UnitTest.Data;
using Bardock.Utils.UnitTest.Data.EF;
using Bardock.Utils.UnitTest.Samples.SUT.Infra;
using Ploeh.AutoFixture;

namespace Bardock.Utils.UnitTest.Samples.Fixtures.Customizations
{
    internal class DataContextWrapperCustomization : ICustomization
    {
        public void Customize(IFixture fixture)
        {
            var db = fixture.Create<DataContext>();
            var wrapper = new DataContextWrapper(db);
            fixture.Register<IDataContextWrapper>(() => wrapper);

            var scopeFactory = new DataContextScopeFactory(wrapper);
            fixture.Register<IDataContextScopeFactory>(() => scopeFactory);
        }
    }
}