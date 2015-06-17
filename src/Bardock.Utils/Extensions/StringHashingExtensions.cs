using System.Security.Cryptography;
using System.Text;

namespace Bardock.Utils.Extensions
{
    public static class StringHashingExtensions
    {
        /// <summary>
        /// Creates a hash using MD5 algorithm and specified encoding (UTF8 by default)
        /// </summary>
        public static byte[] GetHash(this string inputString, Encoding enc = null)
        {
            if (enc == null)
                enc = Encoding.UTF8;

            HashAlgorithm algorithm = MD5.Create();
            return algorithm.ComputeHash(enc.GetBytes(inputString));
        }

        /// <summary>
        /// Creates a hash using MD5 algorithm, and specified encodings for input (UTF8 by default) and output strings (lower hexa by default)
        /// </summary>
        public static string GetHashString(this string inputString, Encoding inputEncoding = null, Encoding outputEncoding = null)
        {
            var hash = inputString.GetHash(inputEncoding);
            if (outputEncoding == null)
                return hash.ToHexa();
            else
                return outputEncoding.GetString(hash);
        }
    }
}