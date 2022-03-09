using Cts.Project.Cts.Models;
using Newtonsoft.Json;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using Sitecore.Links;
using Sitecore.Resources.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;

namespace Cts.Project.Cts.Controllers
{

    public class ArticlesController : ApiController
    {
        [Route("altudoapi/GetArticles")]
        public IHttpActionResult GetArticles()
        {
            var contextItem = Sitecore.Configuration.Factory.GetDatabase("master").GetItem(new Sitecore.Data.ID("{A297B4FA-8A3E-4BC7-9A01-4B4AAB28D482}"));

            var listofArticles = contextItem.GetChildren()
                                            .Select(x => new JsonArticle
                                            {
                                                Name = x.Name,
                                                Title = x.Fields["articleTitle"].Value,
                                                Brief = x.Fields["articleBrief"].Value,
                                                ImageUrl = getImageUrl(x, "featuredImage"),
                                            })
                                            .ToList();
            return Json(listofArticles);
        }

        private string getImageUrl(Item item, string fieldName)
        {
            ImageField image = item.Fields[fieldName];
            return MediaManager.GetMediaUrl(image.MediaItem);
        }
        [Route("altudoapi/GetFullBleedImageFromArticles")]
        public IHttpActionResult GetFullBleedImageFromArticles()
        {
            var parentItem = Sitecore.Configuration.Factory.GetDatabase("master").GetItem(new Sitecore.Data.ID("{A297B4FA-8A3E-4BC7-9A01-4B4AAB28D482}"));
            var listOfCarouselImages = parentItem.GetChildren()
                                                .Select(x => new CarouselImages
                                                {
                                                    CarouselTitle = x.Fields["articleTitle"].Value,
                                                    CarouselImageUrl = getImageUrl(x, "fullBleedImage"),
                                                    CarouselUrl = LinkManager.GetItemUrl(x)
                                                }).ToList();

            return Json(listOfCarouselImages);
        }
    }

    public class JsonArticle
    {
        public string Name { get; set; }
        public string Title { get; set; }

        public string Brief { get; set; }
        public string ImageUrl { get; set; }
    }

    public class CarouselImages
    {
        public string CarouselImageUrl { get; set; }
        public string CarouselTitle { get; set; }
        public string CarouselUrl { get; set; }
    }

   


}
