using System.Linq;
using System.Text;

namespace Bardock.Utils.Extensions
{
    public static class StringNormalizeExtensions
    {
        public static string SEONormalize(int id, string title, int maxLength = 60)
        {
            return SEONormalize(string.Format("{0}-{1}", id, title), maxLength);
        }

        public static string SEONormalize(this string title, int maxLength = 60)
        {
            if (string.IsNullOrWhiteSpace(title))
                return string.Empty;

            title = title.Trim();

            int len = title.Length;
            bool prevdash = false;
            var sb = new StringBuilder(len);

            for (int i = 0; i < len && i < maxLength; i++)
            {
                char c = title[i];
                if (c.IsAlphaNumeric())
                {
                    sb.Append(c.ToLower());
                    prevdash = false;
                }
                else if (c.In('´','’'))
                {
                    sb.Append('\'');
                    prevdash = false;
                }
                else if (c.In(' ', ',', '.', '/', '\\', '-', '_', '='))
                {
                    if (!prevdash && sb.Length > 0)
                    {
                        sb.Append('-');
                        prevdash = true;
                    }
                }
                else if ((int)c >= 128)
                {
                    var fc = c.FoldDiacritical().ToLower();
                    if (fc.IsValidUriPathSegment())
                    {
                        sb.Append(fc);
                        prevdash = false;
                    }
                }
            }
            if (prevdash)
                sb.RemoveLastCharacter();

            return sb.ToString();
        }

        public static string FoldDiacriticals(this string str)
        {
            return new string(
                str
                    .ToCharArray()
                    .Select(x => x.FoldDiacritical())
                    .ToArray()
            );
        }
    }
}
