using System;
using System.Collections.Generic;

namespace FSCReportSite.Models
{
    public partial class ReportFscTp
    {
        public int Id { get; set; }
        public int? DateYear { get; set; }
        public int? DateMonth { get; set; }
        public int? PurchasePoints { get; set; }
        public int? SalesPoints { get; set; }
        public int? DifferencePoints { get; set; }
        public int? AmountOfPoints { get; set; }
    }
}
