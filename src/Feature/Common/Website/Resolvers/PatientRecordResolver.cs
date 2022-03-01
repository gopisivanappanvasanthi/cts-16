using Newtonsoft.Json.Linq;
using Sitecore.Data;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using Sitecore.Globalization;
using Sitecore.LayoutService.Configuration;
using Sitecore.LayoutService.ItemRendering.ContentsResolvers;
using Sitecore.Mvc.Presentation;
using Sitecore.SecurityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cts.Feature.Common.Resolvers
{
    public class PatientRecordResolver : RenderingContentsResolver
    {
        public override object ResolveContents(Rendering rendering, IRenderingConfiguration renderingConfig)
        {
            var contextItem = GetContextItem(rendering, renderingConfig);
            MultilistField recordsFields = contextItem.Fields["records"];
            var patientRecordsItemList = recordsFields.GetItems()
                                                    .Select(x => ExtractPrescriptionFieldFromPatientRecord(x))
                                                    .ToList();
            JObject jobject = new JObject()
            {
                ["records"] = (JToken)new JArray()
            };
            jobject["records"] = ProcessItems(patientRecordsItemList, rendering, renderingConfig);
            return jobject;
        }

        private Item ExtractPrescriptionFieldFromPatientRecord(Item item)
        {
            var fakeId = new ID();
            //creating item definition
            var fakeItemDefinition = new ItemDefinition
                                            (fakeId, 
                                            item.Name, 
                                            new ID("{33C6F6DE-B97E-4455-968F-A3076CA58C9E}"), //template id (new template)
                                            ID.Null);
            //creating fields list
            var fields = new FieldList();
            //fields id
            fields.Add(new ID("{06EAA180-7A71-4558-B36E-F660E4536F66}"), item.Fields["patientPrescription"].Value);
            //assembling into a item data
            var itemData = new ItemData(fakeItemDefinition, Language.Current, Sitecore.Data.Version.First, fields);

            Item newItem = new Item(fakeId, itemData, Sitecore.Context.Database);
            return newItem;
        }
    }
}