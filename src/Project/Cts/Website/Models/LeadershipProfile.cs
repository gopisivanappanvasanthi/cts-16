using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cts.Project.Cts.Models
{
    public class LeadershipProfile
    {
        public HtmlString LeaderName { get; set; }
        public HtmlString Designation { get; set; }
        public HtmlString Brief { get; set; }
        public HtmlString LeaderImage { get; set; }
        public string DetailPageUrl { get; set; }
    }
}