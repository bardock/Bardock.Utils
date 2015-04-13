using System.IO;

namespace Bardock.Utils.Extensions
{
    public static class StreamExtensions
    {
        public static byte[] ReadAllBytes(this Stream input)
        {
            byte[] buffer = new byte[16 * 1024];
            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }
        }

        public static string ReadAllString(this Stream input)
        {
            using (var reader = new StreamReader(input))
            {
                return reader.ReadToEnd();
            }
        }
    }
}