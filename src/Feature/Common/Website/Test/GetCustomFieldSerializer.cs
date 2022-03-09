using Sitecore.Diagnostics;
using Sitecore.LayoutService.Serialization;
using Sitecore.LayoutService.Serialization.Pipelines.GetFieldSerializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cts.Feature.Common.Test
{
    public class GetCustomFieldSerializer : BaseGetFieldSerializer
    {
        public GetCustomFieldSerializer(IFieldRenderer fieldRenderer)
    : base(fieldRenderer)
        {
        }
        protected override void SetResult(GetFieldSerializerPipelineArgs args)
        {
            Assert.ArgumentNotNull((object)args, nameof(args));
            args.Result = new CustomFieldSerializer(this.FieldRenderer);
        }
    }
}