using Sitecore.ContentSearch;
using Sitecore.ContentSearch.ComputedFields;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cts.Foundation.Search.ComputedIndexes
{
    public class Speciality : IComputedIndexField
    {
        public string FieldName { get; set; }
        public string ReturnType { get; set; }

        public object ComputeFieldValue(IIndexable indexable)
        {
            Item articleItem = indexable as SitecoreIndexableItem;

            if (articleItem is null)
                return null;

            if (articleItem.TemplateID != Templates.Articles.ArticleTemplate)
                return null;

            GroupedDroplinkField dropLinkField = articleItem.Fields[Templates.Articles.Fields.SpecialityName];

            return dropLinkField.TargetItem.Fields[Templates.SpecialityMaster.Fields.SpecialityNameFromMaster].Value;
        }
    }
}