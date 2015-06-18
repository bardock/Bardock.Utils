namespace Bardock.Utils.UnitTest.Data
{
    /// <summary>
    /// A factory that creates instances of <see cref="IDataContextScope"/>
    /// </summary>
    public interface IDataContextScopeFactory
    {
        /// <summary>
        /// Creates a <see cref="IDataContextScope"/> using the default configuration.
        /// </summary>
        /// <returns>
        /// A <see cref="IDataContextScope"/> using the default configuration.
        /// </returns>
        IDataContextScope CreateDefault();
    }
}