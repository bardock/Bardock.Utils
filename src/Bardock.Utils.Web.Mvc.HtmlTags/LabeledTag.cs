using HtmlTags;

namespace Bardock.Utils.Web.Mvc.HtmlTags
{
    public class LabeledTag : HtmlTag
    {
        public string Display { get; private set; }

        public HtmlTag InnerTag { get; private set; }

        public LabeledTag(string display, HtmlTag innerTag)
            : base("label")
        {
            this.Display = display;
            this.InnerTag = innerTag;

            this.Append(innerTag).Append(new LiteralTag(display));
        }
    }
}
