using Bardock.Utils.UnitTest.Samples.Fixtures.Attributes;
using Bardock.Utils.UnitTest.Samples.SUT.DTOs;
using Bardock.Utils.UnitTest.Samples.SUT.Entities;
using Bardock.Utils.UnitTest.Samples.SUT.Infra;
using Bardock.Utils.UnitTest.Samples.SUT.Managers;
using Moq;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoMoq;
using Ploeh.AutoFixture.Xunit;
using Xunit;
using Xunit.Extensions;

namespace Bardock.Utils.UnitTest.Samples.Tests.Managers
{
    /// <summary>
    /// This class is responsible for test CustomerManager features
    /// </summary>
    public class CustomerManagerTests
    {
        private abstract class FromDB
        {
            protected dynamic _db;

            public FromDB(dynamic db)
            {
                _db = db;
            }
        }

        private class GetCustomerFromDB : FromDB
        {
            public GetCustomerFromDB(dynamic db) : base((object)db) { }

            public object Resolve()
            {
                return _db.Customers.Find(1);
            }
        }

        private class CreateCustomerFromChina
        {
            public void Configure(IFixture fixture)
            {
                fixture.Build<Customer>().With(x => x.Email, "");
            }
        }

        //[DefaultData]
        [Theory]
        [InlineDefaultData()]
        //[InlineDefaultData(typeof(GetCustomerFromDB))]
        //[InlineDefaultData(typeof(CreateCustomerFromChina))]
        public void Register_ValidEmail_SendMail(
            CustomerCreate data,
            [Frozen] Mock<IAuthService> authService,
            [Frozen] Mock<IMailer> mailer,
            CustomerManager sut)
        {
            sut.Create(data);

            mailer.Verify(x => x.Send(data.Email, "Welcome"));
        }

        /// <summary>
        /// This unit test demonstrates how to use a service registered in the setup phase of this test
        /// </summary>
        [Fact]
        public void Register_ValidEmail_SendMail_UsingMoq()
        {
        }
    }
}