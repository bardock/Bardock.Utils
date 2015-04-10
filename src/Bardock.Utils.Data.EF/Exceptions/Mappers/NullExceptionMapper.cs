using System;

namespace Bardock.Utils.Data.EF.Exceptions.Mappers
{
    internal class NullExceptionMapper : IExceptionMapper
    {
        public virtual Exception Map(Exception ex)
        {
            return ex;
        }
    }
}