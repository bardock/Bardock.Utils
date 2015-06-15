using Ploeh.AutoFixture;
using Ploeh.AutoFixture.Xunit2;
using System;
using System.Linq;

namespace Bardock.Utils.UnitTest.AutoFixture.Xunit2.Fixtures.Attributes
{
    public abstract class DefaultDataAttribute : AutoDataAttribute
    {
        public DefaultDataAttribute(ICustomization defaultCustomization)
            : base(new Fixture().Customize(defaultCustomization))
        { }

        public DefaultDataAttribute(ICustomization defaultCustomization, params Type[] customizationTypes)
            : this(defaultCustomization)
        {
            this.Fixture.Customize(
                new CompositeCustomization(
                    customizationTypes.Select(t =>
                        (ICustomization)Activator.CreateInstance(t, null))));
        }
    }
}