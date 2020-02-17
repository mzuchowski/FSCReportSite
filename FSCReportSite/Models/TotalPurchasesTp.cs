using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace FSCReportSite.Models
{
    public partial class TotalPurchasesTp
    {
        [Key]
        public int Id { get; set; }
        public int? DateYear { get; set; }
        public int? DateMonth { get; set; }
        public int? ProductWeight { get; set; }
        public int? PurchasePointsFsc { get; set; }
        public int? PurchasePointsCw { get; set; }
        public float? PerformParam { get; set; }
        public float? CertificateParamFsc { get; set; }
        public float? CertificateParamCw { get; set; }
        public string CertificateName { get; set; }
    }
}
