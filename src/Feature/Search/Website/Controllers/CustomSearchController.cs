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
            return Json("Handling Search for the keyword: " + param.searchKeyword);
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
