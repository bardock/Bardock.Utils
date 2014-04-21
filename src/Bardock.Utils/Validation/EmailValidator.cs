using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Bardock.Utils.Validation
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
    public class EmailValidator
    {
        public const string REGEX_PATTERN = @"^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?(?:\.[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?)*$";

        public static bool IsValid(string email)
        {
            return Regex.IsMatch(email, REGEX_PATTERN);
        }
    }
}
