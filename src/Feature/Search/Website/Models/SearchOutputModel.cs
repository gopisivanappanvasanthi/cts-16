using Sitecore.ContentSearch;
using Sitecore.ContentSearch.SearchTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cts.Feature.Search.Models
{
    public class SearchOutputModel : SearchResultItem
    {
        [IndexField("articletitle_t")]
        public string ArticleTitle { get; set; }
        [IndexField("articlebrief_t")]
        public string ArticleDescription { get; set; }

        [IndexField("articleurl_s")]
        public string ArticleUrl { get; set; }

        [IndexField("articlespeciality_s")]
        public string SpecialityName { get; set; }

        [IndexField("articlespecialitycode_s")]
        public string SpecialityCode { get; set; }
    }
}