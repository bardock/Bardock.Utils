using Bardock.Utils.UnitTest.Samples.Fixtures.Customizations;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.Xunit;

namespace Bardock.Utils.UnitTest.Samples.Fixtures.Attributes
{
    internal class DefaultDataAttribute : AutoDataAttribute
    {
        public DefaultDataAttribute()
            : base(new Fixture().Customize(new DefaultCustomization()))
        {
        }
    }
}