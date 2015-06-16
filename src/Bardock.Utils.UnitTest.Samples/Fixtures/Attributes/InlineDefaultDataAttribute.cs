using Bardock.Utils.UnitTest.AutoFixture.Xunit2.Attributes;

namespace Bardock.Utils.UnitTest.Samples.Fixtures.Attributes
{
    internal class InlineDefaultDataAttribute : InlineAutoDataAndCustomizationsAttribute
    {
        public InlineDefaultDataAttribute(params object[] valuesAndCustomizationTypes)
            : base(new DefaultDataAttribute(), valuesAndCustomizationTypes)
        {
        }
    }
}