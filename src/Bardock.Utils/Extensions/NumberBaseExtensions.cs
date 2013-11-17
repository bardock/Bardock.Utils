using System;

namespace Bardock.Utils.Extensions
{
    public static class NumberBaseExtensions
    {
        /// <summary>
        /// Converts the given decimal number to the numeral system with the
        /// specified base (in the range [2, 36]).
        /// </summary>
        /// <param name="decimalNumber">The number to convert.</param>
        /// <param name="numBase">The radix of the destination numeral system (in the range [2, 36]).</param>
        /// <returns></returns>
        public static string ConvertBase(this int decimalNumber, int numBase)
        {
            return ((long)decimalNumber).ConvertBase(numBase);
        }

        /// <summary>
        /// Converts the given decimal number to the numeral system with the
        /// specified base (in the range [2, 36]).
        /// </summary>
        /// <param name="decimalNumber">The number to convert.</param>
        /// <param name="numBase">The radix of the destination numeral system (in the range [2, 36]).</param>
        /// <returns></returns>
        public static string ConvertBase(this long decimalNumber, int numBase)
        {
            const int BitsInLong = 64;
            const string Digits = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";

            if (numBase < 2 || numBase > Digits.Length)
                throw new ArgumentException("The radix must be >= 2 and <= " + Digits.Length.ToString());

            if (decimalNumber == 0)
                return "0";

            int index = BitsInLong - 1;
            long currentNumber = Math.Abs(decimalNumber);
            char[] charArray = new char[BitsInLong];

            while (currentNumber != 0)
            {
                int remainder = (int)(currentNumber % numBase);
                charArray[index--] = Digits[remainder];
                currentNumber = currentNumber / numBase;
            }

            string result = new String(charArray, index + 1, BitsInLong - index - 1);
            if (decimalNumber < 0)
            {
                result = "-" + result;
            }

            return result;
        }
    }
}
