using AutoMapper;
using Bardock.Utils.UnitTest.AutoFixture.Customizations;
using System;
using System.Linq;

namespace Bardock.Utils.UnitTest.AutoFixture.AutoMapper.Customizations
{
    /// <summary>
    /// A customization that uses automapper mapping to map the generation of member specimens of
    /// the <paramref name="sourceType"/> to the <paramref name="destinationType"/>
    /// </summary>
    public class AutoMapMembersCustomization : MapMembersCustomization
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AutoMapMembersCustomization"/> class.
        /// </summary>
        /// <param name="sourceType">Type of the source.</param>
        /// <param name="destinationType">Type of the destination.</param>
        public AutoMapMembersCustomization(Type sourceType, Type destinationType)
            : base(
                sourceType,
                destinationType,
                mappings: Mapper.GetAllTypeMaps()
                            .Where(m => m.SourceType == sourceType)
                            .Where(m => m.DestinationType == destinationType)
                            .SelectMany(m => m.GetPropertyMaps())
                            .Select(p => new MemberMapping(p.SourceMember, p.DestinationProperty.MemberInfo)))
        { }
    }

    /// <summary>
    /// A customization that uses automapper mapping to map the generation of member specimens of
    /// the <typeparamref name="TSource"/> to the <typeparamref name="TDestination"/>
    /// </summary>
    /// <typeparam name="TSource">The type of the source.</typeparam>
    /// <typeparam name="TDestination">The type of the destination.</typeparam>
    public class AutoMapMembersCustomization<TSource, TDestination> : AutoMapMembersCustomization
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AutoMapMembersCustomization{TSource, TDestination}"/> class.
        /// </summary>
        public AutoMapMembersCustomization()
            : base(typeof(TSource), typeof(TDestination))
        { }
    }
}