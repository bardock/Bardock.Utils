using AutoMapper;
using Bardock.Utils.UnitTest.AutoFixture.Customizations;
using Bardock.Utils.UnitTest.AutoFixture.Xunit2.Attributes;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.Xunit2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bardock.Utils.UnitTest.AutoFixture.Xunit2.AutoMapper.Attributes
{
    public class ToAttribute : Bardock.Utils.UnitTest.AutoFixture.Xunit2.Attributes.ToAttribute
    {
        public ToAttribute(Type destinationType)
            : base(destinationType)
        { }

        public override MemberMappingBuilder Configure(Type sourceType, Type destinationType)
        {
            var props = Mapper.GetAllTypeMaps()
                            .Where(m => m.SourceType == sourceType)
                            .Where(m => m.DestinationType == destinationType)
                            .SelectMany(m => m.GetPropertyMaps());

            var builder = new MemberMappingBuilder();
            foreach (var p in props)
            {
                builder = builder.Map(p.SourceMember, p.DestinationProperty.MemberInfo);
            }

            return builder;
        }
    }
}
