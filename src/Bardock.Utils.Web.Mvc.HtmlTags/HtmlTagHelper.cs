using System.Collections;
using System.Linq;
using System.Web.Mvc;
using Bardock.Utils.Web.Mvc.Extensions;
using Bardock.Utils.Web.Mvc.Helpers;
using Bardock.Utils.Web.Mvc.HtmlTags.Extensions;
using HtmlTags;

namespace Bardock.Utils.Web.Mvc.HtmlTags
{
    /// <summary>
    /// Represents support for rendering HTML controls in a view
    /// </summary>
    public class HtmlTagHelper
    {
        protected HtmlHelper _htmlHelper;

        public virtual HtmlHelper HtmlHelper { get { return _htmlHelper; } }

        public HtmlTagHelper()
            : this(HtmlHelperFactory.CreateInstance())
        { }

        public HtmlTagHelper(object model)
            : this(HtmlHelperFactory.CreateInstance(model))
        { }

        public HtmlTagHelper(HtmlHelper htmlHelper) 
        {
            this._htmlHelper = htmlHelper;
        }

        public virtual HtmlTag HtmlTag(string tag, string name)
        {
            return new HtmlTag(tag).InitFor(name, _htmlHelper);
        }

        public virtual HtmlTag TextArea(string name)
        {
            return this.HtmlTag("textarea", name);
        }

        public virtual HtmlTag Input(string type, string name)
        {
            return this.HtmlTag("input", name).Type(type);
        }

        public virtual HtmlTag Input(InputType type, string name)
        {
            return Input(HtmlHelper.GetInputTypeString(type), name);
        }

        public virtual HtmlTag TextBox(string name)
        {
            return Input(InputType.Text, name);
        }

        public virtual HtmlTag Password(string name)
        {
            return Input(InputType.Password, name);
        }

        public virtual HtmlTag Hidden(string name)
        {
            return Input(InputType.Hidden, name);
        }

        public virtual HtmlTag File(string name)
        {
            return Input("file", name);
        }

        public virtual HtmlTag Radio(string name, bool isChecked = false)
        {
            return Input(InputType.Radio, name).Checked(isChecked);
        }

        public virtual HtmlTag CheckBox(string name, bool isChecked = false)
        {
            return Input(InputType.CheckBox, name).Checked(isChecked);
        }

        public virtual SelectTag Select(string name)
        {
            return (SelectTag)new SelectTag()
                .InitFor(name, _htmlHelper)
                .RemoveAttr("value");
        }

        public virtual SelectTag Select<TItem>(
            string name,
            OptionsList<TItem> options,
            object defaultValue = null)
        {
            var value = _htmlHelper.GetValueFor(name);
            if (options.IsSelected() == null && value != null)
                options.SelectedValue(value);

            return this.Select(name)
                .AddOptions(options, defaultValue);
        }

        public virtual CheckBoxListTag CheckBoxList(string name, string cssClass = CheckBoxListTag.DEFAULT_CSS_CLASS)
        {
            return new CheckBoxListTag(name, cssClass);
        }

        public virtual CheckBoxListTag CheckBoxList<TItem>(
            string name,
            OptionsList<TItem> options,
            IEnumerable defaultValues = null,
            string cssClass = CheckBoxListTag.DEFAULT_CSS_CLASS)
        {
            var values = (IEnumerable)_htmlHelper.GetValueFor(name);
            if (options.IsSelected() == null && values != null)
                options.SelectedValues(values);

            return this.CheckBoxList(name, cssClass)
                .AddOptions(options, defaultValues);
        }
    }
}
