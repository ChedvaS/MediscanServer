using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Bl;
using Entities;

namespace MediscanProject.Controllers
{
    [RoutePrefix("api/medicine")]
    public class MedicineController : ApiController
    {

            //שליפת רשימת התרופות
            [HttpGet]
            [Route("GetMedicineList")]
            public IHttpActionResult GetMedicineList()
            {
                return Ok(medicineBl.GetMedicineList());
            }

            //שליפת קטגוריה לפי קוד
            [HttpGet]
            [Route("GetMedicineById/{idMedicine}")]
            public IHttpActionResult GetMedicineById(int idMedicine)
            {
                return Ok(medicineBl.GetMedicineById(idMedicine));
            }

            //הוספה לרשימה
            [HttpPut]
            [Route("addMedicine")]
            public IHttpActionResult addMedicine(medicineEntities medicine)
            {
                try
                {
                    medicineBl.addMedicine(medicine);
                    return Ok(true);

                }
                catch
                {
                    return Ok(false);
                }
            }

            //עדכון תרופה ברשימה
            [HttpPost]
            [Route("Category")]
            public IHttpActionResult updateMedicine(medicineEntities medicine)
            {
                try
                {
                    medicineBl.updateMedicine(medicine);
                    return Ok(true);

                }
                catch
                {
                    return Ok(false);
                }

            }

            //הסרת קטגוריה מהרשימה
            [HttpDelete]
            [Route("deleteMedicine/{id}")]
            public IHttpActionResult deleteMedicine(int id)
            {
                try
                {
                    medicineBl.deleteMedicine(id);
                    return Ok(true);

                }
                catch
                {
                    return Ok(false);
                }
            }
    }
}
