using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FSCReportSite.Models
{
    public partial class PerformanceParameters
    {
        [Key]
        public int Id { get; set; }
        public int? YearOfDocument { get; set; }
        public int? MonthOfDocument { get; set; }
        public float? Tlperformance { get; set; }
        public float? Tfperformance { get; set; }
    }
}
