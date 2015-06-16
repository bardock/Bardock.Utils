using AutoMapper;
using Bardock.Utils.UnitTest.AutoFixture.AutoMapper.Customizations;
using Bardock.Utils.UnitTest.AutoFixture.Customizations;
using Bardock.Utils.UnitTest.AutoFixture.Xunit2.Attributes;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.Xunit2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
