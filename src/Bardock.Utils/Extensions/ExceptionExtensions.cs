using System;

namespace Bardock.Utils.Extensions
{
    public static class ExceptionExtensions
    {
        /// <summary>
        /// Gets the first InnerException that satisfies TException
        /// </summary>
        /// <typeparam name="TException">The type of the target inner exception</typeparam>
        /// <param name="ex"></param>
        public static TException GetInnerExceptionOrDefault<TException>(this Exception ex) where TException : Exception
        {
            while (ex.InnerException != null)
            {
                ex = ex.InnerException;
                if (typeof(TException).IsAssignableFrom(ex.GetType()))
                    return (TException)ex;
            }
            return null;
        }
    }
}