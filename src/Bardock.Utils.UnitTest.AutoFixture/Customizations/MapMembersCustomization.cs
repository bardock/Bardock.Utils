using Ploeh.AutoFixture;
using Ploeh.AutoFixture.Kernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Bardock.Utils.UnitTest.AutoFixture.Customizations
{
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

    /// <summary>
    /// TODO
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
    ///
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
    /// TODO
    /// </summary>
    public class MapMembersSpecimenBuilder : ISpecimenBuilder
    {
        private Type _sourceType;
        private Type _destinationType;
        private IEnumerable<MemberMapping> _mappings;

        /// <summary>
        /// Initializes a new instance of the <see cref="MapMembersSpecimenBuilder"/> class.
        /// </summary>
        /// <param name="sourceType">Type of the source.</param>
        /// <param name="destinationType">Type of the destination.</param>
        /// <param name="mappings">The mappings.</param>
        public MapMembersSpecimenBuilder(
            Type sourceType,
            Type destinationType,
            IEnumerable<MemberMapping> mappings)
        {
            _sourceType = sourceType;
            _destinationType = destinationType;
            _mappings = mappings;
        }

        /// <summary>
        /// Creates a new specimen based on a request.
        /// </summary>
        /// <param name="request">The request that describes what to create.</param>
        /// <param name="context">A context that can be used to create other specimens.</param>
        /// <returns>
        /// A valid specimen for the requested member if possible; otherwise a <see cref="T:Ploeh.AutoFixture.Kernel.NoSpecimen" /> instance.
        /// </returns>
        public object Create(object request, ISpecimenContext context)
        {
            if (_mappings == null || !_mappings.Any())
            {
                return new NoSpecimen(request);
            }

            var mi = request as MemberInfo;
            if (mi == null || mi.DeclaringType != _sourceType)
            {
                return new NoSpecimen(request);
            }

            var mapping = _mappings
                        .Where(p => p.SourceMember == mi)
                        .FirstOrDefault();

            if (mapping == null)
            {
                return new NoSpecimen(request);
            }

            return context.Resolve(mapping.DestinationMember);
        }
    }
}