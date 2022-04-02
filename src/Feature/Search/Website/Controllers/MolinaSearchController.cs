using Cts.Feature.Search.Models;
using Sitecore.ContentSearch;
using Sitecore.ContentSearch.Linq.Utilities;
using Sitecore.ContentSearch.SearchTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Cts.Feature.Search.Controllers
{
    public class MolinaSearchController : ApiController
    {
        [HttpPost]
        [Route("molinahcapi/search")]
        public IHttpActionResult Search(SearchTerm term)
        {
            var dbName = Sitecore.Context.Database.Name;
            //get index
            ISearchIndex searchIndex = ContentSearchManager.GetIndex($"sitecore_{dbName}_index");
            //create search context
            List<SearchResultType> searchResults = new List<SearchResultType>();
            var searchPredicates = getPredicate(term.searchKeyword);
            using(IProviderSearchContext searchContext = searchIndex.CreateSearchContext())
            {
                //query the index using the search context
                searchResults = searchContext.GetQueryable<HealthArticleModel>()
                                            .Where(searchPredicates)
                                            .Select(x => new SearchResultType
                                            {
                                                searchTitle = x.ArticleTitle.ToString(),
                                                serachDescription = x.ArticleBrief.ToString(),
                                            }).ToList();

                //build the search result output
            }
            return Json(searchResults);
        }

        private Expression<Func<HealthArticleModel, bool>> getPredicate(string searchKeyword)
        {
            var predicate = PredicateBuilder.True<HealthArticleModel>();
            
            //predicate = predicate.And(x => x.TemplateName == Constants.TemplateNameForSearch);
            predicate = predicate.And(x => x.Content.Contains(searchKeyword));
            predicate = predicate.Or(x => x.Name.Contains(searchKeyword));
            predicate = predicate.Or(x => x.Fields["_displayname"].ToString().Contains(searchKeyword));
            return predicate;
        }

    }
    public class SearchTerm
    {
        public string searchKeyword { get; set; }
    }
    public class SearchResultType
    {
        public string searchTitle { get; set; }
        public string serachDescription { get; set; }
    }
}
