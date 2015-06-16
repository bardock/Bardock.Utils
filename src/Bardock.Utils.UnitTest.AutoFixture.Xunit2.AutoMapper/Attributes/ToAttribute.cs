using Bardock.Utils.UnitTest.AutoFixture.AutoMapper.Customizations;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.Xunit2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bardock.Utils.UnitTest.AutoFixture.Xunit2.AutoMapper.Attributes
{
    public class ToAttribute : CustomizeAttribute
    {
        private Type _destinationType;

        public ToAttribute(Type destinationType)
        {
            _destinationType = destinationType;
        }

        public override ICustomization GetCustomization(System.Reflection.ParameterInfo parameter)
        {
            return new AutoMapperCustomization(parameter.ParameterType, _destinationType);
        }
    }
}
