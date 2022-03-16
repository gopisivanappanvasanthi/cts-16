using Cts.Feature.Search.Models;
using Sitecore.ContentSearch;
using Sitecore.ContentSearch.Linq.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Cts.Feature.Search.Controllers
{
    public class SearchWithPredicateController : ApiController
    {
        [Route("altudoapi/GetSearchResultForPredicate")]
        public IHttpActionResult GetSearchResultForPredicate(SearchParam param)
        {
            List<StadardSearchResult> searchResults = new List<StadardSearchResult>();
            var contextDB = Sitecore.Context.Database;
            ISearchIndex searchIndex = ContentSearchManager.GetIndex($"sitecore_{contextDB.Name}_index");
            var searchPredicate = GetSearchPredicateForArticle(param.searchKeyword);
            using(IProviderSearchContext searchContext = searchIndex.CreateSearchContext())
            {
                searchResults = searchContext.GetQueryable<SearchOutputModel>()
                                             .Where(searchPredicate)
                                             .Select(x => new StadardSearchResult
                                             {
                                                 SearchTitle = x.ArticleTitle,
                                                 SearchDescription = x.ArticleDescription
                                             }).ToList();
            }

            return Json(searchResults);
        }

        public static Expression<Func<SearchOutputModel, bool>> GetSearchPredicateForArticle(string searchTerm)
        {
            var predicate = PredicateBuilder.True<SearchOutputModel>();
            predicate = predicate.Or(x => x.TemplateName == "Article Page");
            predicate = predicate.And(x => x.ArticleTitle.Contains(searchTerm));
            predicate = predicate.And(x => x.ArticleDescription.Contains(searchTerm));
            return predicate;
        }
        public static Expression<Func<SearchOutputModel, bool>> GetSearchPredicateForProfile(string searchTerm)
        {
            var predicate = PredicateBuilder.True<SearchOutputModel>();
            predicate = predicate.Or(x => x.TemplateName == "Article Page");
            predicate = predicate.And(x => x.ArticleTitle.Contains(searchTerm));
            predicate = predicate.And(x => x.ArticleDescription.Contains(searchTerm));
            return predicate;
        }
    }
}
