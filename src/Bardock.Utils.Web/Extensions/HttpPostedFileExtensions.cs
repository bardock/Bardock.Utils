using System.IO;
using System.Runtime.CompilerServices;
using System.Web;

namespace Bardock.Utils.Web.Extensions
{
	public static class HttpPostedFileExtensions
	{
        public static byte[] ReadAllBytes(this HttpPostedFileBase file)
		{
			byte[] buffer = new byte[16 * 1024];
			using (var ms = new MemoryStream()) {
				int read = file.InputStream.Read(buffer, 0, buffer.Length);
				while (read > 0) {
					ms.Write(buffer, 0, read);
					read = file.InputStream.Read(buffer, 0, buffer.Length);
				}
                var bytes = ms.ToArray();
                //Reset stream position for consecutive reads
				file.InputStream.Position = 0;
				return bytes;
			}
		}

	}

}