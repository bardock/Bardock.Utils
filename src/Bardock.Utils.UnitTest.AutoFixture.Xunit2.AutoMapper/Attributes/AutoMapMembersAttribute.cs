using Bardock.Utils.UnitTest.AutoFixture.AutoMapper.Customizations;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.Xunit2;
using System;

namespace Bardock.Utils.UnitTest.AutoFixture.Xunit2.AutoMapper.Attributes
{
    public class AutoMapMembersAttribute : CustomizeAttribute
    {
        private Type _destinationType;

        public AutoMapMembersAttribute(Type destinationType)
        {
            _destinationType = destinationType;
        }

        public override ICustomization GetCustomization(System.Reflection.ParameterInfo parameter)
        {
            return new AutoMapMembersCustomization(parameter.ParameterType, _destinationType);
        }
    }
}