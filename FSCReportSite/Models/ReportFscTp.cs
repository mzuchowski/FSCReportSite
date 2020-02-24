using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FSCReportSite.Models
{
    public partial class ReportFscTp
    {
        [Key]
        public int Id { get; set; }
        public int? DateYear { get; set; }
        public int? DateMonth { get; set; }
        public int? PurchasePoints { get; set; }
        public int? SalesPoints { get; set; }
        public int? DifferencePoints { get; set; }
        public int? OldDifferencePoints { get; set; }
        public int? AmountOfPoints { get; set; }
    }
}
