using Bl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Bl;


namespace MediscanBackend.Controllers
{
    [RoutePrefix("api/medicineStock")]
    public class MedicineStockController : ApiController
    {
        // שליפת  מלאי התרופות לפי קוד
        [HttpGet]
        [Route("GetmedicineStockById/{idMedicine}")]
        public IHttpActionResult GetmedicineStockList(int idMedicine)
        {
            return Ok(medicinestockBl.Getmedicinestock(idMedicine));
        }
    }
}
