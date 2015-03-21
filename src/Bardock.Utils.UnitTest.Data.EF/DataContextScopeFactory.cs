namespace Bardock.Utils.UnitTest.Data.EF
{
    public class DataContextScopeFactory : IDataContextScopeFactory
    {
        private DataContextWrapper _wrapper;

        public DataContextScopeFactory(DataContextWrapper wrapper)
        {
            _wrapper = wrapper;
        }

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