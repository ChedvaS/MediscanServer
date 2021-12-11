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
                catch
                {
                    return Ok(false);
                }
            }

        }
}
