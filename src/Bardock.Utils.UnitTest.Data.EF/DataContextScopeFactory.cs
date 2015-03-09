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
                    .Add(x => x.Configuration.AutoDetectChangesEnabled, false)
                    .Add(x => x.Configuration.ValidateOnSaveEnabled, false));
        }
    }
}