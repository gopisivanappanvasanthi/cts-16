using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cts.Project.Cts.Models
{
    public class CtsProfile
    {
        public HtmlString FirstName { get; set; }
        public HtmlString LastName { get; set; }
        public HtmlString EmailId { get; set; }
        public string LeaderDetailUrl { get; set; }
        public HtmlString DateOfJoining { get; set; }
        public DateTime JoiningDate { get; set; }
    }
}