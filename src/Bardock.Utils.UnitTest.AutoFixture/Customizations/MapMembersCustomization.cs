using Bardock.Utils.UnitTest.AutoFixture.SpecimenBuilders;
using Ploeh.AutoFixture;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Bardock.Utils.UnitTest.AutoFixture.Customizations
{
    /// <summary>
    /// A customization that maps the generation of member specimens of the <paramref name="sourceType"/> to the <paramref name="destinationType"/>
    /// given the specified <paramref name="mappings"/> collection
    /// </summary>
    public class MapMembersCustomization : ICustomization
    {
        private Type _sourceType;
        private Type _destinationType;
        private IEnumerable<MemberMapping> _mappings;

        /// <summary>
        /// Initializes a new instance of the <see cref="MapMembersCustomization"/> class.
        /// </summary>
        /// <param name="sourceType">Type of the source.</param>
        /// <param name="destinationType">Type of the destination.</param>
        /// <param name="mappings">The mappings.</param>
        public MapMembersCustomization(Type sourceType, Type destinationType, IEnumerable<MemberMapping> mappings)
        {
            _sourceType = sourceType;
            _destinationType = destinationType;
            _mappings = mappings;
        }

        /// <summary>
        /// Customizes the specified fixture by mapping the source members to the destination members.
        /// </summary>
        /// <param name="fixture">The fixture to customize.</param>
        public void Customize(IFixture fixture)
        {
            fixture.Customizations.Add(new MapMembersSpecimenBuilder(_sourceType, _destinationType, _mappings));
        }
    }

    /// <summary>
    /// A customization that maps the generation of specimens of the <typeparamref name="TSource"/> to the <typeparamref name="TDestination"/>
    /// given the specified <paramref name="mappings"/> collection
    /// </summary>
    /// <typeparam name="TSource">The type of the source.</typeparam>
    /// <typeparam name="TDestination">The type of the destination.</typeparam>
    public class MapMembersCustomization<TSource, TDestination> : MapMembersCustomization
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MapMembersCustomization{TSource, TDestination}"/> class.
        /// </summary>
        /// <param name="mappings">The mappings.</param>
        public MapMembersCustomization(IEnumerable<MemberMapping> mappings)
            : base(typeof(TSource), typeof(TDestination), mappings)
        { }
    }

    /// <summary>
    /// Represents a relation between two members.
    /// </summary>
    public class MemberMapping
    {
        /// <summary>
        /// Gets the source member.
        /// </summary>
        /// <value>
        /// The source member.
        /// </value>
        public MemberInfo SourceMember { get; private set; }

        /// <summary>
        /// Gets the destination member.
        /// </summary>
        /// <value>
        /// The destination member.
        /// </value>
        public MemberInfo DestinationMember { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="MemberMapping"/> class.
        /// </summary>
        /// <param name="sourceMember">The source member.</param>
        /// <param name="destinationMember">The destination member.</param>
        public MemberMapping(MemberInfo sourceMember, MemberInfo destinationMember)
        {
            SourceMember = sourceMember;
            DestinationMember = destinationMember;
        }
    }
}