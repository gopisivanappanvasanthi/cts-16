using Cts.Project.Cts.Models;
using Sitecore.Data.Fields;
using Sitecore.Links;
using Sitecore.Web.UI.WebControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cts.Project.Cts.Controllers
{
    public class CtsProfileController : Controller
    {
        // GET: CtsProfile
        public ActionResult Index()
        {
            var contextItem = Sitecore.Context.Item;

            LinkField linkField = contextItem.Fields["LeadershipProfile"];
            DateField dateField = contextItem.Fields["DateOfJoining"];
            CtsProfile ctsProfile = new CtsProfile
            {
                FirstName = new HtmlString(FieldRenderer.Render(contextItem, "FirstName")),
                LastName = new HtmlString(FieldRenderer.Render(contextItem, "LastName")),
                EmailId = new HtmlString(FieldRenderer.Render(contextItem, "EmailId")),
                LeaderDetailUrl = LinkManager.GetItemUrl(linkField.TargetItem),
                DateOfJoining = new HtmlString(FieldRenderer.Render(contextItem, "DateOfJoining")),
                JoiningDate = dateField.DateTime,
            };

            return View("/Views/Cts/LeadershipProfile/Profile.cshtml", ctsProfile);
        }
    }
}