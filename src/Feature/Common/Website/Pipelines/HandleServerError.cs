using Sitecore.Pipelines.HttpRequest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cts.Feature.Common.Pipelines
{
    public class HandleServerError : Sitecore.Pipelines.HttpRequest.HttpRequestProcessor
    {
        public override void Process(HttpRequestArgs args)
        {
            if(HttpContext.Current.Response.StatusCode == 500)
            {
                var pageNotFoundItem = Sitecore.Context.Database.GetItem(new Sitecore.Data.ID("{494331D1-6DC0-47B9-A1D7-FDF6640E7AA8}"));
                Sitecore.Context.Item = pageNotFoundItem;
            }
        }
    }
}