﻿using Ploeh.AutoFixture;
using Ploeh.AutoFixture.Xunit2;
using System;
using System.Linq;

namespace Bardock.Utils.UnitTest.AutoFixture.Xunit2.Fixtures.Attributes
{
    /// <summary>
    /// An abstract class that provides auto-generated data specimens generated by AutoFixture as an extension
    /// to xUnit.net's Theory attribute.
    /// You must inherit this class in order to specify a default <see cref="ICustomization"/> instance at constructors.
    /// </summary>
    public abstract class DefaultDataAttribute : AutoDataAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultDataAttribute"/> class with default customization.
        /// </summary>
        /// <param name="defaultCustomization">The default <see cref="ICustomization"/> instance.</param>
        public DefaultDataAttribute(ICustomization defaultCustomization)
            : base(new Fixture().Customize(defaultCustomization))
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultDataAttribute"/> class with default customization
        /// and others to apply.
        /// </summary>
        /// <param name="defaultCustomization">The default <see cref="ICustomization"/> instance.</param>
        /// <param name="customizationTypes">Other <see cref="ICustomization"/> types to apply.</param>
        public DefaultDataAttribute(ICustomization defaultCustomization, params Type[] customizationTypes)
            : this(defaultCustomization)
        {
            this.Fixture.Customize(
                new CompositeCustomization(
                    customizationTypes.Select(t =>
                        (ICustomization)Activator.CreateInstance(t, null))));
        }
    }
}