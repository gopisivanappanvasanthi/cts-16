using Sitecore.Pipelines.HttpRequest;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Cts.Feature.Common.Pipelines
{
    public class PageNotFoundResolver : HttpRequestProcessor
    {
        public override void Process(HttpRequestArgs args)
        {
            //args.Url.FilePath
            if (args.Url.FilePath.Contains("/sitecore") || File.Exists(args.Url.FilePath) || args.Url.FilePath.Contains("altudoapi"))
                return;

            var contextItem = Sitecore.Context.Item;

            if(contextItem is null)
            {
                var pageNotFoundItem = Sitecore.Context.Database.GetItem(new Sitecore.Data.ID("{494331D1-6DC0-47B9-A1D7-FDF6640E7AA8}"));
                Sitecore.Context.Item = pageNotFoundItem;
            }

        }
    }
}