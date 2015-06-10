using Bardock.Utils.UnitTest.Samples.Fixtures.Attributes;
using Bardock.Utils.UnitTest.Samples.Fixtures.Customizations;
using Bardock.Utils.UnitTest.Samples.SUT.DTOs;
using Bardock.Utils.UnitTest.Samples.SUT.Entities;
using Bardock.Utils.UnitTest.Samples.SUT.Infra;
using Bardock.Utils.UnitTest.Samples.SUT.Managers;
using Moq;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.Dsl;
using Ploeh.AutoFixture.Xunit;
using System;
using System.Linq;
using Xunit;
using Xunit.Extensions;
using Bardock.Utils.UnitTest.Data;

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
            public GetCustomerFromDB(dynamic db)
                : base((object)db)
            {
            }

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
        public void Create_ValidEmail_SendMail(
            CustomerCreate data,
            [Frozen] Mock<IAuthService> authService,
            [Frozen] Mock<IMailer> mailer,
            CustomerManager sut)
        {
            sut.Create(data);

            mailer.Verify(x => x.Send(data.Email, "Welcome"));
        }

        [Theory]
        [DefaultData]
        public void Create_InvalidEmail_InvalidOp(
            CustomerCreate data,
            [Frozen] Mock<IAuthService> authService,
            [Frozen] Mock<IMailer> mailer,
            CustomerManager sut)
        {
            data.Email = "invalid";

            Assert.Throws<InvalidOperationException>(() =>
                sut.Create(data)
            );

            mailer.Verify(x => x.Send(data.Email, It.IsAny<string>()), Times.Never);
        }

        private class WithInvalidEmailAttribute : CustomizeAttribute
        {
            public override ICustomization GetCustomization(System.Reflection.ParameterInfo parameter)
            {
                return new WithInvalidEmail();
            }

            private class WithInvalidEmail : CustomizationComposer<CustomerCreate>
            {
                protected override IPostprocessComposer<CustomerCreate> Configure(IFixture f, ICustomizationComposer<CustomerCreate> c)
                {
                    return c.With(x => x.Email, "invalid");
                }
            }
        }

        private class AsAdultAttribute : CustomizeAttribute
        {
            public override ICustomization GetCustomization(System.Reflection.ParameterInfo parameter)
            {
                return new AsAdult();
            }

            private class AsAdult : CustomizationComposer<CustomerCreate>
            {
                protected override IPostprocessComposer<CustomerCreate> Configure(IFixture f, ICustomizationComposer<CustomerCreate> c)
                {
                    return c.With(x => x.Age, 21);
                }
            }
        }

        [Theory]
        [DefaultData]
        public void Create_InvalidEmail_InvalidOp___CustomizeWith(
            //[CustomizeWith(typeof(WithInvalidEmail))] CustomerCreate data,
            [WithInvalidEmail][AsAdult] CustomerCreate data,
            [Frozen] Mock<IAuthService> authService,
            [Frozen] Mock<IMailer> mailer,
            CustomerManager sut)
        {
            Assert.True(data.Age >= 21);
            Assert.Throws<InvalidOperationException>(() =>
                sut.Create(data));

            mailer.Verify(x => x.Send(data.Email, It.IsAny<string>()), Times.Never);
        }

        [Theory]
        [DefaultData]
        public void Create_ExistingEmail_Exception(
            IDataContextWrapper db,
            CustomerCreate data,
            CustomerManager sut)
        {
            data.Email = db.GetQuery<Customer>().Select(x => x.Email).First();

            Assert.Throws<CustomerManager.EmailAlreadyExistsException>(() =>
                sut.Create(data));
        }

        [Theory]
        [DefaultData]
        public void Create_ExistingEmail_Exception___UpdateExisting(
            IDataContextScopeFactory dataScope,
            CustomerCreate data,
            CustomerManager sut)
        {
            using (var s = dataScope.CreateDefault())
            {
                var c = s.Db.GetQuery<Customer>().First();
                c.Email = data.Email;
                s.Db.Update(c);
            }

            Assert.Throws<CustomerManager.EmailAlreadyExistsException>(() =>
                sut.Create(data));
        }
    }
}