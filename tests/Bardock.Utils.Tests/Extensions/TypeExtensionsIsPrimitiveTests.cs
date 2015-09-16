using System;
using Bardock.Utils.Extensions;
using FluentAssertions;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using System.Collections;

namespace Bardock.Utils.Tests.Extensions
{
    public class TypeExtensionsIsPrimitiveTests
    {
        [Theory]
        [InlineData(typeof(byte))]
        [InlineData(typeof(bool))]
        [InlineData(typeof(short))]
        [InlineData(typeof(int))]
        [InlineData(typeof(long))]
        [InlineData(typeof(ushort))]
        [InlineData(typeof(uint))]
        [InlineData(typeof(ulong))]
        [InlineData(typeof(float))]
        [InlineData(typeof(double))]
        [InlineData(typeof(decimal))]
        [InlineData(typeof(char))]
        [InlineData(typeof(string))]
        [InlineData(typeof(Enum))]
        [InlineData(typeof(Guid))]
        [InlineData(typeof(DateTime))]
        [InlineData(typeof(DateTimeOffset))]
        [InlineData(typeof(TimeSpan))]
        [InlineData(typeof(byte?))]
        [InlineData(typeof(bool?))]
        [InlineData(typeof(short?))]
        [InlineData(typeof(int?))]
        [InlineData(typeof(long?))]
        [InlineData(typeof(ushort?))]
        [InlineData(typeof(uint?))]
        [InlineData(typeof(ulong?))]
        [InlineData(typeof(float?))]
        [InlineData(typeof(double?))]
        [InlineData(typeof(decimal?))]
        [InlineData(typeof(char?))]
        [InlineData(typeof(Guid?))]
        [InlineData(typeof(DateTime?))]
        [InlineData(typeof(DateTimeOffset?))]
        [InlineData(typeof(TimeSpan?))]
        [InlineData(typeof(object))]
        public void PrimitiveType_ShouldReturnTrue(Type primitiveType)
        {
            //Exercise
            var actual = primitiveType.IsPrimitive();

            //Verify
            actual.Should().Be(true);
        }

        [Theory]
        [InlineData(typeof(IEnumerable))]
        [InlineData(typeof(ArrayList))]
        [InlineData(typeof(List<string>))]
        [InlineData(typeof(Tuple<int>))]
        [InlineData(typeof(TypeExtensionsIsPrimitiveTests))]
        public void NonPrimitiveType_ShouldReturnFalse(Type nonPrimitiveType)
        {
            //Exercise
            var actual = nonPrimitiveType.IsPrimitive();

            //Verify
            actual.Should().Be(false);
        }
    }
}
