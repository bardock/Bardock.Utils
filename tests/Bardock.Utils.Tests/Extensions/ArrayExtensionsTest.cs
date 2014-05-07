using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Linq;
using Bardock.Utils.Collections;
using Bardock.Utils.Extensions;
using Xunit;

namespace Bardock.Utils.Tests.Extensions
{
    public class ArrayExtensionsTest
	{
        [Fact]
        public void ForEach_MultipleParameters()
        {
            var list = Coll.Array(1, 2, 3, 4);
            var acc = 0;
            list.ForEach((i, o) => 
            {
                acc += (i * o);
            });

            Assert.Equal(20, acc);
        }
	}
}