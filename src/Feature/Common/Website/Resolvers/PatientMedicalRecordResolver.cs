using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json.Linq;
using Sitecore.Data;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using Sitecore.Globalization;
using Sitecore.LayoutService.Configuration;
using Sitecore.LayoutService.ItemRendering.ContentsResolvers;
using Sitecore.Mvc.Presentation;

namespace Cts.Feature.Common.Resolvers
{
    public class PatientMedicalRecordResolver : RenderingContentsResolver
    {
        //access your context item
        //create json object of your item fields
        //use process items method to populate your json object and return 

        public override object ResolveContents(Rendering rendering, IRenderingConfiguration renderingConfig)
        {
            var contextItem = GetContextItem(rendering, renderingConfig);
            MultilistField patientRecords = contextItem.Fields["records"];
            JObject jobject = new JObject()
            {
                ["records"] = (JToken)new JArray()
            };
            var patientRecordItems = patientRecords.GetItems()
                                            .Select(x => ExtractRecordFieldFromItem(x))
                                            .ToList();

            jobject["records"] = ProcessItems(patientRecordItems, rendering, renderingConfig);
            return jobject;
        }

        private Item ExtractRecordFieldFromItem(Item item)
        {
            var fakeId = new ID();
            //create an item definition
            var def = new ItemDefinition
                                (fakeId, 
                                item.Name, 
                                new ID("{5CBF883A-547E-44AE-A43E-D6F0F6870C24}"), //template id
                                ID.Null);
            //extract fields
            var fields = new FieldList();
            fields.Add(new ID("{50C8094E-6ADB-47A0-B21C-37AC23DDB642}"),item.Fields["prescription"].Value);
            //assemble the item data
            var data = new ItemData(def, Language.Current, Sitecore.Data.Version.First, fields);
            //instantiate using the definition and data
            var resultItem = new Item(fakeId, data, Sitecore.Context.Database);
            return resultItem;
        }
    }
}