using Bardock.Utils.UnitTest.Samples.SUT.Infra;
using System;

namespace Bardock.Utils.UnitTest.Samples.Tests.Mocks
{
    /// <summary>
    /// This class is used only as an example. You must use Moq to achieve the same functionality.
    /// </summary>
    public class MailerMock : IMailer
    {
        private Tuple<string, string> _lastSend = null;

        public void Send(string address, string body)
        {
            _lastSend = new Tuple<string, string>(address, body);
        }

        public void VerifyWasSent(string address, string body)
        {
            if (_lastSend == null)
                throw new Exception("zero invocations");

            if (_lastSend.Item1 != address || _lastSend.Item2 != body)
                throw new Exception(string.Format("last invocation address: '{0}'; body: '{1}'", _lastSend.Item1, _lastSend.Item2));
        }
    }
}