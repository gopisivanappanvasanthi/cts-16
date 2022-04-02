using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Cts.Feature.Contacts.Controllers
{
    public class DefaultController : ApiController
    {
        [HttpGet]
        [Route("altudoapi/testmethod")]
        public IHttpActionResult TestMethod()
        {
            return Json("");
        }
    }
}
