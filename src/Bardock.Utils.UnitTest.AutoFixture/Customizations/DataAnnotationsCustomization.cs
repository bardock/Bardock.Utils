using Bardock.Utils.UnitTest.AutoFixture.SpecimenBuilders;
using Ploeh.AutoFixture;

namespace Bardock.Utils.UnitTest.AutoFixture.Customizations
{
    /// <summary>
    /// A customization that removes AutoFixture DataAnnotations default support and
    /// adds custom combined support for DataAnnotation attributes.
    /// <remarks>
    /// Adds custom combined support for <see cref="StringLengthAttribute"/>,
    /// <see cref="MinLengthAttribute"/>, <see cref="MaxLengthAttribute"/>,
    /// <see cref="RangeAttribute"/>, <see cref="RegularExpressionAttribute"/>
    /// and <see cref="EmailAddressAttribute"/>.
    /// </remarks>
    /// </summary>
    public class DataAnnotationsCustomization : CompositeCustomization
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DataAnnotationsCustomization"/> class.
        /// </summary>
        public DataAnnotationsCustomization()
            : base(
                new Customization(),
                new NoDataAnnotationsCustomization())
        { }

        private class Customization : ICustomization
        {
            /// <summary>
            /// Customizes the specified fixture by adding combined support for data annotations.
            /// </summary>
            /// <param name="fixture">The fixture to customize.</param>
            public void Customize(IFixture fixture)
            {
                fixture.Customizations.Add(new DataAnnotationsSpecimenBuilder());
            }
        }
    }
}