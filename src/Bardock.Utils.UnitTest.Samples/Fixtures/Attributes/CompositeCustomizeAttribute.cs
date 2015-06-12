using Ploeh.AutoFixture;
using Ploeh.AutoFixture.Xunit2;

namespace Bardock.Utils.UnitTest.Samples.Fixtures.Attributes
{
    /// <summary>
    /// Base class for applying multiple customizations to parameters in methods decorated with Ploeh.AutoFixture.Xunit2.AutoDataAttribute.
    /// Important: Customizations are applied from left to right, this is important because the order of cutomizations matters: 
    /// http://blog.ploeh.dk/2012/07/31/TheorderofAutoFixtureCustomizationsmatter/
    /// </summary>
    public abstract class CompositeCustomizeAttribute : CustomizeAttribute
    {
        public override ICustomization GetCustomization(System.Reflection.ParameterInfo parameter)
        {
            return new CompositeCustomization(GetCustomizations(parameter));
        }

        /// <summary>
        /// Gets a multiple customizations for a parameter.
        /// </summary>
        /// <param name="parameter">The parameter for which the customizations are requested.</param>
        /// <returns>The customizations to apply for the given parameter</returns>
        public abstract ICustomization[] GetCustomizations(System.Reflection.ParameterInfo parameter);
    }
}