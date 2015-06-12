using System;
using Bardock.Utils.UnitTest.Samples.Fixtures.Customizations;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.Xunit2;

namespace Bardock.Utils.UnitTest.Samples.Fixtures.Attributes
{
    public class DefaultDataAttribute : AutoDataAttribute
    {
        public DefaultDataAttribute()
            : base(new Fixture().Customize(new DefaultCustomization()))
        { }

        public DefaultDataAttribute(params Type[] customizationTypes)
            : base(new Fixture().Customize(new DefaultCustomization())) // If this line is replaced by "this()", the sample tests stop working
        {
            foreach (var t in customizationTypes)
            {
                this.Fixture.Customize((ICustomization)Activator.CreateInstance(t, null));
            }
        }
    }
}