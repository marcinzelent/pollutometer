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
            EmailService.Start();
        }
    }
}
