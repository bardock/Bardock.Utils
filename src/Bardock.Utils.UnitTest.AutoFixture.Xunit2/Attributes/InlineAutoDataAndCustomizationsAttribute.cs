using Ploeh.AutoFixture;
using Ploeh.AutoFixture.Xunit2;
using System;
using System.Linq;

namespace Bardock.Utils.UnitTest.AutoFixture.Xunit2.Attributes
{
    public abstract class InlineAutoDataAndCustomizationsAttribute : InlineAutoDataAttribute
    {
        public InlineAutoDataAndCustomizationsAttribute(AutoDataAttribute autoDataAttribute, params object[] valuesAndCustomizationTypes)
            : base(
                autoDataAttribute,
                values: valuesAndCustomizationTypes.Where(x => !IsCustomizationType(x)).ToArray())
        {
            var customizations = valuesAndCustomizationTypes
                .Select(x => ToCustomizationTypeOrDefault(x))
                .Where(ct => ct != null)
                .Select(ct => (ICustomization)Activator.CreateInstance(ct, null));

            foreach (var c in customizations)
            {
                this.AutoDataAttribute.Fixture.Customize(c);
            }
        }

        private static bool IsCustomizationType(object target)
        {
            return ToCustomizationTypeOrDefault(target) != null;
        }

        private static Type ToCustomizationTypeOrDefault(object target)
        {
            var type = target as Type;
            if (type != null && typeof(ICustomization).IsAssignableFrom(type))
            {
                return type;
            }
            return null;
        }
    }
}