using NUnit.Framework;
using System;
namespace PollutometerWebApi.Tests
{
    [TestFixture()]
    public class Test
    {
        [Test()]
        public void TestCase()
        {
            var result = AqiCalculator.CalculateAqi(new Models.Reading() {Co = 30.4, No = 1.0, So = 0.4});
            EmailSender.SendEmail(result);
        }
    }
}
