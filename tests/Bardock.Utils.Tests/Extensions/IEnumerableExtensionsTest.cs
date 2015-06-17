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

        public class WhereInDTO
        {
            public int Property1 { get; set; }
            public string Property2 { get; set; }
            public int? Property3 { get; set; }

            public WhereInDTO(int property1, string property2, int? property3)
            {
                Property1 = property1;
                Property2 = property2;
                Property3 = property3;
            }
        }

        [Fact]
        public void Where_In_TProp_TPropItems_ReturnDTO1()
        {
            var dto1 = new WhereInDTO(1, "zzz", 1);
            var dto2 = new WhereInDTO(2, "aaa", null);
            var dto3 = new WhereInDTO(3, "hhh", 3);

            var items = Enumerable.Range(1, 1);

            var query = Coll.Array(dto1, dto2, dto3);

            var res = query.Where(x => x.Property1, items);

            Assert.Equal(dto1, res.ElementAt(0));
            Assert.Equal(1, res.Count());

        }

        [Fact]
        public void Where_In_TProp_TPropItems_NullItems_ReturnsAll()
        {
            var dto1 = new WhereInDTO(1, "zzz", 1);
            var dto2 = new WhereInDTO(2, "aaa", null);
            var dto3 = new WhereInDTO(3, "hhh", 3);

            IEnumerable<int> items = null;

            var query = Coll.Array(dto1, dto2, dto3);

            var res = query.Where(x => x.Property1, items);

            Assert.Equal(dto1, res.ElementAt(0));
            Assert.Equal(dto2, res.ElementAt(1));
            Assert.Equal(dto3, res.ElementAt(2));
        }

        [Fact]
        public void Where_In_TPropNullable_TPropNullableItems_ReturnDTO1()
        {
            var dto1 = new WhereInDTO(1, "zzz", 1);
            var dto2 = new WhereInDTO(2, "aaa", null);
            var dto3 = new WhereInDTO(3, "hhh", 3);

            var items = Enumerable.Range(1, 1).Cast<int?>();

            var query = Coll.Array(dto1, dto2, dto3);

            var res = query.Where(x => x.Property3, items);

            Assert.Equal(dto1, res.ElementAt(0));
            Assert.Equal(1, res.Count());
        }

        [Fact]
        public void Where_In_TPropNullable_TPropNullableItems_NullItems_ReturnsAll()
        {
            var dto1 = new WhereInDTO(1, "zzz", 1);
            var dto2 = new WhereInDTO(2, "aaa", null);
            var dto3 = new WhereInDTO(3, "hhh", 3);

            IEnumerable<int?> items = null;

            var query = Coll.Array(dto1, dto2, dto3);

            var res = query.Where(x => x.Property3, items);

            Assert.Equal(dto1, res.ElementAt(0));
            Assert.Equal(dto2, res.ElementAt(1));
            Assert.Equal(dto3, res.ElementAt(2));
        }

        [Fact]
        public void Where_In_TPropNullable_TPropItems_ReturnsDTO1()
        {
            var dto1 = new WhereInDTO(1, "zzz", 1);
            var dto2 = new WhereInDTO(2, "aaa", null);
            var dto3 = new WhereInDTO(3, "hhh", 3);

            var items = Enumerable.Range(1, 1);

            var query = Coll.Array(dto1, dto2, dto3);

            var res = query.Where(x => x.Property3, items);

            Assert.Equal(dto1, res.ElementAt(0));
            Assert.Equal(1, res.Count());
        }

        [Fact]
        public void Where_In_TPropNullable_TPropItems_NullItems_ReturnsAll()
        {
            var dto1 = new WhereInDTO(1, "zzz", 1);
            var dto2 = new WhereInDTO(2, "aaa", null);
            var dto3 = new WhereInDTO(3, "hhh", 3);

            IEnumerable<int> items = null;

            var query = Coll.Array(dto1, dto2, dto3);

            var res = query.Where(x => x.Property3, items);

            Assert.Equal(dto1, res.ElementAt(0));
            Assert.Equal(dto2, res.ElementAt(1));
            Assert.Equal(dto3, res.ElementAt(2));
        }

        [Fact]
        public void Where_In_TProp_TPropNullableItems_ReturnsDTO1()
        {
            var dto1 = new WhereInDTO(1, "zzz", 1);
            var dto2 = new WhereInDTO(2, "aaa", null);
            var dto3 = new WhereInDTO(3, "hhh", 3);

            var items = Enumerable.Range(1, 1).Cast<int?>();

            var query = Coll.Array(dto1, dto2, dto3);

            var res = query.Where(x => x.Property1, items);

            Assert.Equal(dto1, res.ElementAt(0));
            Assert.Equal(1, res.Count());
        }

        [Fact]
        public void Where_In_TProp_TPropNullableItems_NullItems_ReturnsAll()
        {
            var dto1 = new WhereInDTO(1, "zzz", 1);
            var dto2 = new WhereInDTO(2, "aaa", null);
            var dto3 = new WhereInDTO(3, "hhh", 3);

            IEnumerable<int?> items = null;

            var query = Coll.Array(dto1, dto2, dto3);

            var res = query.Where(x => x.Property1, items);

            Assert.Equal(dto1, res.ElementAt(0));
            Assert.Equal(dto2, res.ElementAt(1));
            Assert.Equal(dto3, res.ElementAt(2));
        }
	}
}