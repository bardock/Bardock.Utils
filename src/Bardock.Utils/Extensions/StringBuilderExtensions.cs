using System.Text;

namespace Bardock.Utils.Extensions
{
    public static class StringBuilderExtensions
    {
        public static StringBuilder RemoveLastCharacter(this StringBuilder sb)
        {
            return sb.RemoveLastCharacters(1);
        }

        public static StringBuilder RemoveLastCharacters(this StringBuilder sb, int num)
        {
            sb.Length -= num;
            return sb;
        }
    }
}
