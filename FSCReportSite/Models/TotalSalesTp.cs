using System;
using System.Collections.Generic;

namespace FSCReportSite.Models
{
    public partial class TotalSalesTp
    {
        public int Id { get; set; }
        public int? DateYear { get; set; }
        public int? DateMonth { get; set; }
        public int? SalesPoints { get; set; }
        public string CertificatName { get; set; }
    }
}
