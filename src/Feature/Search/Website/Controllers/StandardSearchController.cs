using Cts.Feature.Search.Models;
using Sitecore.ContentSearch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Cts.Feature.Search.Controllers
{
    public class StandardSearchController : ApiController
    {
        [Route("altudoapi/StandardResult")]
        [HttpPost]
        public IHttpActionResult GetStandardSearchResult(SearchParam param)
        {
            var contextDB = Sitecore.Context.Database;
            List<StadardSearchResult> searchResults = new List<StadardSearchResult>();
            ISearchIndex searchIndex = ContentSearchManager.GetIndex($"sitecore_{contextDB.Name}_index");
            using (IProviderSearchContext searchContext = searchIndex.CreateSearchContext())
            {
                searchResults = searchContext.GetQueryable<SearchOutputModel>()
                                                .Where(x => x.TemplateName == "Article Page")
                                                .Where(x => x.ArticleTitle.Contains(param.searchKeyword))
                                                .Where(x => x.ArticleDescription.Contains(param.searchKeyword))
                                                .Select(x => new StadardSearchResult
                                                {
                                                    SearchTitle = x.ArticleTitle,
                                                    SearchDescription = x.ArticleDescription,
                                                    SearchTileUrl = x.ArticleUrl,
                                                    SpecialityName = x.SpecialityName,
                                                    SpecialityCode = x.SpecialityCode
                                                }).ToList();
            }
            return Json(searchResults);
        }
    }
}
