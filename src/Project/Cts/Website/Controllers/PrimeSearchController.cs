using Sitecore.ContentSearch;
using Sitecore.ContentSearch.SearchTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace Cts.Project.Cts.Controllers
{
    public class PrimeSearchController : ApiController
    {
        [Route("altudo/GetSearchResult")]
        [HttpPost]
        public IHttpActionResult GetSearchResult(SearchTerm searchTerm)
        {

            ISearchIndex selectedIndex = ContentSearchManager.GetIndex("sitecore_master_index");
            List<ResultItem> results = new List<ResultItem>();
            using (IProviderSearchContext context = selectedIndex.CreateSearchContext())
            {
                results = context.GetQueryable<SearchResultItem>()
                                    .Where(x => x.Content.Contains(searchTerm.Term))
                                    .Select(x => new ResultItem
                                    {
                                        ResultTitle = x.Name,
                                    }).ToList();
            }

            return Json(results);
        }
    }
    public class SearchTerm
    {
        public string Term { get; set; }
    }

    public class ResultItem
    {
        public string ResultTitle { get; set; }
        public string ResultDescription { get; set; }
        public string ResultUrl { get; set; }
    }
}