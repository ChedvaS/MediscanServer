using Bl;
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

    }
}
