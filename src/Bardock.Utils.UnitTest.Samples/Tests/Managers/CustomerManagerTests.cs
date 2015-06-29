using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Bardock.Utils.UnitTest.AutoFixture.AutoMapper.Customizations;
using Bardock.Utils.UnitTest.AutoFixture.Customizations;
using Bardock.Utils.UnitTest.AutoFixture.Extensions;
using Bardock.Utils.UnitTest.AutoFixture.Xunit2.Attributes;
using Bardock.Utils.UnitTest.AutoFixture.Xunit2.AutoMapper.Attributes;
using Bardock.Utils.UnitTest.AutoFixture.Xunit2.Data.Attributes;
using Bardock.Utils.UnitTest.Data;
using Bardock.Utils.UnitTest.Data.AutoFixture.Customizations;
using Bardock.Utils.UnitTest.Samples.Fixtures.Attributes;
using Bardock.Utils.UnitTest.Samples.SUT.DTOs;
using Bardock.Utils.UnitTest.Samples.SUT.Entities;
using Bardock.Utils.UnitTest.Samples.SUT.Infra;
using Bardock.Utils.UnitTest.Samples.SUT.Managers;
using Moq;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.Xunit2;
using Xunit;

namespace Bardock.Utils.UnitTest.Samples.Tests.Managers
{
    /// <summary>
    /// This class is responsible for test CustomerManager features
    /// </summary>
    public class CustomerManagerTests
    {
        public class WithInvalidEmailAttribute : CustomizeAttribute
        {
            public override ICustomization GetCustomization(ParameterInfo parameter)
            {
                return new WithInvalidEmailCustomization();
            }
        }

        public class WithInvalidEmailCustomization : ICustomization
        {
            public void Customize(IFixture fixture)
            {
                fixture.Customize<CustomerCreate>(c => c.With(x => x.Email, "invalid"), append: true);
                fixture.Customize<Customer>(c => c.With(x => x.Email, "invalid"), append: true);
            }
        }

        private class AsAdultAttribute : CustomizeAttribute
        {
            public override ICustomization GetCustomization(ParameterInfo parameter)
            {
                return new AsAdultCustomization();
            }
        }

        public class AsAdultCustomization : ICustomization
        {
            public void Customize(IFixture fixture)
            {
                fixture.Customize<CustomerCreate>(c => c.With(x => x.Age, 21), append: true);
                fixture.Customize<Customer>(c => c.With(x => x.Age, 21), append: true);
            }
        }

        public class WithInvalidEmailAsAdultAttribute : CustomizeAttribute
        {
            public override ICustomization GetCustomization(ParameterInfo parameter)
            {
                return new CompositeCustomization(new WithInvalidEmailCustomization(), new AsAdultCustomization());
            }
        }

        public class AsAdultPersistedAttribute : CustomizeAttribute
        {
            public override ICustomization GetCustomization(ParameterInfo parameter)
            {
                return new CompositeCustomization(new AsAdultCustomization(), new PersistedEntityCustomization(parameter));
            }
        }

        public class AutoMapToCustomerAttribute : AutoMapMembersAttribute
        {
            public AutoMapToCustomerAttribute()
                : base(typeof(Customer))
            { }
        }

        public class MapToCustomerAttribute : MapMembersAttribute
        {
            public MapToCustomerAttribute()
                : base(typeof(Customer))
            { }

            public override IEnumerable<MemberMapping> Configure(Type sourceType, Type destinationType)
            {
                return new MembersMappingComposer<CustomerCreate, Customer>()
                            .Map(c => c.FirstName, c => c.FirstName)
                            .Map(c => c.Surname, c => c.LastName)
                            .Map(c => c.Age, c => c.Age)
                            .Map(c => c.Email, c => c.Email)
                            .Map(c => c.StatusID, c => c.StatusID);
            }
        }

        //NOTE: DO NOT DO THE FOLLOWING:
        //public class WithInvalidEmailAsAdultPersistedAttribute : AsAdultPersistedAttribute
        //{
        //    public override ICustomization GetCustomization(ParameterInfo parameter)
        //    {
        //        return base.GetCustomization(parameter);
        //    }
        //}

        //[DefaultData]
        //[InlineDefaultData(typeof(GetCustomerFromDB))]
        //[InlineDefaultData(typeof(CreateCustomerFromChina))]
        //[DefaultData]
        [Theory]
        [DefaultData]
        public void Create_ValidEmail_SendMail(
            [AutoMapToCustomer] CustomerCreate data,
            [Frozen] Mock<IAuthService> authService,
            [Frozen] Mock<IMailer> mailer,
            CustomerManager sut)
        {
            sut.Create(data);

            mailer.Verify(x => x.Send(data.Email, "Welcome"));
        }

        [Theory]
        [DefaultData]
        public void Create_ValidEmail_SendMail2(
            [MapToCustomer] CustomerCreate data,
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

        [Theory]
        [DefaultData]
        public void Create_InvalidEmail2_InvalidOp___WithInvalidEmailAsAdult(
            [WithInvalidEmail][AsAdult] CustomerCreate data,
            [Frozen] Mock<IAuthService> authService,
            [Frozen] Mock<IMailer> mailer,
            CustomerManager sut)
        {
            Assert.True(data.Age == 21);
            Assert.Throws<InvalidOperationException>(() =>
                sut.Create(data));

            mailer.Verify(x => x.Send(data.Email, It.IsAny<string>()), Times.Never);
        }

        [Theory]
        [DefaultData]
        public void Create_InvalidEmail_InvalidOp___WithInvalidEmailAsAdultComposed(
            [WithInvalidEmailAsAdult] CustomerCreate data,
            [Frozen] Mock<IAuthService> authService,
            [Frozen] Mock<IMailer> mailer,
            CustomerManager sut)
        {
            Assert.True(data.Age == 21);
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
            data.Email = db.Set<Customer>().Select(x => x.Email).First();

            Assert.Throws<CustomerManager.EmailAlreadyExistsException>(() =>
                sut.Create(data));
        }

        //[Theory]
        //[DefaultData]
        //public void Create_ExistingEmail_Exception___UpdateExisting(
        //    IDataContextScopeFactory dataScope,
        //    CustomerCreate data,
        //    CustomerManager sut)
        //{
        //    using (var s = dataScope.CreateDefault())
        //    {
        //        var c = s.Db.GetQuery<Customer>().First();
        //        c.Email = data.Email;
        //        s.Db.Update(c);
        //    }

        //    Assert.Throws<CustomerManager.EmailAlreadyExistsException>(() =>
        //        sut.Create(data));
        //}

        [Theory]
        [DefaultData(typeof(AutoMapMembersCustomization<CustomerUpdate, Customer>))]
        public void Update_ExistingAdultCustomer_Success(
            //warning: the declaring order of the attrs
            //does not guarantee that customizations will
            //apply in that order, if ordering is required
            //then combine multiple customizations using
            //CompositeCustomizeAttribute like the example
            //below
            [Persisted][AsAdult] Customer e,
            IFixture fixture,
            [Frozen] Mock<ICustomerLogManager> customerLogManager,
            CustomerManager sut)
        {
            var data = fixture.Build<CustomerUpdate>()
                .With(x => x.ID, e.ID)
                .Create();

            sut.Update(data);

            customerLogManager.Verify(x => x.LogUpdate(It.Is<Customer>(c => c.ID == data.ID)));
        }

        [Theory]
        [DefaultData(typeof(AutoMapMembersCustomization<CustomerUpdate, Customer>))]
        public void Update_ExistingAdultCustomer_Success_Composed(
            [AsAdultPersisted] Customer e,
            CustomerManager sut,
            IFixture fixture)
        {
            var data = fixture.Build<CustomerUpdate>()
                .With(x => x.ID, e.ID)
                .Create();

            sut.Update(data);
        }

        [Theory]
        [InlineDefaultData(false, false)]
        [InlineDefaultData(true, false, typeof(AsAdultCustomization))]
        [InlineDefaultData(true, true, typeof(AsAdultCustomization), typeof(PersistedEntityCustomization<Customer>))]
        public void Update_AsAdult_Success___Inline(
            bool isAdult,
            bool isPersisted,
            Customer e)
        {
            Assert.Equal(isAdult, e.Age == 21);
            Assert.Equal(isPersisted, e.ID > 0);
        }

        [Theory]
        [DefaultData]
        public void Create_ValidData_LogCreate(
            [AutoMapToCustomer] CustomerCreate data,
            [Frozen] Mock<ICustomerLogManager> customerLogManager,
            CustomerManager sut)
        {
            var e = sut.Create(data);

            customerLogManager.Verify(x => x.LogCreate(e));
        }
    }
}