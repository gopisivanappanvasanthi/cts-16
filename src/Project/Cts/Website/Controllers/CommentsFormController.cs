using Cts.Project.Cts.Models;
using Sitecore.Data;
using Sitecore.Publishing;
using Sitecore.SecurityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cts.Project.Cts.Controllers
{
    public class CommentsFormController : Controller
    {
        [HttpGet]
        public ActionResult CommentsFormAction()
        {
            Comment comment = new Comment();
            return View("/Views/Cts/LeadershipProfile/CommentsForm.cshtml", comment);
        }
        [HttpPost]
        public ActionResult CommentsFormAction(Comment comment)
        {

            //Create a new comment item as child item for the article item

            //template
            TemplateID templateID = new TemplateID(new ID("{F846D510-AA86-423F-A87C-32891375DC31}"));
            //parent item
            var parentItem = Sitecore.Context.Item;
            var masterDB = Sitecore.Configuration.Factory.GetDatabase("master");
            var webDb = Sitecore.Configuration.Factory.GetDatabase("web");
            var parenteItemFrommMasterDb = masterDB.GetItem(parentItem.ID);
            //create item
            using (new SecurityDisabler())
            {
                var commentsItem = parenteItemFrommMasterDb.Add(comment.Name, templateID);
                //update the fields 
                commentsItem.Editing.BeginEdit();
                commentsItem["Comments"] = comment.Comments;
                commentsItem["Name"] = comment.Name;
                commentsItem["EmailId"] = comment.EmailId;
                commentsItem.Editing.EndEdit();

                PublishOptions publishOptions = new PublishOptions(masterDB,webDb,PublishMode.SingleItem,Sitecore.Context.Language,DateTime.Now);
                Publisher publisher = new Publisher(publishOptions);
                publisher.Options.RootItem = commentsItem;
                publisher.Options.Deep = true;
                publisher.Options.Mode = PublishMode.SingleItem;
                publisher.Publish();

            }

            return View("/Views/Cts/LeadershipProfile/ThankYou.cshtml");
        }
    }
}