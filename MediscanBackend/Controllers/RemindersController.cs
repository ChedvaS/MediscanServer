using Bl;
using Dal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MediscanBackend.App_Start
{
    [RoutePrefix("api/reminders")]
    public class RemindersController : ApiController
    {
        //שליפת פרטי תיזכורת לפי קוד
        [HttpGet]
        [Route("GetReminderById/{idReminders}")]
        public IHttpActionResult GetReminder(int idReminders)
        {
            return Ok(remindersBl.GetReminderById(idReminders));
        }

        
        //שליפת פרטי תיזכורת לפי מייל
        [HttpGet]
        [Route("GetReminderByGmail/{gmail}/1")]
        public IHttpActionResult GetReminderByGmail(string gmail)
        {
            return Ok(remindersBl.GetReminderByGmail(gmail));
        }
        

        //שליפת תיזכורות פעילות לפי מייל  
        [HttpGet]
        [Route("GetActivityRemindersByGmail/{gmail}/1")]
        public IHttpActionResult GetActivityRemindersByGmail(string gmail)
        {
            return Ok(remindersDal.GetActivityRemindersByGmail(gmail));
        }



    }
}
