using Bardock.Utils.Web.Mvc.HtmlTags.Extensions;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Web.Mvc.Html;
using System.Web.Mvc;
using HtmlTags;
using System.Linq.Expressions;

namespace Bardock.Utils.Web.Mvc.HtmlTags
{
    public class HtmlTagHelper
    {
        private HtmlHelper _htmlHelper;

        public HtmlHelper HtmlHelper { get { return _htmlHelper; } }

        public HtmlTagHelper()
            : this(HtmlHelperFactory.CreateInstance()) 
        { }

        public HtmlTagHelper(HtmlHelper htmlHelper) 
        {
            this._htmlHelper = htmlHelper;
        }

        public virtual HtmlTag HtmlTag(string tag, string name)
        {
            return new HtmlTag(tag)
                .Attr("id", _htmlHelper.Id(name))
                .Attr("name", name);
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

        public virtual HtmlTag Radio(string name, bool isChecked = false)
        {
            return Input(InputType.Radio, name).Checked(isChecked);
        }

        public virtual HtmlTag CheckBox(string name, bool isChecked = false)
        {
            return Input(InputType.CheckBox, name).Checked(isChecked);
        }
    }
}
