using Sitecore.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cts.Project.Cts
{
    public class Templates
    {
        public struct Articles
        {
            public static readonly ID ArticleTemplateId = new ID("{82EB1EFB-AFCC-45A6-9D9A-0C68F213E359}");

            public struct Fields
            {
                public static readonly ID ArticleTitle = new ID("{F046DD94-9D39-4501-8528-33FE7BC69FBD}");
                public static readonly ID ArticleBrief = new ID("{0D619AD0-E2CA-487F-9220-625041A6ECAB}");
                public static readonly ID FeaturedImage = new ID("{B949AF08-3E2B-45A6-B173-7530CA1FDC32}");
            }
        }
    }
}