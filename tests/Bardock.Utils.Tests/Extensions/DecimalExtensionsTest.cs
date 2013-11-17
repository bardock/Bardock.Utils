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
    public class DecimalExtensionsTest
    {
        private const decimal DEC1 = 123456.789M;

        [Fact]
        public void ToCurrencyString()
        {
            using (var c = new ContextCulture())
            {
                var r = DEC1.ToCurrencyString();
                Assert.Equal("123,456.79", r);
            }
        }

        [Fact]
        public void ToCurrencyInputString()
        {
            using (var c = new ContextCulture())
            {
                var r = DEC1.ToCurrencyInputString();
                Assert.Equal("123456.79", r);
            }
        }
    }
}
