using Bardock.Utils.Data.EF.Exceptions;
using Bardock.Utils.Data.EF.Exceptions.Mappers;
using Bardock.Utils.Extensions;
using System;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;

namespace Bardock.Utils.Data.EF.SqlServer.Exceptions.Mappers
{
    public class SqlServerExceptionMapper : IExceptionMapper
    {
        public Exception Map(Exception ex)
        {
            var updateEx = ex as DbUpdateException;
            if (updateEx == null)
                return ex;

            var sqlEx = ex.GetInnerExceptionOrDefault<SqlException>();
            if (sqlEx != null && sqlEx.Number.In(2601, 2627))
            {
                // 2601: http://technet.microsoft.com/en-us/library/ms151779(v=sql.105).aspx
                // 2627: http://technet.microsoft.com/en-us/library/ms151757(v=sql.105).aspx
                return new DuplicatedEntryException(ex);
            }
            return ex;
        }
    }
}