using System;
using System.Collections.Generic;
using Xunit;
using System.Linq;
using Bardock.Utils.Collections;

namespace Bardock.Utils.Tests.Collections
{
	public class DisposableListTest
	{
        private class MyDisposable : IDisposable
        {
            public bool IsDisposed { get; private set; }

            public void Dispose()
            {
                IsDisposed = true;
            }
        }

        [Fact]
        public void Dispose()
        {
            var list = new DisposableList<MyDisposable>();

            Assert.True(list.All(x => !x.IsDisposed));

            list.Dispose();

            Assert.True(list.All(x => x.IsDisposed));
        }
	}
}