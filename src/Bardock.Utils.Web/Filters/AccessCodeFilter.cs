using System.Web;

namespace Bardock.Utils.Web.Filters
{
    public class AccessCodeFilter
    {
        public virtual string Code { get; set; }

        public virtual string ParamName { get; set; }

        public AccessCodeFilter()
        {
            ParamName = "accessCode";
        }

        public virtual bool IsValidAccessCode()
        {
            return IsDebuggingEnabled()
                || CodeMatches();
        }

        protected virtual bool IsDebuggingEnabled()
        {
            return HttpContext.Current.IsDebuggingEnabled;
        }

        protected virtual bool CodeMatches()
        {
            return !string.IsNullOrWhiteSpace(this.Code)
                && !string.IsNullOrWhiteSpace(this.ParamName)
                && GetRequestCode() == this.Code;
        }

        protected virtual string GetRequestCode()
        {
            return HttpContext.Current.Request[this.ParamName];
        }
    }
}
