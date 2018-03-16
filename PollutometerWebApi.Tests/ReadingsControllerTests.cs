using Microsoft.VisualStudio.TestTools.UnitTesting;
using PollutometerWebApi.Models;
using System;
using System.Collections.Generic;
using System.Web.Http.Results;

namespace PollutometerWebApi.Controllers.Tests
{
    [TestClass()]
    public class ReadingsControllerTests
    {
        ReadingsController controller = new ReadingsController();

        [TestMethod()]
        public void GetAllReadingsTest()
        {
            var actionResult = controller.GetAllReadings();
            var content = ((OkNegotiatedContentResult<List<Reading>>)actionResult).Content;
            Assert.IsTrue(content.Count >= 1);
        }


        [TestMethod()]
        public void GetReadingTest()
        {
            var actionResult = controller.GetReading(0);
            Assert.IsInstanceOfType(actionResult, typeof(OkNegotiatedContentResult<Reading>));
        }

        [TestMethod()]
        public void GetLatestReadingTest()
        {
            var actionResult = controller.GetLatestReading();
            var content = ((OkNegotiatedContentResult<Reading>)actionResult).Content;
            Assert.IsNotNull(content);
        }

        [TestMethod()]
        public void GetReadingsFromLastWeekTest()
        {
            var actionResult = controller.GetReadingsFromLastWeek();
            var content = ((OkNegotiatedContentResult<List<Reading>>)actionResult).Content;
            foreach(var reading in content)
            {
                var now = DateTimeOffset.Now.ToUnixTimeSeconds();
                Assert.IsTrue(reading.TimeStamp < now && reading.TimeStamp > now - 7*24*3600);
            }
        }

        [TestMethod()]
        public void PostReadingPassTest()
        {
            var newReading = new Reading
            {
                TimeStamp = (int)DateTimeOffset.Now.ToUnixTimeSeconds(),
                Co = 1.0,
                No = 1.0,
                So = 1.0
            };
            var actionResult = controller.PostReading(newReading);
            var reading = controller.GetLatestReading();
            var content = ((OkNegotiatedContentResult<Reading>)reading).Content;
            Assert.AreEqual(newReading.TimeStamp, content.TimeStamp);
        }

        [TestMethod()]
        public void PostReadingFailTest()
        {
            var actionResult = controller.PostReading(null);
            Assert.IsInstanceOfType(actionResult, typeof(BadRequestResult));
        }

        [TestMethod()]
        public void PutReadingPassTest()
        {
            var latest = controller.GetLatestReading();
            var content = ((OkNegotiatedContentResult<Reading>)latest).Content;
            var actionResult = controller.PutReading(content.Id,
                new Reading
                {
                    TimeStamp = (int)DateTimeOffset.Now.ToUnixTimeSeconds(),
                    Co = 2.0,
                    No = 2.0,
                    So = 2.0
                }
                );
            Assert.AreEqual(typeof(OkResult), actionResult.GetType());
        }

        [TestMethod()]
        public void PutReadingFailTest()
        {
            var latest = controller.GetLatestReading();
            var content = ((OkNegotiatedContentResult<Reading>)latest).Content;
            var actionResult = controller.PutReading(content.Id, null);
            Assert.AreEqual(typeof(BadRequestResult), actionResult.GetType());
        }

        [TestMethod()]
        public void DeleteReadingPassTest()
        {
            var latest = controller.GetLatestReading();
            var content = ((OkNegotiatedContentResult<Reading>)latest).Content;
            var actionResult = controller.DeleteReading(content.Id);
            var reading = controller.GetReading(content.Id);
            Assert.IsInstanceOfType(reading, typeof(NotFoundResult));
        }

        [TestMethod()]
        public void DeleteReadingFailTest()
        {
            var actionResult = controller.DeleteReading(1337);
            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult));
        }
    }
}