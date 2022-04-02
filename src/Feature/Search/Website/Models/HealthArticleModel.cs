using Sitecore.ContentSearch;
using Sitecore.ContentSearch.SearchTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cts.Feature.Search.Models
{
    public class HealthArticleModel : SearchResultItem
    {
        [IndexField("articletitle_t")]
        public string ArticleTitle { get; set; }
        [IndexField("articlebrief_t")]
        public string ArticleBrief { get; set; }
    }
}