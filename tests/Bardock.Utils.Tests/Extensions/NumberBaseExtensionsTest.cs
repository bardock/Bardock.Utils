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
    public class NumberBaseExtensionsTest
    {
        [Fact]
        public void ConvertBase_0()
        {
            Assert.Throws<ArgumentException>(
                () => 0.ConvertBase(0)
            );
        }

        [Fact]
        public void ConvertBase_1()
        {
            Assert.Throws<ArgumentException>(
                () => 0.ConvertBase(1)
            );
        }

        [Fact]
        public void ConvertBase_37()
        {
            Assert.Throws<ArgumentException>(
                () => 0.ConvertBase(37)
            );
        }

        [Fact]
        public void ConvertBase_NumberZero()
        {
            var r1 = 0.ConvertBase(2);
            var r2 = 0.ConvertBase(3);
            var r3 = 0.ConvertBase(4);
            var r4 = 0.ConvertBase(7);
            var r5 = 0.ConvertBase(9);
            var r6 = 0.ConvertBase(15);
            var r7 = 0.ConvertBase(36);
            Assert.Equal("0", r1);
            Assert.Equal("0", r2);
            Assert.Equal("0", r3);
            Assert.Equal("0", r4);
            Assert.Equal("0", r5);
            Assert.Equal("0", r6);
            Assert.Equal("0", r7);
        }

        [Fact]
        public void ConvertBase_2()
        {
            var r = 123.ConvertBase(2);
            Assert.Equal("1111011", r);
        }

        [Fact]
        public void ConvertBase_10()
        {
            var r = 123.ConvertBase(10);
            Assert.Equal("123", r);
        }

        [Fact]
        public void ConvertBase_16()
        {
            var r = 123.ConvertBase(16);
            Assert.Equal("7B", r);
        }

        [Fact]
        public void ConvertBase_36()
        {
            var r = 987356456345245123.ConvertBase(36);
            Assert.Equal("7I1WNI87PJGJ", r);
        }
    }
}
