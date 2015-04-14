using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Linq;
using Bardock.Utils.Collections;
using Bardock.Utils.Extensions;
using Bardock.Utils.Linq.Expressions;
using Xunit;

namespace Bardock.Utils.Tests.Extensions
{
    public class IQueryableExtensionsTest
	{     
        [Fact]
        public void Where_If_Queryable()
        {
            var query = Coll.Array(1, 2, 3, 4, 5, 6, 7, 8).AsQueryable();
            var condition = true;

            Assert.Equal(4, query.Where(condition, x => x % 2 == 0).Count());
            Assert.Equal(query.Count(), query.Where(!condition, x => x % 2 == 0).Count());
        }

        [Fact]
        public void Where_IfElse_Queryable()
        {
            var query = Coll.Array(1, 2, 3, 4, 5, 6, 7, 8).AsQueryable();
            var condition = true;

            Assert.Equal(4, query.Where(condition, x => x % 2 == 0, x => x % 3 == 0).Count());
            Assert.Equal(2, query.Where(!condition, x => x % 2 == 0, x => x % 3 == 0).Count());
        }

        [Fact]
        public void WhereBetween()
        {
            var query = Coll.Array(
                DateTime.Today.AddMonths(-2), 
                DateTime.Today,
                DateTime.Today.AddMonths(2)
            ).AsQueryable();

            var queryFiltered = query.WhereBetween(x => x, DateTime.Today.AddMonths(-1), DateTime.Today.AddMonths(1));

            Assert.Equal(1, queryFiltered.Count());
            Assert.True(queryFiltered.Contains(query.ElementAt(1)));
            Assert.False(queryFiltered.ContainsAny(query.ElementAt(0), query.ElementAt(2)));
        }

        [Fact]
        public void WhereBetween_From()
        {
            var query = Coll.Array(
                DateTime.Today.AddMonths(-2),
                DateTime.Today,
                DateTime.Today.AddMonths(2)
            ).AsQueryable();

            var queryFiltered = query.WhereBetween(x => x, fromDate: DateTime.Today.AddMonths(-1), toDate: null);

            Assert.Equal(2, queryFiltered.Count());
            Assert.True(queryFiltered.Contains(query.ElementAt(1)));
            Assert.True(queryFiltered.Contains(query.ElementAt(2)));
            Assert.False(queryFiltered.ContainsAny(query.ElementAt(0)));
        }

        [Fact]
        public void WhereBetween_To()
        {
            var query = Coll.Array(
                DateTime.Today.AddMonths(-2),
                DateTime.Today,
                DateTime.Today.AddMonths(2)
            ).AsQueryable();

            var queryFiltered = query.WhereBetween(x => x, fromDate: null, toDate: DateTime.Today.AddMonths(1));

            Assert.Equal(2, queryFiltered.Count());
            Assert.True(queryFiltered.Contains(query.ElementAt(0)));
            Assert.True(queryFiltered.Contains(query.ElementAt(1)));
            Assert.False(queryFiltered.ContainsAny(query.ElementAt(2)));
        }

        public class OrderByPropertyDTO 
        {
            public int Property1 { get; set; }
            public string Property2 { get; set; }
            public DateTime Property3 { get; set; }
            public OrderByPropertyDTO(int a, string b, DateTime c)
            {
                this.Property1 = a;
                this.Property2 = b;
                this.Property3 = c;
            }
        }

        [Fact]
        public void OrderByProperty_asc()
        {
            var dto1 = new OrderByPropertyDTO(1, "zzz", DateTime.Today);
            var dto2 = new OrderByPropertyDTO(2, "aaa", DateTime.Today.AddMonths(1));
            var dto3 = new OrderByPropertyDTO(3, "hhh", DateTime.Today.AddMonths(-1));

            var query = Coll.Array(dto1, dto2, dto3).AsQueryable();

            query = query.OrderByProperty("Property1", true);
            Assert.Equal(dto1.Property1, query.ElementAt(0).Property1);
            Assert.Equal(dto2.Property1, query.ElementAt(1).Property1);
            Assert.Equal(dto3.Property1, query.ElementAt(2).Property1);

            query = query.OrderByProperty("Property2", true);
            Assert.Equal(dto2.Property2, query.ElementAt(0).Property2);
            Assert.Equal(dto3.Property2, query.ElementAt(1).Property2);
            Assert.Equal(dto1.Property2, query.ElementAt(2).Property2);

            query = query.OrderByProperty("Property3", true);
            Assert.Equal(dto3.Property3, query.ElementAt(0).Property3);
            Assert.Equal(dto1.Property3, query.ElementAt(1).Property3);
            Assert.Equal(dto2.Property3, query.ElementAt(2).Property3);
        }

        [Fact]
        public void OrderByProperty_desc()
        {
            var dto1 = new OrderByPropertyDTO(1, "zzz", DateTime.Today);
            var dto2 = new OrderByPropertyDTO(2, "aaa", DateTime.Today.AddMonths(1));
            var dto3 = new OrderByPropertyDTO(3, "hhh", DateTime.Today.AddMonths(-1));

            var query = Coll.Array(dto1, dto2, dto3).AsQueryable();

            query = query.OrderByProperty("Property1", false);
            Assert.Equal(dto1.Property1, query.ElementAt(2).Property1);
            Assert.Equal(dto2.Property1, query.ElementAt(1).Property1);
            Assert.Equal(dto3.Property1, query.ElementAt(0).Property1);

            query = query.OrderByProperty("Property2", false);
            Assert.Equal(dto2.Property2, query.ElementAt(2).Property2);
            Assert.Equal(dto3.Property2, query.ElementAt(1).Property2);
            Assert.Equal(dto1.Property2, query.ElementAt(0).Property2);

            query = query.OrderByProperty("Property3", false);
            Assert.Equal(dto3.Property3, query.ElementAt(2).Property3);
            Assert.Equal(dto1.Property3, query.ElementAt(1).Property3);
            Assert.Equal(dto2.Property3, query.ElementAt(0).Property3);
        }

        public class SearchDTO
        {
            public string Property1 { get; set; }
            public string Property2 { get; set; }
            public string Property3 { get; set; }

            public SearchDTO(string property1, string property2, string property3)
            {
                Property1 = property1;
                Property2 = property2;
                Property3 = property3;
            }
        }

        [Fact]
        public void Search()
        {
            var dto1 = new SearchDTO("Isis", "Castañeda", "Finantial");
            var dto2 = new SearchDTO("Helenia", "Caro", "Online");
            var dto3 = new SearchDTO("Humberto", "Carmona", "Finantial");

            var query = Coll.Array(dto1, dto2, dto3).AsQueryable();

            var result = query.Search("isis", x => x.Property1).ToList();
            Assert.Equal(1, result.Count());
            Assert.Equal(dto1.Property1, result.First().Property1);

            result = query.Search("isis casta", x => x.Property1 + " " + x.Property2).ToList();
            Assert.Equal(1, result.Count());
            Assert.Equal(dto1.Property1, result.First().Property1);
        }
	}
}