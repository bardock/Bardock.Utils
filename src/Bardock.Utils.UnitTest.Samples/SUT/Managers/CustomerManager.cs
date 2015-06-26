using Bardock.Utils.UnitTest.Samples.SUT.DTOs;
using Bardock.Utils.UnitTest.Samples.SUT.Entities;
using Bardock.Utils.UnitTest.Samples.SUT.Infra;
using System;
using System.Linq;

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
        public Customer Create(CustomerCreate data)
        {
            _authService.Authorize(this.UserName, "customer_create");

            if (data.Email == "invalid")
                throw new InvalidOperationException();

            if (_db.Customers.Any(x => x.Email == data.Email))
                throw new EmailAlreadyExistsException();

            var e = AutoMapper.Mapper.Map<Entities.Customer>(data);

            _db.Customers.Add(e);

            _customerLogManager.LogCreate(e);

            _db.SaveChanges();

            _mailer.Send(data.Email, "Welcome");

            return e;
        }

        public void Update(CustomerUpdate data)
        {
            var e = _db.Customers.First(x => x.ID == data.ID);

            AutoMapper.Mapper.Map(data, e);

            _db.SaveChanges();

            _customerLogManager.LogUpdate(e);
        }

        public class EmailAlreadyExistsException : Exception { }
    }
}