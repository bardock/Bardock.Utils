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
    public abstract class MapMembersAttribute : CustomizeAttribute
    {
        private Type _destinationType;

        public MapMembersAttribute(Type destinationType)
        {
            _destinationType = destinationType;
        }

        public override ICustomization GetCustomization(ParameterInfo parameter)
        {
            var sourceType = parameter.ParameterType;
            return new MapMembersCustomization(
                sourceType,
                _destinationType,
                Configure(sourceType, _destinationType));
        }

        public abstract IEnumerable<MemberMapping> Configure(Type sourceType, Type destinationType);
    }

    public class MembersMappingComposer<TSource, TDestination> : IEnumerable<MemberMapping>
    {
        private readonly IReadOnlyList<MemberMapping> _mappings;

        public MembersMappingComposer()
            : this(new List<MemberMapping>())
        { }

        private MembersMappingComposer(List<MemberMapping> mappings)
        {
            _mappings = mappings;
        }

        public MembersMappingComposer<TSource,TDestination> Map<TReturn>(
            Expression<Func<TSource, TReturn>> source,
            Expression<Func<TDestination, TReturn>> destination)
        {
            var mappings = _mappings.ToList();

            mappings.Add(
                new MemberMapping(
                    ((MemberExpression)ExpressionHelper.RemoveConvert(source).Body).Member,
                    ((MemberExpression)ExpressionHelper.RemoveConvert(destination).Body).Member));

            return new MembersMappingComposer<TSource,TDestination>(mappings);
        }

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