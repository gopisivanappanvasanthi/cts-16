using Cts.Project.Cts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cts.Project.Cts.Controllers
{
    public class CommentsListController : Controller
    {
        // GET: CommentsList
        public ActionResult GetCommentsList()
        {
            var contextItem = Sitecore.Context.Item;
            var CommentsList = contextItem.GetChildren()
                                .Where(x => x.TemplateName == "Comment")
                                .Select(x => new Comment
                                {
                                    Comments = x.Fields["Comments"].Value,
                                    EmailId = x.Fields["EmailId"].Value,
                                    Name = x.Fields["Name"].Value,
                                });

            return View("/Views/Cts/LeadershipProfile/DisplayComment.cshtml", CommentsList);
        }
    }
}