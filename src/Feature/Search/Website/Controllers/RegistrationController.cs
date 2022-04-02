using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cts.Feature.Search.Controllers
{
    public class RegistrationController : Controller
    {
        [HttpPost]
        public ActionResult RegisterUser()
        {
            AccountManager
            return View();
        }
    }
    public class NewUserModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailId { get; set; }
    }
}