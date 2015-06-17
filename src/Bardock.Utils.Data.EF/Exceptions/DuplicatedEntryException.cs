using System;

namespace Bardock.Utils.Data.EF.Exceptions
{
    public class DuplicatedEntryException : Exception
    {
        public DuplicatedEntryException(Exception ex)
            : base("Cannot insert duplicate entry", ex)
        { }
    }
}