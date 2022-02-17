
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
    public class LeadershipProfileController : Controller
    {
        // GET: LeadershipProfile
        public ActionResult GetLeadershipProfileInfo()
        {
            var contextItem = Sitecore.Context.Item;

            LeadershipProfile leadershipProfile = new LeadershipProfile();

            leadershipProfile.LeaderName = new HtmlString(FieldRenderer.Render(contextItem, "LeaderName"));
            leadershipProfile.Designation = new HtmlString(FieldRenderer.Render(contextItem, "Designation"));
            leadershipProfile.Brief = new HtmlString(FieldRenderer.Render(contextItem, "ProfileBrief"));
            leadershipProfile.LeaderImage = new HtmlString(FieldRenderer.Render(contextItem, "Image"));


            LinkField linkField = contextItem.Fields["ProfileDetail"];
            var targetItem = linkField.TargetItem;

            leadershipProfile.DetailPageUrl = LinkManager.GetItemUrl(targetItem);

            return View("/Views/Cts/LeadershipProfile/ProfileInfo.cshtml", leadershipProfile);
        }

        public ActionResult GetLeadershipArticles()
        {
            List<Article> articleList = new List<Article>();

            var contextItem = Sitecore.Context.Item;
            MultilistField multilistField = contextItem.Fields["Articles"];

            articleList = multilistField.GetItems()
                            .Select(x => new Article
                            {
                                ArticleTitle = new HtmlString(FieldRenderer.Render(x, "Title")),
                                ArticleBrief = new HtmlString(FieldRenderer.Render(x, "Brief")),
                                ArticleUrl = LinkManager.GetItemUrl(x)
                            }).ToList();

            return View("/Views/Cts/LeadershipProfile/Articles.cshtml", articleList);
        }
    }
}