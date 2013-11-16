using Bardock.Utils.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Bardock.Utils.Tests.Collections
{
    public class CollTest
    {
        [Fact]
        public void Array()
        {
            var r = Coll.Array(1, 2, 3, 3, 5);

            Assert.Equal(5, r.Length);
            Assert.Equal(1, r[0]);
            Assert.Equal(2, r[1]);
            Assert.Equal(3, r[2]);
            Assert.Equal(3, r[3]);
            Assert.Equal(5, r[4]);
        }
    }
}
