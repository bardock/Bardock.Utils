namespace Bardock.Utils.UnitTest.Samples.SUT.Managers
{
    public abstract class BaseManager
    {
        public string UserName { get; protected set; }

        public BaseManager(string userName)
        {
            this.UserName = userName;
        }
    }
}