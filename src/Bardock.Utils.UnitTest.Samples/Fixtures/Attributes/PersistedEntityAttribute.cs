using Bardock.Utils.UnitTest.Samples.Fixtures.Customizations;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.Xunit2;
using System.Reflection;

namespace Bardock.Utils.UnitTest.Samples.Fixtures.Attributes
{
    public class PersistedEntityAttribute : CustomizeAttribute
    {
        public override ICustomization GetCustomization(ParameterInfo parameter)
        {
            return new PersistedEntityCustomization(parameter);
        }
    }
}