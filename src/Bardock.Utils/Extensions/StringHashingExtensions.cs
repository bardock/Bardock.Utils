using System.Security.Cryptography;
using System.Text;

namespace Bardock.Utils.Extensions
{
    public static class StringHashingExtensions
    {
        public static byte[] GetHash(this string inputString)
        {
            HashAlgorithm algorithm = MD5.Create();
            return algorithm.ComputeHash(Encoding.UTF8.GetBytes(inputString));
        }

        public static string GetHashString(this string inputString)
        {
            StringBuilder sb = new StringBuilder();
            foreach (byte b in GetHash(inputString))
                sb.Append(b.ToString("X2"));

            return sb.ToString();
        }
    }
}






