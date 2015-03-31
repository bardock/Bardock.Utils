using System;

namespace Bardock.Utils.UnitTest.Data
{
    public interface IDataContextScope : IDisposable
    {
        IDataContextWrapper Db { get; }
    }
}