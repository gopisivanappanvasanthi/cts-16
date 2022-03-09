using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace Cts.Feature.Search.Controllers
{
    public class PrimeSearchController : ApiController
    {
        // GET: PrimeSearch
        [Route("altudoapi/GetSearchResult")]
        [HttpPost]
        public IHttpActionResult GetSearchResult(SearchParam param)
        {
            //ISearchIndex selectedIndex = ContentSearchManager.GetIndex(“sitecore_master_index”);


            return Json("Handling Search for the keyword: " + param.searchKeyword);
        }
    }
}