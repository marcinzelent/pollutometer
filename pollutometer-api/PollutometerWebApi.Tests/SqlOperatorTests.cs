using Microsoft.VisualStudio.TestTools.UnitTesting;
using PollutometerWebApi.Models;
using System;

namespace PollutometerWebApi.Tests
{
    [TestClass()]
    public class SqlOperatorTests
    {
        [TestMethod()]
        public void GetReadingsTest()
        {
            var readings = SqlOperator.GetReadings("SELECT * FROM Readings");
            Assert.IsTrue(readings.Count >= 1);
        }

        [TestMethod()]
        public void PostReadingTest()
        {
            var newReading = new Reading
            {
                TimeStamp = (int)DateTimeOffset.Now.ToUnixTimeSeconds(),
                Co = 0.0,
                No = 0.0,
                So = 0.0
            };
            SqlOperator.PostReading(newReading);
            var command = "SELECT * FROM Readings " +
                "WHERE TimeStamp IN(SELECT MAX(TimeStamp) FROM Readings)";
            var latestReading = SqlOperator.GetReadings(command)[0];
            var command2 = $"SELECT * FROM Readings WHERE Id={latestReading.Id}";
            var reading = SqlOperator.GetReadings(command2)[0];
            Assert.AreEqual(newReading.TimeStamp, reading.TimeStamp);
        }

        [TestMethod()]
        public void PutReadingTest()
        {
            var newReading = new Reading
            {
                TimeStamp = (int)DateTimeOffset.Now.ToUnixTimeSeconds(),
                Co = 0.5,
                No = 0.5,
                So = 0.5

            };
            var command = "SELECT * FROM Readings " +
                "WHERE TimeStamp IN(SELECT MAX(TimeStamp) FROM Readings)";
            var latestReading = SqlOperator.GetReadings(command)[0];
            SqlOperator.PutReading(latestReading.Id, newReading);
            var command2 = $"SELECT * FROM Readings WHERE Id={latestReading.Id}";
            var reading = SqlOperator.GetReadings(command2)[0];
            Assert.AreEqual(newReading.TimeStamp, reading.TimeStamp);
        }

        [TestMethod()]
        public void DeleteReadingTest()
        {
            var command = "SELECT * FROM Readings " +
                   "WHERE TimeStamp IN(SELECT MAX(TimeStamp) FROM Readings)";
            var latestReading = SqlOperator.GetReadings(command)[0];
            SqlOperator.DeleteReading(latestReading.Id);
            var command2 = $"SELECT * FROM Readings WHERE Id={latestReading.Id}";
            var readings = SqlOperator.GetReadings(command2);
            Assert.IsTrue(readings.Count == 0);
        }
    }
}