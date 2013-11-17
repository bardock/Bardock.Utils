using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Linq;
using Bardock.Utils.Collections;
using Bardock.Utils.Collections.Extensions;
using Xunit;

namespace Bardock.Utils.Tests.Collections.Extensions
{
    public class IEnumerableExtensionsTest
	{
        [Fact]
        public void Split_Empty()
		{
            var empty = Coll.Array<int>();
            var r = empty.Split(x => x == 1);

            Assert.NotNull(r);
            Assert.Equal(1, r.Count());
            Assert.Empty(r.ElementAt(0));
		}

        [Fact]
        public void Split_EmptyCleared()
        {
            var empty = Coll.Array<int>();
            var r = empty.Split(x => x == 1, clearEmpty: true);

            Assert.NotNull(r);
            Assert.Empty(r);
        }

        [Fact]
        public void Split()
        {
            var list = Coll.Array(1, 2, 2, 3, 4, 5, 6, 7, 8);
            var r = list.Split(x => x == 2 || x == 4 || x == 8, clearEmpty: true);

            Assert.NotNull(r);
            Assert.Equal(3, r.Count());

            Assert.Equal(1, r.ElementAt(0).Count());
            Assert.Equal(1, r.ElementAt(0).ElementAt(0));

            Assert.Equal(1, r.ElementAt(1).Count());
            Assert.Equal(3, r.ElementAt(1).ElementAt(0));

            Assert.Equal(3, r.ElementAt(2).Count());
            Assert.Equal(5, r.ElementAt(2).ElementAt(0));
            Assert.Equal(6, r.ElementAt(2).ElementAt(1));
            Assert.Equal(7, r.ElementAt(2).ElementAt(2));
        }
	}

}