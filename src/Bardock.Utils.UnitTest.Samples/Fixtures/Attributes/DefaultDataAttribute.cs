using System;
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

        internal DefaultDataAttribute(params Type[] customizations)
            : base()
        {
            throw new NotImplementedException();
        }
    }
}