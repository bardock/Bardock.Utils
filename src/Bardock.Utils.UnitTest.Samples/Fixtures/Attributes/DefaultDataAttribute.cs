using System;
using System.Linq;
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
            : this()
        {
            this.Fixture.Customize(
                new CompositeCustomization(
                    customizationTypes.Select(t =>
                        (ICustomization)Activator.CreateInstance(t, null))));
        }
    }
}