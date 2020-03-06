using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FSCReportSite.Models
{
    public partial class PurchasesDoc
    {
        [Key]
        public int Id { get; set; }
        public string OperationType { get; set; }
        public string Fsc { get; set; }
        public string ProductIndex { get; set; }
        public string Batch { get; set; }
        public int? Quantity { get; set; }
        public int? QuantityAccepted { get; set; }
        public string Unit { get; set; }
        public float? Price { get; set; }
        public string NumberOfPurchaseDoc { get; set; }
        public DateTime? DateOfDocument { get; set; }
        public string Contractor { get; set; }
        public string WarehouseName { get; set; }
        public int? YearOfDocument { get; set; }
        public int? MonthOfDocument { get; set; }
        public string ProductType { get; set; }
        public string ProductGroup { get; set; }
    }
}
