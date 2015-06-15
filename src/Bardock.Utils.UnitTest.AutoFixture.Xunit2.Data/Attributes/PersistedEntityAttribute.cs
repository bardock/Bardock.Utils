using Bardock.Utils.UnitTest.Data.AutoFixture.Customizations;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.Xunit2;
using System.Reflection;

namespace Bardock.Utils.UnitTest.AutoFixture.Xunit2.Data.Attributes
{
    public class PersistedEntityAttribute : CustomizeAttribute
    {
        public override ICustomization GetCustomization(ParameterInfo parameter)
        {
            return new PersistedEntityCustomization(parameter);
        }
    }
}