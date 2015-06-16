using Bardock.Utils.Linq.Expressions;
using Bardock.Utils.UnitTest.AutoFixture.Customizations;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.Xunit2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Bardock.Utils.UnitTest.AutoFixture.Xunit2.Attributes
{
    public class MemberMappingBuilder
    {
        private readonly IReadOnlyList<MemberMapping> _mappings;

        public MemberMappingBuilder()
        {
            _mappings = new List<MemberMapping>();
        }

        private MemberMappingBuilder(List<MemberMapping> mappings)
        {
            _mappings = mappings;
        }

        public MemberMappingBuilder Map<TSource, TDestination>(
            Expression<Func<TSource, object>> source,
            Expression<Func<TDestination, object>> destination)
        {
            return Map(
                ((MemberExpression)ExpressionHelper.RemoveConvert(source).Body).Member, 
                ((MemberExpression)ExpressionHelper.RemoveConvert(destination).Body).Member);
        }

        public MemberMappingBuilder Map(MemberInfo sourceMember, MemberInfo destinationMember)
        {
            var mappings = _mappings.ToList();

            mappings.Add(
                new MemberMapping(
                    sourceMember,
                    destinationMember));

            return new MemberMappingBuilder(mappings);
        }

        public IEnumerable<MemberMapping> Mappings()
        {
            return _mappings;
        }
    }

    public abstract class ToAttribute : CustomizeAttribute
    {
        private Type _destinationType;

        public ToAttribute(Type destinationType)
        {
            _destinationType = destinationType;
        }

        public override ICustomization GetCustomization(System.Reflection.ParameterInfo parameter)
        {
            var sourceType = parameter.ParameterType;
            return new PropertyMappingCustomization(
                sourceType, 
                _destinationType, 
                Configure(sourceType, _destinationType).Mappings());
        }

        public abstract MemberMappingBuilder Configure(Type sourceType, Type destinationType);
    }
}