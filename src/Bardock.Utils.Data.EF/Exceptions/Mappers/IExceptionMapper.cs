using System;

namespace Bardock.Utils.Data.EF.Exceptions.Mappers
{
    public interface IExceptionMapper
    {
        Exception Map(Exception ex);
    }
}