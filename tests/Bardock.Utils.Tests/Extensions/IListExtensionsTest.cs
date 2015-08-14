using Bardock.Utils.Collections;
using Bardock.Utils.Extensions;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Bardock.Utils.Tests.Extensions
{
    public class IListExtensionsTest
    {
       
        [Fact]
        public void AddItem_NotNullValue_ShouldContainItem()
        {
            // Setup
            var sut = (IList<int>)new List<int>() { 1, 2, 3, 4, 5, 6, 7 };
            var item = 8;

            // Exercise
            sut = sut.AddItem(item);
            var lastIndex = sut.Count() > 0
                            ? sut.Count() - 1
                            : 0;
            //Verify
            Assert.True(sut.Contains(item));
            Assert.True(sut.ElementAt(lastIndex) == item);
        }

        [Fact]
        public void InsertItem_NotNullValue_ShouldContainItemInSpecificIndex()
        {
            // Setup
            var sut = (IList<int>) new List<int>() { 1, 2, 3, 4, 5, 6, 7 };
            var item = 8;
            var index = 1;

            // Exercise
            sut = sut.InsertItem(item, index);
            
            //Verify
             Assert.True(sut.ElementAt(index) == item);
        }

      
    }
}