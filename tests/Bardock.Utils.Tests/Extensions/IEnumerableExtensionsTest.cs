using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Linq;
using Bardock.Utils.Collections;
using Bardock.Utils.Extensions;
using Xunit;

namespace Bardock.Utils.Tests.Extensions
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
        public void Split_Empty_ClearEmpty()
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
            var r = list.Split(x => x == 2 || x == 4 || x == 8);

            Assert.NotNull(r);
            Assert.Equal(5, r.Count());

            Assert.Equal(1, r.ElementAt(0).Count());
            Assert.Equal(1, r.ElementAt(0).ElementAt(0));

            Assert.Equal(0, r.ElementAt(1).Count());

            Assert.Equal(1, r.ElementAt(2).Count());
            Assert.Equal(3, r.ElementAt(2).ElementAt(0));

            Assert.Equal(3, r.ElementAt(3).Count());
            Assert.Equal(5, r.ElementAt(3).ElementAt(0));
            Assert.Equal(6, r.ElementAt(3).ElementAt(1));
            Assert.Equal(7, r.ElementAt(3).ElementAt(2));

            Assert.Equal(0, r.ElementAt(4).Count());
        }

        [Fact]
        public void Split_ClearEmpty()
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

        [Fact]
        public void ContainsAny()
        {
            var list = Coll.Array(1, 2, 3, 4, 5, 6, 7, 8);

            Assert.True(list.ContainsAny(new int[] { 1, 3, 5 }));
            Assert.True(list.ContainsAny(new int[] { 4, 13, 15 }));
            Assert.False(list.ContainsAny(new int[] { 9, 15, 22 }));
        }

        [Fact]
        public void Where_If()
        {
            var list = Coll.Array(1, 2, 3, 4, 5, 6, 7, 8);
            var condition = true;

            Assert.Equal(4, list.Where(condition, x => x % 2 == 0).Count());
            Assert.Equal(list.Count(), list.Where(!condition, x => x % 2 == 0).Count());
        }

        [Fact]
        public void Where_IfElse()
        {
            var list = Coll.Array(1, 2, 3, 4, 5, 6, 7, 8);
            var condition = true;

            Assert.Equal(4, list.Where(condition, x => x % 2 == 0, x => x % 3 == 0).Count());
            Assert.Equal(2, list.Where(!condition, x => x % 2 == 0, x => x % 3 == 0).Count());
        }

        [Fact]
        public void ForEach_SingleParameter()
        {
            var list = Coll.Array(1, 2, 3, 4, 5, 6, 7, 8);
            var acc = 0;
            list.ForEach((i) => 
            {
                acc += i;
            });

            Assert.Equal(list.Sum(), acc);
        }
	}
}