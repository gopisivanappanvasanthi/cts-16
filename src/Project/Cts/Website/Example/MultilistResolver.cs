using Sitecore.LayoutService.ItemRendering.ContentsResolvers;
using Sitecore.LayoutService.Configuration;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.Mvc.Presentation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sitecore.Data;
using Newtonsoft.Json.Linq;

namespace Cts.Project.Cts.Example
{
    public class MultilistResolver : RenderingContentsResolver
    {
        public override object ResolveContents(Rendering rendering, IRenderingConfiguration renderingConfig)
        {
            Assert.ArgumentNotNull(rendering, nameof(rendering));
            Assert.ArgumentNotNull(renderingConfig, nameof(renderingConfig));

            Item ds = GetContextItem(rendering, renderingConfig);

            var multilistitemfieldid = new ID("");

            MultilistField multilistField = ds.Fields[multilistitemfieldid];

            JObject jobject = new JObject
            {
                ["multilistitems"] = (JToken)new JArray()
            };
            jobject["multilistitems"] = ProcessItems(multilistField.GetItems(), rendering, renderingConfig);

            return jobject;
        }
    }
}