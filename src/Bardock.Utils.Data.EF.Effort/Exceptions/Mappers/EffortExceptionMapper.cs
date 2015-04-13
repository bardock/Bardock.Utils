using System;
using System.Data.Entity.Infrastructure;
using Bardock.Utils.Data.EF.Exceptions;
using Bardock.Utils.Data.EF.Exceptions.Mappers;
using Bardock.Utils.Extensions;

namespace Bardock.Utils.Data.EF.Effort.Exceptions.Mappers
{
    public class EffortExceptionMapper : IExceptionMapper
    {
        public Exception Map(Exception ex)
        {
            var updateEx = ex as DbUpdateException;
            if (updateEx == null)
                return ex;

            var nmemEx = ex.GetInnerExceptionOrDefault<NMemory.Exceptions.NMemoryException>();
            if (nmemEx != null && typeof(NMemory.Exceptions.MultipleUniqueKeyFoundException).IsAssignableFrom(nmemEx.GetType()))
            {
                return new DuplicatedEntryException(ex);
            }
            return ex;
        }
    }
}