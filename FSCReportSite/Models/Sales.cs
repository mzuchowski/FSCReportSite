using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FSCReportSite.Models
{
    public partial class Sales
    {
        [Key]
        public int Id { get; set; }
        public string OperationType { get; set; }
        public string Fsc { get; set; }
        public string ProductIndex { get; set; }
        public string Batch { get; set; }
        public int? Quantity { get; set; }
        public string Unit { get; set; }
        public float? Price { get; set; }
        public string NumberOfSaleDoc { get; set; }
        public string NumberOfInvoice { get; set; }
        public DateTime? DateOfSaleDoc { get; set; }
        public string Contractor { get; set; }
        public string WarehouseName { get; set; }
        public int? YearOfSaleDoc { get; set; }
        public int? MonthOfSaleDoc { get; set; }
        public string ProductType { get; set; }
        public string ProductGroup { get; set; }
    }
}
