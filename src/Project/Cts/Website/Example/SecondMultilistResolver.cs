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
using Sitecore.Globalization;

namespace Cts.Project.Cts.Example
{
    public class SecondMultilistResolver : RenderingContentsResolver
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

            var createdcustomitem = multilistField.GetItems()
                                            .Select(x => MapItemToRenderingModelItem(x))
                                            .ToList();

            jobject["multilistitems"] = ProcessItems(createdcustomitem, rendering, renderingConfig);

            return jobject;

        }

        private Item MapItemToRenderingModelItem(Item item)
        {
            var newId = new ID();
            var def = new ItemDefinition(newId, item.Name, new ID(""), ID.Null);
            var fields = new FieldList();
            fields.Add(new ID(""), "");
            fields.Add(new ID(""), "");
            fields.Add(new ID(""), "");

            var data = new ItemData(def, Language.Current, Sitecore.Data.Version.First, fields);
            var db = Sitecore.Context.Database;
            var resultItem = new Item(newId, data, db);
            return resultItem;
        }
    }
}