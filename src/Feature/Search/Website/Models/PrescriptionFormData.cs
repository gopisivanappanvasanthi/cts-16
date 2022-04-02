using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cts.Feature.Search.Models
{
    public class PrescriptionFormData
    {
        public string diagnosis { get; set; }
        public string patientBPlevel { get; set; }
        public string patientHeight { get; set; }
        public string patientWeight { get; set; }
    }
}