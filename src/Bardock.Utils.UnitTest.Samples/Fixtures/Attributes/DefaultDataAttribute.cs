using System;
using Bardock.Utils.UnitTest.Samples.Fixtures.Customizations;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.Xunit;

namespace Bardock.Utils.UnitTest.Samples.Fixtures.Attributes
{
    internal class DefaultDataAttribute : AutoDataAttribute
    {
        internal DefaultDataAttribute()
            : base(new Fixture().Customize(new DefaultCustomization()))
        { }

        internal DefaultDataAttribute(params Type[] customizationTypes)
            : base(new Fixture().Customize(new DefaultCustomization())) // If this line is replaced by "this()", the sample tests stop working
        {
            foreach (var t in customizationTypes)
            {
                this.Fixture.Customize((ICustomization)Activator.CreateInstance(t, null));
            }
        }
    }
}