using System;

namespace Bardock.Utils.UnitTest.Data
{
    /// <summary>
    /// A scope for a <see cref="IDataContextWrapper"/>
    /// </summary>
    public interface IDataContextScope : IDisposable
    {
        /// <summary>
        /// Gets the <see cref="IDataContextScope"/> instance.
        /// </summary>
        /// <value>
        /// The <see cref="IDataContextScope"/> instance
        /// </value>
        IDataContextWrapper Db { get; }
    }
}