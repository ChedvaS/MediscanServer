using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Bl;
using Dal;
using Entities;

namespace MediscanBackend.Controllers
{

    [RoutePrefix("api/reminderdatails")]
    public class ReminderDetailsController : ApiController
    {
        //שליפת רשימת פרטי תזכורת
        [HttpGet]
        [Route("GetReminderDetailsList")]
        public IHttpActionResult GetReminderDetailsList()
        {
            return Ok(reminderdetailsBl.GetReminderDetailsList());
        }

        //שליפת פרטי תיזכורת לפי קוד
        [HttpGet]
        [Route("GetReminderDetailsById/{idReminderDetails}")]
        public IHttpActionResult GetReminderDetails(int idReminderDetails)
        {
            return Ok(reminderdetailsBl.GetReminderDetailsById(idReminderDetails));
        }

        //הוספה לרשימה
        [HttpPut]
        [Route("addReminderDetails")]
        public IHttpActionResult addReminderDetails(reminderdetailsEntities reminderdetails)
        {
            try
            {
                var id = reminderdetailsBl.addReminderDetails(reminderdetails);
                return Ok(id);

            }
            catch
            {
                return Ok(0);
            }
        }

        //עדכון פרטי תיזכורת ברשימה
        [HttpPost]
        [Route("updateReminderDetails")]
        public IHttpActionResult updateReminderDetails(reminderdetailsEntities reminderdetails)
        {
            try
            {
                reminderdetailsBl.updateReminderDetails(reminderdetails);
                return Ok(true);

            }
            catch
            {
                return Ok(false);
            }

        }

        //הסרת פרטי תיזכורת מהרשימה
        [HttpDelete]
        [Route("deleteReminderDetails/{id}")]
        public IHttpActionResult deleteReminderDetails(int id)
        {
            try
            {
                bool result = reminderdetailsBl.deleteReminderDetails(id);
                return Ok(result);

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return Ok(false);
            }
        }

        //שליפת פירוט לקיחה לפי מייל

        [HttpGet]
        [Route("GetTakingDetailsByGmail/{gmail}/1")]
        public IHttpActionResult GetTakingDetailsByGmail(string gmail)
        {
            return Ok(reminderdetailsDal.GetTakingDetailsByGmail(gmail));
        }
    }
}
