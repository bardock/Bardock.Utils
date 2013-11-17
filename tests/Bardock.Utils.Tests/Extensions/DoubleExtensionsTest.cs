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
    public class DoubleExtensionsTest
    {
        private const decimal DEC1 = 123456.789M;

        [Fact]
        public void ToInvariantString()
        {
            var r = DEC1.ToInvariantString();
            Assert.Equal("123456.789", r);
        }
    }
}
