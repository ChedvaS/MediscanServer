using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Bl;
using Entities;

namespace MediscanBackend.Controllers
{

    [RoutePrefix("api/user")]
    public class UsersController : ApiController
    {
        //הוספה לרשימה
        [HttpPut]
        [Route("addUser")]
        public IHttpActionResult addReminderDetails(useresEntities userentities)
        {
            try
            {
                usersBl.addUser(userentities);
                return Ok(true);

            }
            catch (Exception e)
            {
                return Ok(false);
            }
        }

        //שליפת לקוח לפי קוד
        [HttpGet]
        [Route("GetUserById/{gmail}/1")]
        public IHttpActionResult GetUserById(string gmail)
        {
            return Ok(usersBl.GetUserById(gmail));
        }


    }
}
