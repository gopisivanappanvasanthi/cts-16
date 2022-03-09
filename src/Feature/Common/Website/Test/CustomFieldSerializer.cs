using Newtonsoft.Json;
using Sitecore.Data.Fields;
using Sitecore.LayoutService.Serialization;
using Sitecore.LayoutService.Serialization.FieldSerializers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cts.Feature.Common.Test
{
    public class CustomFieldSerializer : BaseFieldSerializer
    {
        public CustomFieldSerializer(IFieldRenderer fieldRenderer) : base(fieldRenderer)
        {
        }

        protected override void WriteValue(Field field, JsonTextWriter writer)
        {
            writer.WriteStartObject();
            writer.WritePropertyName(field.Name);
            writer.WriteValue("Your custom field value here.");
            writer.WriteEndObject();
        }
    }
}