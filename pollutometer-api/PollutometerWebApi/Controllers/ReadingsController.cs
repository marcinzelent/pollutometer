﻿using System;
using System.Web.Http;
using PollutometerWebApi.Models;

namespace PollutometerWebApi.Controllers
{
    public class ReadingsController : ApiController
    {
		public ReadingsController() {}

		public IHttpActionResult GetAllReadings()
		{
            var command = "SELECT * FROM Readings";
            var readings = SqlOperator.GetReadings(command);

            if (readings.Count > 0) return Ok(readings);
            else return NotFound();
		}

		public IHttpActionResult GetReading(int id)
		{
            var command = $"SELECT * FROM Readings WHERE Id={id}";
            var readings = SqlOperator.GetReadings(command);

			if (readings.Count == 1) return Ok(readings[0]);
			else return NotFound();
		}

        [Route("api/Readings/latest")]
        public IHttpActionResult GetLatestReading()
        {
            var command = "SELECT * FROM Readings " +
                "WHERE TimeStamp IN(SELECT MAX(TimeStamp) FROM Readings)";
            var reading = SqlOperator.GetReadings(command)[0];

			if (reading != null) return Ok(reading);
			else return NotFound();
        }

        [Route("api/Readings/lastweek")]
		public IHttpActionResult GetReadingsFromLastWeek()
		{
            var timeNow = DateTimeOffset.Now.ToUnixTimeSeconds();
            var command = "SELECT * FROM Readings " +
                $"WHERE TimeStamp BETWEEN {timeNow-7*24*3600} AND {timeNow}";
			var readings = SqlOperator.GetReadings(command);

			if (readings.Count > 0) return Ok(readings);
			else return NotFound();
		}

		public IHttpActionResult PutReading(int id, Reading reading)
		{
			if (reading != null)
			{
				SqlOperator.PutReading(id, reading);
				return Ok();
			}
			else return BadRequest();
		}

		public IHttpActionResult PostReading(Reading reading)
		{
			if (reading != null)
			{
                SqlOperator.PostReading(reading);
                var aqi = AqiCalculator.CalculateAqi(reading);
                if (aqi.Value >= 151)
                    EmailSender.SendEmail(aqi);
				return Ok(reading);
			}
			else return BadRequest();
		}

		public IHttpActionResult DeleteReading(int id)
		{
            var command = $"SELECT * FROM Readings WHERE Id={id}";
            var readings = SqlOperator.GetReadings(command);
			if (readings.Count == 0) return NotFound();
			SqlOperator.DeleteReading(id);
            
			return Ok(readings[0]);
		}
    }
}