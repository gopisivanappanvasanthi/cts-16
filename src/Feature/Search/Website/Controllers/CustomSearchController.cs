using Sitecore.ContentSearch;
using Sitecore.ContentSearch.SearchTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Cts.Feature.Search.Controllers
{
    public class CustomSearchController : ApiController
    {
        [Route("altudoapi/HandleSearch")]
        [HttpPost]
        public IHttpActionResult HandleSearch(SearchParam param)
        {
            #region Sample Data
            SearchResult searchResult1 = new SearchResult
            {
                SearchTitle = "Sample Title - 1",
                SearchDescription = "Sample Search Description -1",
                SearchTileUrl = "http://altudoapp.dev.local"
            };
            SearchResult searchResult2 = new SearchResult
            {
                SearchTitle = "Sample Title - 2",
                SearchDescription = "Sample Search Description -2",
                SearchTileUrl = "http://altudoapp.dev.local"
            };

            List<SearchResult> searchResults = new List<SearchResult>();
            searchResults.Add(searchResult1);
            searchResults.Add(searchResult2);

            #endregion

            var contextDB = Sitecore.Context.Database;

            //get index instance
            ISearchIndex searchIndex = ContentSearchManager.GetIndex($"sitecore_{contextDB.Name}_index");
            //create search context
            using (IProviderSearchContext searchContext = searchIndex.CreateSearchContext())
            {
                var searchResultFromSolr = searchContext.GetQueryable<SearchResultItem>()
                                                    .Where(x => x.TemplateName == "Article Page")
                                                    .Where(x => x.Content.Contains(param.searchKeyword))
                                                    .Select(x => new SearchResult
                                                    {
                                                        SearchTitle = Convert.ToString(x.Fields["articletitle_t"]),
                                                        SearchDescription = Convert.ToString(x.Fields["articlebrief_t"]),
                                                    }).ToList();
                searchResults = searchResultFromSolr;
            }
            //do query


            return Json(searchResults);
        }
    }

    public class SearchResult
    {
        public string SearchTitle { get; set; }
        public string SearchDescription { get; set; }
        public string SearchTileUrl { get; set; }
    }

    public class SearchParam
    {
        public string searchKeyword { get; set; }
    }

}
