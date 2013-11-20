using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Web.Mvc;
using Bardock.Utils.Validation;

namespace Bardock.Utils.Web.Mvc.Validation
{

	/// <summary>
	/// Validates email based on W3C HTML5 specification:
    /// http://www.w3.org/TR/html5/forms.html#e-mail-state-(type=email)
    /// Note: This requirement is a willful violation of RFC 5322, 
    /// which defines a syntax for e-mail addresses that is simultaneously too strict 
    /// (before the “@” character), too vague (after the “@” character), and too lax 
    /// (allowing comments, whitespace characters, and quoted strings in manners unfamiliar to most users) 
    /// to be of practical use here.
	/// </summary>
    /// <remarks>
    /// For client-side validation you must register the adapter when the application starts:
    /// DataAnnotationsModelValidatorProvider.RegisterAdapter(typeof(EmailAttribute), typeof(RegularExpressionAttributeAdapter));
    /// </remarks>
	public class EmailAttribute : RegularExpressionAttribute
	{
        public EmailAttribute()
            : base(EmailValidator.REGEX_PATTERN)
        {            
        }
	}
}