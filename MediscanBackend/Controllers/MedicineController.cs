using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using Bl;
using Entities;

namespace MediscanBackend.Controllers
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
            [Route("updateMedicine")]
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

            //הסרת תרופה מהרשימה
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

        //פונקציה ששומרת התמונה של מדבקת התרופה
        [Route("saveSticker/{email}/{num}")]
        [HttpPost]
        public IHttpActionResult saveSticker(string email, int num)
        {
            Dictionary<string, short> s=new Dictionary<string, short>();
            var httpRequest = HttpContext.Current.Request;
            var postedFile = httpRequest.Files["sticker"];
            string filePath = "";
            if (postedFile != null)
            {
                string name = postedFile.FileName;
                name = name.Substring(0, name.IndexOf('.'));
                var fileName = name  + Path.GetExtension(postedFile.FileName);
                filePath = HttpContext.Current.Server.MapPath("~/Stickers/" + fileName);
                if (!File.Exists(filePath))
                    postedFile.SaveAs(filePath);
              s =medicineBl.PullTextFromSticker(filePath,email);
            }
            return Ok(s);
        }





    }
}
