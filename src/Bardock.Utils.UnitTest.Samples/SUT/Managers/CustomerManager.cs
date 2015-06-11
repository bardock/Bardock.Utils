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

        public CustomerManager(
            string userName,
            DataContext db,
            IAuthService authService,
            IMailer mailer)
            : base(userName)
        {
            _db = db;
            _authService = authService;
            _mailer = mailer;
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
                CreatedOn = DateTime.Now
            };
            _db.Customers.Add(e);
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