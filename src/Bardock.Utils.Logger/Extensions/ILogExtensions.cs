using System;
using System.Runtime.CompilerServices;

namespace Bardock.Utils.Logger.Extensions
{
	public static class ILogExtensions
	{
        public static void Process(this ILog log, Func<object> f, bool ignoreError = false, bool logSuccess = false)
		{
			try {
				object ret = f();

				if ((logSuccess)) {
					log.Info(ret);
				}

			} catch (Exception ex) {
				log.Error(ex);

				if ((!ignoreError)) {
					throw ex;
				}

			}
		}

        public static void Process(this ILog log, Action f, bool ignoreError = false, bool logSuccess = false)
		{
			log.Process(() =>
			{
				f();
				return null;
			}, ignoreError, logSuccess);
		}
	}

}