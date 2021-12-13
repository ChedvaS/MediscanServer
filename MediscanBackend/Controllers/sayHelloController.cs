using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MediscanBackend.Controllers
{
    [RoutePrefix("api/hello")]
    public class sayHelloController : ApiController
    {
        [HttpGet]
        [Route("sayHello")]
        public string GetReminderDetailsList()
        {
            return "hello";
        }
    }
}
