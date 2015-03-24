using Ploeh.AutoFixture.Xunit;

namespace Bardock.Utils.UnitTest.Samples.Fixtures.Attributes
{
    internal class InlineDefaultDataAttribute : InlineAutoDataAttribute
    {
        public InlineDefaultDataAttribute()
            : base(new DefaultDataAttribute())
        {
        }
    }
}