using System.Collections.Generic;
using System.Web.Http;
using AirPollutionWebApi.Models;
using AirPollutionWebApi.Singletons;

namespace AirPollutionWebApi.Controllers
{
    public class ReadingsController : ApiController
    {
		public ReadingsController() { }

		public IEnumerable<Reading> GetAllReadings()
		{
            var readings = SqlOperator.GetAllReadings();
            return readings;
		}

		public IHttpActionResult GetReading(int id)
		{
            var reading = SqlOperator.GetReadingById(id);

			if (reading != null) return Ok(reading);
			else return NotFound();
		}

        [Route("api/Readings/latest")]
        public IHttpActionResult GetLatestReading()
        {
            var readings = SqlOperator.GetAllReadings();
            Reading latestReading = null;

            foreach(var reading in readings)
            {
                if (latestReading == null) latestReading = reading;
                if (reading.TimeStamp > latestReading.TimeStamp)
                    latestReading = reading;
            }

			if (latestReading != null) return Ok(latestReading);
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
				return Ok();
			}
			else return BadRequest();
		}

		public IHttpActionResult DeleteReading(int id)
		{
            Reading reading = SqlOperator.GetReadingById(id);
			if (reading == null)
			{
				return NotFound();
			}

			SqlOperator.DeleteReading(id);

			return Ok(reading);
		}
    }
}