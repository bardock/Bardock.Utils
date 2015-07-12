using Bardock.Utils.Collections;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace Bardock.Utils.Tests.Collections
{
    public class DisposableListTest
    {
        [Theory, AutoMoqData]
        public void Dispose_ListWithManyItems_ShouldDisposeAllItems(
            IEnumerable<IDisposable> items)
        {
            //Setup
            var sut = new DisposableList<IDisposable>();
            sut.AddRange(items);

            //Exercise
            sut.Dispose();

            //Verify
            sut.Should().NotBeEmpty();
            sut.ForEach(item => Mock.Get(item).Verify(x => x.Dispose()));
        }

        [Theory, AutoMoqData]
        public void Dispose_EmptyList_ShouldDoNothing()
        {
            //Setup
            var sut = new DisposableList<IDisposable>();

            //Exercise
            sut.Dispose();

            //Verify
            sut.Should().BeEmpty();
        }
    }
}