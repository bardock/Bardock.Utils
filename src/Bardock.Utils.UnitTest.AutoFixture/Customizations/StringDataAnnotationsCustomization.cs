using Bardock.Utils.UnitTest.AutoFixture.SpecimenBuilders;
using Ploeh.AutoFixture;
using System.Linq;
using System;
using Ploeh.AutoFixture.DataAnnotations;

namespace Bardock.Utils.UnitTest.AutoFixture.Customizations
{
    /// <summary>
    /// A customization that removes AutoFixture StringLengthAttribute DataAnnotations default support and
    /// adds custom combined support for String DataAnnotation attributes.
    /// <remarks>
    /// Adds custom combined support for <see cref="StringLengthAttribute"/>,
    /// <see cref="MinLengthAttribute"/>, <see cref="MaxLengthAttribute"/>, 
    /// and <see cref="EmailAddressAttribute"/>.
    /// </remarks>
    /// </summary>
    public class StringDataAnnotationsCustomization : CompositeCustomization
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StringDataAnnotationsCustomization"/> class.
        /// </summary>
        public StringDataAnnotationsCustomization()
            : base(
                new InnerCustomization(),
                new RemoveStringLengthDataAnnotationsSupportCustomization())
        { }

        private class InnerCustomization : ICustomization
        {
            /// <summary>
            /// Customizes the specified fixture by adding combined support for data annotations.
            /// </summary>
            /// <param name="fixture">The fixture to customize.</param>
            public void Customize(IFixture fixture)
            {
                fixture.Customizations.Add(new StringDataAnnotationsSpecimenBuilder());
            }
        }

        private class RemoveStringLengthDataAnnotationsSupportCustomization : ICustomization
        {
            public void Customize(IFixture fixture)
            {
                if (fixture == null)
                {
                    throw new ArgumentNullException("fixture");
                }

                fixture
                    .Customizations
                    .Where(c => typeof(StringLengthAttributeRelay) == c.GetType())
                    .ToList()
                    .ForEach(c => fixture.Customizations.Remove(c));
            }
        }
    }
}