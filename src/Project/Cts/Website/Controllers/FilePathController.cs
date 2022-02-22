using Cts.Project.Cts.Models;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using Sitecore.Links;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cts.Project.Cts.Controllers
{
    public class FilePathController : Controller
    {
        // GET: FilePath
        public ActionResult GetBreadcrumb()
        {
            var contextItem = Sitecore.Context.Item;

            var homeItem = Sitecore.Context.Database.GetItem(Sitecore.Context.Site.StartPath);
            NavigationItem currentItemNav = new NavigationItem
            {
                NavTitle = contextItem.DisplayName,
                NavUrl = LinkManager.GetItemUrl(contextItem)
            };

            var navItemList = contextItem.Axes.GetAncestors()
                                    .Where(x => x.Axes.IsDescendantOf(homeItem)) 
                                    .Where(x => CheckForNavigableOption(x))
                                    .Select(x => new NavigationItem
                                    {
                                        NavTitle = x.DisplayName,
                                        NavUrl = LinkManager.GetItemUrl(x)
                                    })
                                    .ToList()
                                    .Concat(new List<NavigationItem> { currentItemNav });

            return View("/Views/Cts/Common/Breadcrumb.cshtml", navItemList);
        }

        private bool CheckForNavigableOption(Item item)
        {
            CheckboxField checkbox = item.Fields["IsNavigable"];
            return checkbox.Checked;
        }
    }
}