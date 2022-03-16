using Sitecore.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cts.Foundation.Search
{
    public class Templates
    {
        public struct Articles
        {
            public static readonly ID ArticleTemplate = new ID("{82EB1EFB-AFCC-45A6-9D9A-0C68F213E359}");
            public struct Fields
            {
                public static readonly ID SpecialityName = new ID("{9342A193-5657-4158-87FA-52AA10217253}");
            }
        }
        public struct SpecialityMaster
        {
            public static readonly ID SpecialityMasterTemplate = new ID("{C6FFD643-1F8A-4BF2-B935-DBA8920E2BE7}");

            public struct Fields
            {
                public static readonly ID SpecialityNameFromMaster = new ID("{532E05C5-73FF-4ECF-8670-C19B1C2F4D33}");
            }
        }
    }
}