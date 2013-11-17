using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Bardock.Utils.Extensions;
using Bardock.Utils.Globalization;
using System.IO;

namespace Bardock.Utils.Tests.Extensions
{
    public class StreamExtensionsTest
    {    
        [Fact]
        public void ReadAllBytes()
        {
            var bytes = new byte[] {1, 0, 0, 1, 0, 1, 0, 0};
            var s = new MemoryStream(bytes);
            var r = s.ReadAllBytes();
            Assert.Equal(bytes, r);
        }

        [Fact]
        public void ReadAllBytes_SeekEnd()
        {
            var bytes = new byte[] { 1, 0, 0, 1, 0, 1, 0, 0 };
            var s = new MemoryStream(bytes);
            s.Seek(bytes.Length, SeekOrigin.Begin);
            var r = s.ReadAllBytes();
            Assert.Empty(r);
        }
    }
}
