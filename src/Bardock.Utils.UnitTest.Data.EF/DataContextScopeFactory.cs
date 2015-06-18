namespace Bardock.Utils.UnitTest.Data.EF
{
    /// <summary>
    /// A <see cref="IDataContextScopeFactory"/> entity framework implementation.
    /// </summary>
    public class DataContextScopeFactory : IDataContextScopeFactory
    {
        private DataContextWrapper _wrapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="DataContextScopeFactory"/> class.
        /// </summary>
        /// <param name="wrapper">The wrapper.</param>
        public DataContextScopeFactory(DataContextWrapper wrapper)
        {
            _wrapper = wrapper;
        }

        /// <summary>
        /// Creates a <see cref="IDataContextScope" /> using the default configuration.
        /// </summary>
        /// <remarks>
        /// The instance provided has AutoDetectAutoDetectChangesEnabled and
        /// ValidateOnSaveEnabled configuration set to false.
        /// </remarks>
        /// <returns>
        /// A <see cref="IDataContextScope" /> using the default configuration.
        /// </returns>
        public IDataContextScope CreateDefault()
        {
            return new DataContextScope(
                _wrapper,
                b => b
                    .Set(x => x.Configuration.AutoDetectChangesEnabled, false)
                    .Set(x => x.Configuration.ValidateOnSaveEnabled, false));
        }
    }
}