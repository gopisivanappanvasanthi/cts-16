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
            ResultItem result = new ResultItem
            {
                ResultTitle = "Test Title",
                ResultDescription = "Test Description",
                ResultUrl = "https://google.com"
            };
            List<ResultItem> results = new List<ResultItem>();
            results.Add(result);

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