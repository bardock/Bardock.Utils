using Ploeh.AutoFixture;
using Ploeh.AutoFixture.Dsl;
using Ploeh.AutoFixture.Kernel;
using System;
using System.Linq;

namespace Bardock.Utils.UnitTest.AutoFixture.Extensions
{
    public static class IFixtureExtensions
    {
        public static void Customize<T>(this IFixture @this, Func<ICustomizationComposer<T>, ISpecimenBuilder> composerTransformation, bool append)
        {
            if (append)
            {
                var composer = @this.GetCurrentCustomizationComposer<T>();
                if (composer != null)
                {
                    @this.Customize<T>(b => composerTransformation(composer));
                    return;
                }
            }
            @this.Customize(composerTransformation);
        }

        public static ICustomizationComposer<T> GetCurrentCustomizationComposer<T>(this IFixture @this)
        {
            return @this.Customizations
                .Where(xf => xf is ICustomizationComposer<T>)
                .Cast<ICustomizationComposer<T>>()
                .FirstOrDefault();
        }
    }
}