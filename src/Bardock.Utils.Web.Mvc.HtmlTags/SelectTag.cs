using System;
using System.Linq;
using Bardock.Utils.Extensions;
using Bardock.Utils.Web.Mvc.HtmlTags.Extensions;
using HtmlTags;

namespace Bardock.Utils.Web.Mvc.HtmlTags
{
    public class SelectTag : HtmlTag
    {
        public SelectTag()
            : base("select")
        {
        }

        protected virtual HtmlTag BuildOption(string display, object value, Action<HtmlTag> configure = null)
        {
            var option = new HtmlTag("option").Text(display).Val(value);
            if (configure != null)
                configure(option);
            return option;
        }

        public virtual SelectTag PrependOption(
            string display,
            object value,
            Action<HtmlTag> configure = null)
        {
            var option = BuildOption(display, value, configure);
            return (SelectTag)this.Prepend(option);
        }

        public virtual SelectTag AddOption(
            string display,
            object value,
            Action<HtmlTag> configure = null)
        {
            var option = BuildOption(display, value, configure);
            return (SelectTag)this.Append(option);
        }

        public virtual SelectTag AddDefaultOption(
            string display = "",
            Action<HtmlTag> configure = null)
        {
            return this.PrependOption(display, null, configure);
        }

        public virtual SelectTag AddOptions<TItem>(
            OptionsList<TItem> options,
            object defaultValue = null)
        {
            object selectedVal = null;
            foreach (var item in options)
            {
                this.AddOption(item.Display, item.Value, item.Configure);

                if (options.IsSelected != null && item.IsSelected)
                    selectedVal = item.Value;
            }
            if (selectedVal == null)
                selectedVal = defaultValue;

            if (selectedVal != null)
                this.SelectValue(selectedVal);

            return this;
        }

        public virtual SelectTag SelectValue(
            object value,
            string format = null)
        {
            this.Children
                .Where(x => x.Selected())
                .ToList()
                .ForEach(x => x.Selected(false));

            this.Children
                .Where(x => x.ValueIsEqual(value, format)).ToList()
                .ForEach(x => x.Selected(true));

            return this;
        }
    }
}
