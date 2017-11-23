using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using AirPollutionWebApi.Models;
using AirPollutionWebApi.Singletons;

namespace AirPollutionWebApi.Controllers
{
    public class ReadingsController : ApiController
    {
		public ReadingsController() { }

		public IEnumerable<Reading> GetAllReadings()
		{
            Singleton.Instance.GetData();
			return Singleton.Instance.Readings;
		}

		//public IHttpActionResult GetReading(int id)
		//{
  //          var customer = Singleton.Instance.Readings.FirstOrDefault((p) => p.TimeStamp == id);

		//	if (customer != null) return Ok(customer);
		//	else return NotFound();
		//}

		//public IHttpActionResult PutReading(int id, Reading customer)
		//{
		//	if (customer != null)
		//	{
		//		Singleton.Instance.PutData(id, customer);
		//		return Ok();
		//	}
		//	else return BadRequest();
		//}

		public IHttpActionResult PostReading(Reading customer)
		{
			if (customer != null)
			{
				Singleton.Instance.PostData(customer);
				return Ok();
			}
			else return BadRequest();
		}

		//public IHttpActionResult DeleteReading(int id)
		//{
		//	Reading customer = Singleton.Instance.Readings.Find((p) => p.Id == id);
		//	if (customer == null)
		//	{
		//		return NotFound();
		//	}

		//	Singleton.Instance.DeleteData(id);

		//	return Ok(customer);
		//}
    }
}