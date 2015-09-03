using System;
using Bardock.Utils.Extensions;
using FluentAssertions;
using Xunit;

namespace Bardock.Utils.Tests.Extensions
{
    public class TypeExtensionsIsAssignableToGenericTypeTests
    {
        private interface IFoo<T> { }

        private class Foo<T> : IFoo<T> { }

        private class Bar : IFoo<int> { }

        [Theory]
        [InlineData(typeof(Foo<>), typeof(IFoo<>))]
        [InlineData(typeof(IFoo<>), typeof(IFoo<>))]
        [InlineData(typeof(Foo<>), typeof(Foo<>))]
        [InlineData(typeof(IFoo<int>), typeof(IFoo<>))]
        [InlineData(typeof(Foo<int>), typeof(IFoo<>))]
        [InlineData(typeof(Foo<int>), typeof(Foo<>))]
        [InlineData(typeof(Bar), typeof(IFoo<>))]
        public void ValidParams_ShouldReturnTrue(Type givenType, Type genericType)
        {
            //Exercise
            var actual = givenType.IsAssignableToGenericType(genericType);

            //Verify
            actual.Should().Be(true);
        }

        [Theory]
        [InlineData(typeof(IFoo<>), typeof(Foo<>))]
        [InlineData(typeof(IFoo<int>), typeof(Foo<>))]
        [InlineData(typeof(IFoo<int>), typeof(IFoo<object>))]
        [InlineData(typeof(IFoo<object>), typeof(IFoo<int>))]
        public void InvalidParams_ShouldReturnTrue(Type givenType, Type genericType)
        {
            //Exercise
            var actual = givenType.IsAssignableToGenericType(genericType);

            //Verify
            actual.Should().Be(false);
        }
    }
}