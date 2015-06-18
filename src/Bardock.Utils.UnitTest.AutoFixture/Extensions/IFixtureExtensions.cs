using Ploeh.AutoFixture;
using Ploeh.AutoFixture.Dsl;
using Ploeh.AutoFixture.Kernel;
using System;
using System.Linq;

namespace Bardock.Utils.UnitTest.AutoFixture.Extensions
{
    public static class IFixtureExtensions
    {
        /// <summary>
        /// Customizes the specified composer transformation.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fixture">The fixture.</param>
        /// <param name="composerTransformation">The composer transformation.</param>
        /// <param name="append">if set to <c>true</c> [append].</param>
        public static void Customize<T>(this IFixture fixture, Func<ICustomizationComposer<T>, ISpecimenBuilder> composerTransformation, bool append)
        {
            if (append)
            {
                var composer = fixture.GetCurrentCustomizationComposer<T>();
                if (composer != null)
                {
                    fixture.Customize<T>(b => composerTransformation(composer));
                    return;
                }
            }
            fixture.Customize(composerTransformation);
        }

        /// <summary>
        /// Gets the current customization composer.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fixture">The fixture.</param>
        /// <returns></returns>
        public static ICustomizationComposer<T> GetCurrentCustomizationComposer<T>(this IFixture fixture)
        {
            return fixture.Customizations
                .Where(xf => xf is ICustomizationComposer<T>)
                .Cast<ICustomizationComposer<T>>()
                .FirstOrDefault();
        }
    }
}