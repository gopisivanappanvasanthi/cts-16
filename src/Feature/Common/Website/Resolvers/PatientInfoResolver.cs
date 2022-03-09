using Newtonsoft.Json.Linq;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.LayoutService.Configuration;
using Sitecore.LayoutService.ItemRendering.ContentsResolvers;
using Sitecore.Mvc.Presentation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cts.Feature.Common.Resolvers
{
    public class PatientInfoResolver : RenderingContentsResolver
    {
        public override object ResolveContents(Rendering rendering, IRenderingConfiguration renderingConfig)
        {
            var contextItem = GetContextItem(rendering, renderingConfig);

            JObject jobject = new JObject();
            JToken patientName = contextItem.Fields["patientName"].Value;
            JToken patientGender = contextItem.Fields["patientGender"].Value;

            jobject.Add(patientName);
            jobject.Add(patientGender);

            return jobject;
        }
    }
}