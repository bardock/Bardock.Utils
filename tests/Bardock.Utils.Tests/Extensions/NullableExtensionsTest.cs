using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Bardock.Utils.Extensions;
using Bardock.Utils.Globalization;

namespace Bardock.Utils.Tests.Extensions
{
    public class NullableExtensionsTest
    {
        [Fact]
        public void ApplyOrDefault_Null_NullableStruct()
        {
            int? nullable = null;
            var r = nullable.ApplyOrDefault((int x) => (int?)(x * 2));
            Assert.Equal(null, r);
        }

        [Fact]
        public void ApplyOrDefault_Null_NullableIntToDefault()
        {
            int? nullable = null;
            var r = nullable.ApplyOrDefault((int x) => x * 2);
            Assert.Equal(0, r);
        }

        [Fact]
        public void ApplyOrDefault_Valued_NullableStruct()
        {
            int? nullable = 1;
            var r = nullable.ApplyOrDefault((int x) => x * 2);
            Assert.Equal(2, r);
        }

        [Fact]
        public void ApplyOrDefault_Null_NullableClass()
        {
            Object nullable = null;
            var r = nullable.ApplyOrDefault(x => "ok");
            Assert.Equal(null, r);
        }

        [Fact]
        public void ApplyOrDefault_Valued_NullableClass()
        {
            Object nullable = new Object();
            var r = nullable.ApplyOrDefault(x => "ok");
            Assert.Equal("ok", r);
        }
    }
}
