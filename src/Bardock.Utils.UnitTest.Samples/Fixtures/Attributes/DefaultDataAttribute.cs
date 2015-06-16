using Bardock.Utils.UnitTest.Samples.Fixtures.Customizations;
using System;

namespace Bardock.Utils.UnitTest.Samples.Fixtures.Attributes
{
    public class DefaultDataAttribute : Bardock.Utils.UnitTest.AutoFixture.Xunit2.Fixtures.Attributes.DefaultDataAttribute
    {
        public DefaultDataAttribute()
            : base(new DefaultCustomization())
        { }

        public DefaultDataAttribute(params Type[] customizationTypes)
            : base(new DefaultCustomization(), customizationTypes)
        { }
    }
}