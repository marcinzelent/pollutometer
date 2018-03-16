using Microsoft.VisualStudio.TestTools.UnitTesting;
using PollutometerWebApi.Models;

namespace PollutometerWebApi.Tests
{
    [TestClass()]
    public class AqiCalculatorTests
    {
        [TestMethod()]
        public void CalculateAqiPerfectTest()
        {
            var reading = new Reading
            {
                Co = 2.2,
                No = 0.1,
                So = 0.8
            };

            var result = AqiCalculator.CalculateAqi(reading);
            Assert.AreEqual(398.0100502512563, result.Value);
        }

        [TestMethod()]
        public void CalculateAqiBigNumbersTest()
        {
            var reading = new Reading
            {
                Co = 100.2,
                No = 100.1,
                So = 100.8
            };

            var result = AqiCalculator.CalculateAqi(reading);
            Assert.AreEqual(0, result.Value);
        }

        [TestMethod()]
        public void CalculateAqiZerosTest()
        {
            var reading = new Reading
            {
                Co = 0,
                No = 0,
                So = 0
            };

            var result = AqiCalculator.CalculateAqi(reading);
            Assert.AreEqual(0, result.Value);
        }
    }
}