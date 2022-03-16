using Sitecore.ContentSearch;
using Sitecore.ContentSearch.ComputedFields;
using Sitecore.Data.Items;
using Sitecore.Links;
using Sitecore.Links.UrlBuilders;
using Sitecore.Sites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cts.Foundation.Search.ComputedIndexes
{
    public class ArticleUrl : IComputedIndexField
    {
        public string FieldName { get; set; }
        public string ReturnType { get; set; }

        public object ComputeFieldValue(IIndexable indexable)
        {
            Item sitecoreItem = indexable as SitecoreIndexableItem;
            
            if (sitecoreItem == null)
                return null;

            if (sitecoreItem.TemplateID != Templates.Articles.ArticleTemplate)
                return null;
            var siteContext = SiteContextFactory.GetSiteContext(Constants.SiteName);
            Sitecore.Context.Site = siteContext;
            Sitecore.Context.Item = sitecoreItem;
            ItemUrlBuilderOptions itemUrlBuilder = new ItemUrlBuilderOptions
            {
                SiteResolving = true,
                AlwaysIncludeServerUrl = false,
                LowercaseUrls = true
            };
            var articleUrl = LinkManager.GetItemUrl(sitecoreItem, itemUrlBuilder);
            return articleUrl;
        }
    }
}