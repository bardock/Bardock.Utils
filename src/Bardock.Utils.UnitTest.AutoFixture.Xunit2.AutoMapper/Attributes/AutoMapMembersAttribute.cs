using Bardock.Utils.UnitTest.AutoFixture.AutoMapper.Customizations;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.Xunit2;
using System;
using System.Reflection;

namespace Bardock.Utils.UnitTest.AutoFixture.Xunit2.AutoMapper.Attributes
{
    /// <summary>
    /// An attribute that can be applied to parameters in an <see cref="AutoDataAttribute"/>-driven
    /// Theory to indicate that the parameter value should have its members auto mapped by an
    /// AutoMapper map to the <paramref name="destinationType"/> members.
    /// </summary>
    public class AutoMapMembersAttribute : CustomizeAttribute
    {
        private Type _destinationType;

        public AutoMapMembersAttribute(Type destinationType)
        {
            _destinationType = destinationType;
        }

        /// <summary>
        /// Gets a customization that auto maps the members of the <see cref="Type"/> of the a parameter
        /// to the members of the <paramref name="destinationType"/> by an AutoMapper Map.
        /// </summary>
        /// <param name="parameter">The parameter for which the customization is requested.</param>
        /// <returns>
        /// A customization that maps the members of the <see cref="Type"/> of the a parameter
        /// to the members of the <paramref name="destinationType"/>.
        /// </returns>
        public override ICustomization GetCustomization(ParameterInfo parameter)
        {
            return new AutoMapMembersCustomization(parameter.ParameterType, _destinationType);
        }
    }
}