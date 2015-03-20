using System;
using Bardock.Utils.UnitTest.Samples.SUT.Infra;
using Bardock.Utils.UnitTest.Samples.SUT.Entities;

namespace Bardock.Utils.UnitTest.Samples.SUT.Managers
{
    public class CustomerManager : BaseManager
    {
        private IAuthService _authService;
        private IMailer _mailer;

        public CustomerManager(string userName, IAuthService authService, IMailer mailer)
            : base(userName)
        {
            _authService = authService;
            _mailer = mailer;
        }

        /// <summary>
        /// Method used for demonstrate interaction testing
        /// </summary>
        public void Create(Customer customer)
        {
            _authService.Authorize(this.UserName, "customer_create");

            if (customer.Email == "invalid")
                throw new InvalidOperationException();

            _mailer.Send(customer.Email, "Welcome");
        }
    }
}