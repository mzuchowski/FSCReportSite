using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace FSCReportSite.Models
{
    public partial class TotalSalesTf
    {
        [Key]
        public int Id { get; set; }
        public int? DateYear { get; set; }
        public int? DateMonth { get; set; }
        public int? SalesPoints { get; set; }
        public string CertificatName { get; set; }
    }
}
