using Bardock.Utils.Scoping;
using System;
using System.Data.Entity;

namespace Bardock.Utils.UnitTest.Data.EF
{
    /// <summary>
    /// An entity framework implementation of <see cref="IDataContextScope"/>
    /// </summary>
    public class DataContextScope : Scope<DbContext>, IDataContextScope
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DataContextScope"/> class.
        /// </summary>
        /// <param name="wrapper">The wrapper.</param>
        /// <param name="factoryFunc">The factory function.</param>
        public DataContextScope(DataContextWrapper wrapper, Action<Builder> factoryFunc)
            : base(wrapper._db, factoryFunc)
        {
            Db = wrapper;
        }

        /// <summary>
        /// Gets the <see cref="IDataContextScope" /> instance.
        /// </summary>
        /// <value>
        /// The <see cref="IDataContextScope" /> instance
        /// </value>
        public IDataContextWrapper Db { get; private set; }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <remarks>
        /// Saves the current state of the <see cref="IDataContextWrapper"/>
        /// </remarks>
        public override void Dispose()
        {
            Db.Save();

            base.Dispose();
        }
    }
}