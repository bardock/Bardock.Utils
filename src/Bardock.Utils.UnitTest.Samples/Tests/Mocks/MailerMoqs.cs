using Bardock.Utils.UnitTest.Samples.SUT.Infra;
using Moq;
using System;

namespace Bardock.Utils.UnitTest.Samples.Tests.Mocks
{
    public static class MailerMoqs
    {
        public static IMailer Default
        {
            get
            {
                return new Mock<IMailer>().Object;
            }
        }

        public static IMailer ValidatesEmail
        {
            get
            {
                var moq = new Mock<IMailer>();

                moq.Setup(m => m.Send(It.Is<string>(email => !email.Contains("@")), It.IsAny<string>())).Throws<ArgumentException>();

                return moq.Object;
            }
        }
    }
}