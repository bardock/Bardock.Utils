using System.Linq;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.Xunit;

namespace Bardock.Utils.UnitTest.Samples.Fixtures.Attributes
{
    internal class InlineDefaultDataAttribute : InlineAutoDataAttribute
    {
        public InlineDefaultDataAttribute(params object[] values)
            : base(new DefaultDataAttribute(), values.Where(x => !(x is ICustomization)))
        {
            foreach (var c in values.Where(x => x is ICustomization).Cast<ICustomization>())
            {
                this.AutoDataAttribute.Fixture.Customize(c);
            }
        }
    }
}