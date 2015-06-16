using Bardock.Utils.UnitTest.Samples.SUT.Entities;

namespace Bardock.Utils.UnitTest.Samples.SUT.Managers
{
    public interface ICustomerLogManager
    {
        void LogCreate(Customer e);
    }

    public class CustomerLogManager : BaseManager, ICustomerLogManager
    {
        public CustomerLogManager(string userName)
            : base(userName)
        {
        }

        public void LogCreate(Customer c)
        {
        }
    }
}