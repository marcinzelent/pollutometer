using Microsoft.VisualStudio.TestTools.UnitTesting;
using PollutometerWebApi.Models;

namespace PollutometerWebApi.Tests
{
    [TestClass()]
    public class EmailSenderTests
    {
        [TestMethod()]
        public void SendEmailTest()
        {
            var aqi = new Aqi
            {
                GasName = "SO",
                Value = 69,
                Level = "Moderate"
            };
            EmailSender.SendEmail(aqi);
        }
    }
}