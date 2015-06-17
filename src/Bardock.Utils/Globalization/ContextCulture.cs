using System;
using System.Globalization;
using System.Threading;

namespace Bardock.Utils.Globalization
{
    public class ContextCulture : IDisposable
    {
        private CultureInfo _previousUICulture;
        private CultureInfo _previousCulture;

        public ContextCulture(string cultureName = "en")
        {
            _previousUICulture = Thread.CurrentThread.CurrentUICulture;
            _previousCulture = Thread.CurrentThread.CurrentCulture;
            Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo(cultureName);
            Thread.CurrentThread.CurrentCulture = Thread.CurrentThread.CurrentUICulture;
        }

        public void Dispose()
        {
            Thread.CurrentThread.CurrentUICulture = _previousUICulture;
            Thread.CurrentThread.CurrentCulture = _previousCulture;
        }
    }
}