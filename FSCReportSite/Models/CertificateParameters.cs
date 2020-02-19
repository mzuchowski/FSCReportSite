using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FSCReportSite.Models
{
    public partial class CertificateParameters
    {
        [Key]
        public int Id { get; set; }
        public string CertificateName { get; set; }
        public float? ParameterFsc { get; set; }
        public float? ParameterCw { get; set; }
    }
}
