using Ploeh.AutoFixture;
using Ploeh.AutoFixture.Xunit;
using System;
using System.Reflection;

namespace Bardock.Utils.UnitTest.Samples.Fixtures.Attributes
{
    [AttributeUsage(AttributeTargets.Parameter, AllowMultiple = true)]
    public class CustomizeWithAttribute : CustomizeAttribute
    {
        private ICustomization _customization;

        public CustomizeWithAttribute(ICustomization customization)
        {
            this._customization = customization;
        }

        public CustomizeWithAttribute(Type type, params object[] args)
        {
            this._customization = Activator.CreateInstance(type, args) as ICustomization;
        }

        public override ICustomization GetCustomization(ParameterInfo parameter)
        {
            return this._customization;
        }
    }
}