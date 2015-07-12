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
            var list = new DisposableList<IDisposable>();
            list.AddRange(items);

            //Exercise
            list.Dispose();

            //Verify
            list.Should().NotBeEmpty();
            list.ForEach(item => Mock.Get(item).Verify(x => x.Dispose()));
        }

        [Theory, AutoMoqData]
        public void Dispose_EmptyList_ShouldDoNothing()
        {
            //Setup
            var list = new DisposableList<IDisposable>();

            //Exercise
            list.Dispose();

            //Verify
            list.Should().BeEmpty();
        }
    }
}