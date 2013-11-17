using System.Security.Cryptography;
using System.Text;

namespace Bardock.Utils.Extensions
{
    public static class StringHashingExtensions
    {
        /// <summary>
        /// Creates a hash using MD5 algorithm and UTF8 encoding for input string
        /// </summary>
        public static byte[] GetHash(this string inputString)
        {
            HashAlgorithm algorithm = MD5.Create();
            return algorithm.ComputeHash(Encoding.UTF8.GetBytes(inputString));
        }

        /// <summary>
        /// Creates a hash using MD5 algorithm, UTF8 encoding for input string and hexadecimal for output string
        /// </summary>
        public static string GetHashString(this string inputString)
        {
            StringBuilder sb = new StringBuilder();
            foreach (byte b in GetHash(inputString))
                sb.Append(b.ToString("X2"));

            return sb.ToString();
        }
    }
}






