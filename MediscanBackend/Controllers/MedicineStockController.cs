using Bl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MediscanBackend.Controllers
{
    [RoutePrefix("api/medicines")]
    public class MedicineStockController : ApiController
    {
        // שליפת רשימת מלאי התרופות לפי קוד
        [HttpGet]

        [Route("GetmedicineStockList /{idMedicine}")]
        public IHttpActionResult GetmedicineStockList(int idMedicine)
        {
            return Ok(medicinestockBl.Getmedicinestock(idMedicine));
        }
    }
}
