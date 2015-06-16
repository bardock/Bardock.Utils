using System;
using System.Linq;
using Bardock.Utils.UnitTest.Samples.SUT.DTOs;
using Bardock.Utils.UnitTest.Samples.SUT.Entities;
using Bardock.Utils.UnitTest.Samples.SUT.Infra;

namespace Bardock.Utils.UnitTest.Samples.SUT.Managers
{
    public class CustomerManager : BaseManager
    {
        private DataContext _db;
        private IAuthService _authService;
        private IMailer _mailer;
        private ICustomerLogManager _customerLogManager;

        public CustomerManager(
            string userName,
            DataContext db,
            IAuthService authService,
            IMailer mailer,
            ICustomerLogManager customerLogManager)
            : base(userName)
        {
            _db = db;
            _authService = authService;
            _mailer = mailer;
            _customerLogManager = customerLogManager;
        }

        /// <summary>
        /// Method used for demonstrate interaction testing
        /// </summary>
        public void Create(CustomerCreate data)
        {
            _authService.Authorize(this.UserName, "customer_create");

            if (data.Email == "invalid")
                throw new InvalidOperationException();

            if (_db.Customers.Any(x => x.Email == data.Email))
                throw new EmailAlreadyExistsException();

            var e = new Customer()
            {
                FirstName = data.FirstName,
                LastName = data.LastName,
                Email = data.Email,
                Age = data.Age,
                StatusID = data.StatusID,
                CreatedOn = DateTime.Now
            };
            _db.Customers.Add(e);

            _customerLogManager.LogCreate(e);

            _db.SaveChanges();

            _mailer.Send(data.Email, "Welcome");
        }

        public void Update(int id, CustomerCreate data)
        {
            var e = _db.Customers.First(x => x.ID == id);
        }

        public class EmailAlreadyExistsException : Exception { }
    }
}