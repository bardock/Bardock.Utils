using Bardock.Utils.Collections;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bardock.Utils.Extensions
{
    public static class LongExtensions
    {
        public static string ToHumanFileSize(this long bytes)
        {
            var sizes = Coll.Array("B", "KB", "MB", "GB");

            var len = bytes;
            var order = 0;

            while (len >= 1024 && order + 1 < sizes.Length)
            {
                order = order + 1;
                len = len / 1024;
            }

            return string.Format("{0:0.##} {1}", len, sizes[order]);
        }
    }
}
