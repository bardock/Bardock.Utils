using Bardock.Utils.Linq.Expressions;
using Bardock.Utils.UnitTest.AutoFixture.Customizations;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.Xunit2;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Bardock.Utils.UnitTest.AutoFixture.Xunit2.Attributes
{
    /// <summary>
    /// An attribute that can be applied to parameters in an <see cref="AutoDataAttribute"/>-driven
    /// Theory to indicate that the parameter value should have its members mapped to the
    /// <paramref name="destinationType"/> members.
    /// </summary>
    public abstract class MapMembersAttribute : CustomizeAttribute
    {
        private Type _destinationType;

        /// <summary>
        /// Initializes a new instance of the <see cref="MapMembersAttribute"/> class.
        /// </summary>
        /// <param name="destinationType">Type of the destination.</param>
        public MapMembersAttribute(Type destinationType)
        {
            _destinationType = destinationType;
        }

        /// <summary>
        /// Gets a customization that maps the members of the <see cref="Type"/> of the a parameter
        /// to the members of the <paramref name="destinationType"/>.
        /// </summary>
        /// <param name="parameter">The parameter for which the customization is requested.</param>
        /// <returns>
        /// A customization that maps the members of the <see cref="Type"/> of the a parameter
        /// to the members of the <paramref name="destinationType"/>.
        /// </returns>
        public override ICustomization GetCustomization(ParameterInfo parameter)
        {
            var sourceType = parameter.ParameterType;
            return new MapMembersCustomization(
                sourceType,
                _destinationType,
                Configure(sourceType, _destinationType));
        }

        /// <summary>
        /// Configures member mapping for the specified source type.
        /// </summary>
        /// <param name="sourceType">Type of the source.</param>
        /// <param name="destinationType">Type of the destination.</param>
        /// <returns>
        /// A collection of <see cref="MemberMapping"/> that describes how to perform the mapping.
        /// </returns>
        public abstract IEnumerable<MemberMapping> Configure(Type sourceType, Type destinationType);
    }

    /// <summary>
    /// A composer that maps members of a <typeparamref name="TSource"/> to a <typeparamref name="TDestination"/>
    /// </summary>
    /// <typeparam name="TSource">The type of the source.</typeparam>
    /// <typeparam name="TDestination">The type of the destination.</typeparam>
    public class MembersMappingComposer<TSource, TDestination> : IEnumerable<MemberMapping>
    {
        private readonly IReadOnlyList<MemberMapping> _mappings;

        /// <summary>
        /// Initializes a new instance of the <see cref="MembersMappingComposer{TSource, TDestination}"/> class.
        /// </summary>
        public MembersMappingComposer()
            : this(new List<MemberMapping>())
        { }

        private MembersMappingComposer(List<MemberMapping> mappings)
        {
            _mappings = mappings;
        }

        /// <summary>
        /// Maps the specified source and returns a new modified instance of the composer.
        /// </summary>
        /// <typeparam name="TReturn">The type of the return.</typeparam>
        /// <param name="source">The source.</param>
        /// <param name="destination">The destination.</param>
        /// <returns></returns>
        public MembersMappingComposer<TSource, TDestination> Map<TReturn>(
            Expression<Func<TSource, TReturn>> source,
            Expression<Func<TDestination, TReturn>> destination)
        {
            var mappings = _mappings.ToList();

            mappings.Add(
                new MemberMapping(
                    ((MemberExpression)ExpressionHelper.RemoveConvert(source).Body).Member,
                    ((MemberExpression)ExpressionHelper.RemoveConvert(destination).Body).Member));

            return new MembersMappingComposer<TSource, TDestination>(mappings);
        }

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Collections.Generic.IEnumerator`1" /> that can be used to iterate through the collection.
        /// </returns>
        public IEnumerator<MemberMapping> GetEnumerator()
        {
            return _mappings.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}