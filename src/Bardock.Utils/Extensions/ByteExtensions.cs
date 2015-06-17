using System.Text;

namespace Bardock.Utils.Extensions
{
    public static class ByteExtensions
    {
        public static string ToHexa(this byte b, bool upperCase = false)
        {
            return b.ToString(upperCase ? "X2" : "x2");
        }

        public static string ToHexa(this byte[] bytes, bool upperCase = false)
        {
            StringBuilder sb = new StringBuilder();
            foreach (byte b in bytes)
                sb.Append(b.ToHexa(upperCase));
            return sb.ToString();
        }

        public static string Encode(this byte[] bytes, Encoding enc)
        {
            return enc.GetString(bytes);
        }
    }
}