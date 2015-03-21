namespace Bardock.Utils.UnitTest.Data
{
    public interface IDataContextScope
    {
        IDataContextWrapper Db { get; }
    }
}