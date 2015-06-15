using Ploeh.AutoFixture;
using Ploeh.AutoFixture.Xunit2;
using System.Linq;

namespace Bardock.Utils.UnitTest.AutoFixture.Xunit2.Fixtures.Attributes
{
    public abstract class InlineDefaultDataAttribute : InlineAutoDataAttribute
    {
        public InlineDefaultDataAttribute(DefaultDataAttribute defaultDataAttribute, params object[] values)
            : base(defaultDataAttribute, values.Where(x => !(x is ICustomization)))
        {
            foreach (var c in values.Where(x => x is ICustomization).Cast<ICustomization>())
            {
                this.AutoDataAttribute.Fixture.Customize(c);
            }
        }
    }
}