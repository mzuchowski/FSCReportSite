using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FSCReportSite.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FSCReportSite.Models
{
    public class Reports
    {
        public string prodType = "";
        public string certType = "";
        public string errorMsg = "";

        private readonly DbContextOptions<ApplicationDbContext> _options;
        private readonly DbContextOptions<SourceDbContext> _sourceOptions;

        public Reports(DbContextOptions<ApplicationDbContext> options, DbContextOptions<SourceDbContext> sourceOptions)
        {
            _options = options;
            _sourceOptions = sourceOptions;
        }

        public List<ReportFscTp> ShowReportFscTp()
        {
            using (var context = new ApplicationDbContext(_options))
            {
                var resultFscTp = context.ReportFscTp.ToList();
                return resultFscTp;
            }
        }

        public List<ReportFscTf> ShowReportFscTf()
        {
            using (var context = new ApplicationDbContext(_options))
            {
                var resultFscTf = context.ReportFscTf.ToList();
                return resultFscTf;
            }
        }

        public List<ReportCwTp> ShowReportCwTp()
        {
            using (var context = new ApplicationDbContext(_options))
            {
                var resultCwTp = context.ReportCwTp.ToList();
                return resultCwTp;
            }
        }

        public List<ReportCwTf> ShowReportCwTf()
        {
            using (var context = new ApplicationDbContext(_options))
            {
                var resultCwTf = context.ReportCwTf.ToList();
                return resultCwTf;
            }
        }

        public bool ImportData()
        {
            using (var sourceContext = new SourceDbContext(_sourceOptions))
            {
                if (sourceContext != null)
                {
                    try
                    {
                        var salesDoc = sourceContext.SalesDoc.Where(s => s.Fsc != null && s.Fsc != "");
                        var purchasesDoc = sourceContext.PurchasesDoc.Where(s => s.Fsc != null && s.Fsc != "");

                        using (var context = new ApplicationDbContext(_options))
                        {
                            if (context != null)
                            {
                                try
                                {
                                    foreach (var sale in salesDoc)
                                    {
                                        context.Sales.Add(new Sales
                                        {
                                            OperationType = sale.OperationType,
                                            Fsc = sale.Fsc,
                                            ProductIndex = sale.ProductIndex,
                                            Batch = sale.Batch,
                                            Quantity = sale.Quantity,
                                            Unit = sale.Unit,
                                            Price = sale.Price,
                                            NumberOfSaleDoc = sale.NumberOfSaleDoc,
                                            NumberOfInvoice = sale.NumberOfInvoice,
                                            DateOfSaleDoc = sale.DateOfSaleDoc,
                                            Contractor = sale.Contractor,
                                            WarehouseName = sale.WarehouseName,
                                            YearOfSaleDoc = sale.YearOfSaleDoc,
                                            MonthOfSaleDoc = sale.MonthOfSaleDoc
                                        });
                                        context.SaveChanges();
                                    }

                                    foreach (var purchase in purchasesDoc)
                                    {
                                        context.Purchases.Add(new Purchases
                                        {
                                            OperationType = purchase.OperationType,
                                            Fsc = purchase.Fsc,
                                            ProductIndex = purchase.ProductIndex,
                                            Batch = purchase.Batch,
                                            Quantity = purchase.Quantity,
                                            Unit = purchase.Unit,
                                            Price = purchase.Price,
                                            NumberOfPurchaseDoc = purchase.NumberOfPurchaseDoc,
                                            DateOfDocument = purchase.DateOfDocument,
                                            Contractor = purchase.Contractor,
                                            WarehouseName = purchase.WarehouseName,
                                            YearOfDocument = purchase.YearOfDocument,
                                            MonthOfDocument = purchase.MonthOfDocument
                                        });
                                        context.SaveChanges();
                                    }

                                    return true;
                                }
                                catch (Exception ex)
                                {
                                    errorMsg = ex.Message;
                                    return false;
                                }
                            }
                            else
                            {
                                return false;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        errorMsg = ex.Message;
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
        }

        public bool AddRowsToReport(string prodTypeParam, string certTypeParam)
        {
            this.prodType = prodTypeParam;
            this.certType = certTypeParam;

            using (var context =new ApplicationDbContext(_options))
            {
                var minYear = context.Purchases.Min(p => p.YearOfDocument);
                var maxYear = context.Purchases.Max(p => p.YearOfDocument);
                int month = 1;

                if (prodType == "TP" && certType == "FSC")
                {
                    try
                    {
                        while (minYear<=maxYear)
                        {
                            context.ReportFscTp.Add(new ReportFscTp()
                            {
                                DateYear = minYear,
                                DateMonth = month,
                                PurchasePoints = 0,
                                SalesPoints = 0
                            });
                            context.SaveChanges();

                            if (month < 12)
                            {
                                month++;
                            }
                            else
                            {
                                month = 1;
                                minYear++;
                            }
                        }
                        return true;
                    }
                    catch (Exception ex)
                    {
                        errorMsg = ex.Message;
                        return false;
                    }
                }
                else if (prodType == "TP" && certType == "CW")
                {
                    try
                    {
                        while (minYear<=maxYear)
                        {
                            context.ReportCwTp.Add(new ReportCwTp()
                            {
                                DateYear = minYear,
                                DateMonth = month,
                                PurchasePoints = 0,
                                SalesPoints = 0
                            });
                            context.SaveChanges();

                            if (month < 12)
                            {
                                month++;
                            }
                            else
                            {
                                month = 1;
                                minYear++;
                            }
                        }
                        return true;
                    }
                    catch (Exception ex)
                    {
                        errorMsg = ex.Message;
                        return false;
                    }
                }
                else if (prodType == "TF" && certType == "FSC")
                {
                    try
                    {
                        while (minYear <= maxYear)
                        {
                            context.ReportFscTf.Add(new ReportFscTf()
                            {
                                DateYear = minYear,
                                DateMonth = month,
                                PurchasePoints = 0,
                                SalesPoints = 0
                            });
                            context.SaveChanges();

                            if (month < 12)
                            {
                                month++;
                            }
                            else
                            {
                                month = 1;
                                minYear++;
                            }
                        }

                        return true;
                    }
                    catch (Exception ex)
                    {
                        errorMsg = ex.Message;
                        return false;
                    }
                }
                else if (prodType == "TF" && certType == "CW")
                {
                    try
                    {
                        while (minYear <= maxYear)
                        {
                            context.ReportCwTf.Add(new ReportCwTf()
                            {
                                DateYear = minYear,
                                DateMonth = month,
                                PurchasePoints = 0,
                                SalesPoints = 0
                            });
                            context.SaveChanges();

                            if (month < 12)
                            {
                                month++;
                            }
                            else
                            {
                                month = 1;
                                minYear++;
                            }
                        }

                        return true;
                    }
                    catch (Exception ex)
                    {
                        errorMsg = ex.Message;
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
        }

        public bool AddDataToReport(string prodTypeParam, string certTypeParam)
        {
            this.prodType = prodTypeParam;
            this.certType = certTypeParam;

            using (var context = new ApplicationDbContext(_options))
            {
                try
                {
                    if (prodType == "TP" && certType == "FSC")
                    {
                        var purchasesTpData = context.TotalPurchasesTp.GroupBy(p =>
                            new { p.DateMonth, p.DateYear }).Select(p =>
                              new
                              {
                                  p.Key.DateYear,
                                  p.Key.DateMonth,
                                  sumOfPurchasePoints = p.Sum(o => o.PurchasePointsFsc)
                              }).ToList();

                        var salesTpData = context.TotalSalesTp.Where(p => p.CertificatName != "FSC Controlled Wood").GroupBy(p =>
                            new { p.DateMonth, p.DateYear }).Select(p =>
                            new
                            {
                                p.Key.DateYear,
                                p.Key.DateMonth,
                                sumOfSalePoints = p.Sum(o => o.SalesPoints)
                            }).ToList();

                        var reportData = purchasesTpData.Join(salesTpData,
                            purchase => new { purchase.DateYear, purchase.DateMonth },
                            sale => new { sale.DateYear, sale.DateMonth },
                            (purchase, sale) => new
                            {
                                year = purchase.DateYear,
                                month = purchase.DateMonth,
                                purchasePoints = purchase.sumOfPurchasePoints,
                                salePoints = sale.sumOfSalePoints
                            }).ToList();

                        foreach (var report in reportData)
                        {
                            var recordToUpd = context.ReportFscTp.Where(s => s.DateYear == report.year && s.DateMonth == report.month).ToList();
                            recordToUpd.ForEach(p => p.PurchasePoints = report.purchasePoints);
                            recordToUpd.ForEach(p => p.SalesPoints = report.salePoints);
                            context.SaveChanges();
                        }

                        return true;

                    }
                    else if (prodType == "TP" && certType == "CW")
                    {
                        var purchasesTpData = context.TotalPurchasesTp.GroupBy(p =>
                            new { p.DateMonth, p.DateYear }).Select(p =>
                              new
                              {
                                  p.Key.DateYear,
                                  p.Key.DateMonth,
                                  sumOfPurchasePoints = p.Sum(o => o.PurchasePointsCw)
                              }).ToList();

                        var salesTpData = context.TotalSalesTp.Where(p => p.CertificatName == "FSC Controlled Wood").GroupBy(p =>
                            new { p.DateMonth, p.DateYear }).Select(p =>
                            new
                            {
                                p.Key.DateYear,
                                p.Key.DateMonth,
                                sumOfSalePoints = p.Sum(o => o.SalesPoints)
                            }).ToList();

                        var reportData = purchasesTpData.Join(salesTpData,
                            purchase => new { purchase.DateYear, purchase.DateMonth },
                            sale => new { sale.DateYear, sale.DateMonth },
                            (purchase, sale) => new
                            {
                                year = purchase.DateYear,
                                month = purchase.DateMonth,
                                purchasePoints = purchase.sumOfPurchasePoints,
                                salePoints = sale.sumOfSalePoints
                            }).ToList();

                        foreach (var report in reportData)
                        {
                            var recordToUpd = context.ReportCwTp.Where(s => s.DateYear == report.year && s.DateMonth == report.month).ToList();
                            recordToUpd.ForEach(p => p.PurchasePoints = report.purchasePoints);
                            recordToUpd.ForEach(p => p.SalesPoints = report.salePoints);
                            context.SaveChanges();
                        }

                        return true;
                    }
                    else if (prodType == "TF" && certType == "FSC")
                    {
                        var purchasesTfData = context.TotalPurchasesTf.GroupBy(p =>
                            new { p.DateMonth, p.DateYear }).Select(p =>
                              new
                              {
                                  p.Key.DateYear,
                                  p.Key.DateMonth,
                                  sumOfPurchasePoints = p.Sum(o => o.PurchasePointsFsc)
                              }).ToList();

                        var salesTfData = context.TotalSalesTf.Where(p => p.CertificatName != "FSC Controlled Wood").GroupBy(p =>
                            new { p.DateMonth, p.DateYear }).Select(p =>
                            new
                            {
                                p.Key.DateYear,
                                p.Key.DateMonth,
                                sumOfSalePoints = p.Sum(o => o.SalesPoints)
                            }).ToList();

                        var reportData = purchasesTfData.Join(salesTfData,
                            purchase => new { purchase.DateYear, purchase.DateMonth },
                            sale => new { sale.DateYear, sale.DateMonth },
                            (purchase, sale) => new
                            {
                                year = purchase.DateYear,
                                month = purchase.DateMonth,
                                purchasePoints = purchase.sumOfPurchasePoints,
                                salePoints = sale.sumOfSalePoints
                            }).ToList();

                        foreach (var report in reportData)
                        {
                            var recordToUpd = context.ReportFscTf.Where(s => s.DateYear == report.year && s.DateMonth == report.month).ToList();
                            recordToUpd.ForEach(p => p.PurchasePoints = report.purchasePoints);
                            recordToUpd.ForEach(p => p.SalesPoints = report.salePoints);
                            context.SaveChanges();
                        }

                        return true;
                    }
                    else if (prodType == "TF" && certType == "CW")
                    {
                        var purchasesTpData = context.TotalPurchasesTf.GroupBy(p =>
                            new { p.DateMonth, p.DateYear }).Select(p =>
                              new
                              {
                                  p.Key.DateYear,
                                  p.Key.DateMonth,
                                  sumOfPurchasePoints = p.Sum(o => o.PurchasePointsCw)
                              }).ToList();
                         
                        var salesTfData = context.TotalSalesTf.Where(p => p.CertificatName == "FSC Controlled Wood").GroupBy(p =>
                            new { p.DateMonth, p.DateYear }).Select(p =>
                            new
                            {
                                p.Key.DateYear,
                                p.Key.DateMonth,
                                sumOfSalePoints = p.Sum(o => o.SalesPoints)
                            }).ToList();

                        var reportData = purchasesTpData.Join(salesTfData,
                            purchase => new { purchase.DateYear, purchase.DateMonth },
                            sale => new { sale.DateYear, sale.DateMonth },
                            (purchase, sale) => new
                            {
                                year = purchase.DateYear,
                                month = purchase.DateMonth,
                                purchasePoints = purchase.sumOfPurchasePoints,
                                salePoints = sale.sumOfSalePoints
                            }).ToList();

                        foreach (var report in reportData)
                        {
                            var recordToUpd = context.ReportCwTf.Where(s => s.DateYear == report.year && s.DateMonth == report.month).ToList();
                            recordToUpd.ForEach(p => p.PurchasePoints = report.purchasePoints);
                            recordToUpd.ForEach(p => p.SalesPoints = report.salePoints);
                            context.SaveChanges();
                        }

                        return true;
                    }
                    else
                    {
                        errorMsg = "Przesłany rodzaj certyfikatu jest nieprawidłowy";
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    errorMsg = ex.Message;
                    return false;
                }
            }
        }

        public bool GroupPurchasesAndSales(string prodTypeParam)
        {
            this.prodType = prodTypeParam;

            using (var context = new ApplicationDbContext(_options))
            {
                if (context != null)
                {
                    try
                    {
                        if (prodType == "TP")
                        {
                            var purchasesGroupBy = context.Purchases.Where(p =>
                                p.ProductGroup == "P.3.2 Tektura Powlekana").GroupBy(p =>
                                new { p.YearOfDocument, p.MonthOfDocument, p.Fsc }).Select(p =>
                                  new
                                  {
                                      p.Key.YearOfDocument,
                                      p.Key.MonthOfDocument,
                                      sumOfQuantity = p.Sum(o => o.Quantity),
                                      p.Key.Fsc
                                  }).ToList();

                            foreach (var purchases in purchasesGroupBy)
                            {
                                context.TotalPurchasesTp.Add(new TotalPurchasesTp
                                {
                                    DateYear = purchases.YearOfDocument,
                                    DateMonth = purchases.MonthOfDocument,
                                    ProductWeight = purchases.sumOfQuantity,
                                    CertificateName = purchases.Fsc
                                });
                                context.SaveChanges();
                            }

                            var salesGroupBy = context.Sales.Where(p =>
                                p.ProductGroup == "P.3.2 Tektura Powlekana").GroupBy(p =>
                                new { p.YearOfSaleDoc, p.MonthOfSaleDoc, p.Fsc }).Select(p =>
                                new
                                {
                                    p.Key.YearOfSaleDoc,
                                    p.Key.MonthOfSaleDoc,
                                    sumOfQuantity = p.Sum(o => o.Quantity),
                                    p.Key.Fsc
                                }).ToList();

                            foreach (var sales in salesGroupBy)
                            {
                                context.TotalSalesTp.Add(new TotalSalesTp
                                {
                                    DateYear = sales.YearOfSaleDoc,
                                    DateMonth = sales.MonthOfSaleDoc,
                                    SalesPoints = sales.sumOfQuantity,
                                    CertificatName = sales.Fsc
                                });
                                context.SaveChanges();
                            }

                            return true;
                        }

                        else if (prodType == "TF")
                        {
                            var purchasesGroupBy = context.Purchases.Where(p =>
                                p.ProductGroup == "P.4 Papier Tektura Falista").GroupBy(p =>
                                new { p.YearOfDocument, p.MonthOfDocument, p.Fsc }).Select(p =>
                                new { p.Key.YearOfDocument, p.Key.MonthOfDocument, sumOfQuantity = p.Sum(o => o.Quantity), p.Key.Fsc }).ToList();

                            foreach (var purchases in purchasesGroupBy)
                            {
                                context.TotalPurchasesTf.Add(new TotalPurchasesTf
                                {
                                    DateYear = purchases.YearOfDocument,
                                    DateMonth = purchases.MonthOfDocument,
                                    ProductWeight = purchases.sumOfQuantity,
                                    CertificateName = purchases.Fsc
                                });
                                context.SaveChanges();
                            }

                            var salesGroupBy = context.Sales.Where(p =>
                                p.ProductGroup == "P.4 Papier Tektura Falista").GroupBy(p =>
                                new { p.YearOfSaleDoc, p.MonthOfSaleDoc, p.Fsc }).Select(p =>
                                new
                                {
                                    p.Key.YearOfSaleDoc,
                                    p.Key.MonthOfSaleDoc,
                                    sumOfQuantity = p.Sum(o => o.Quantity),
                                    p.Key.Fsc
                                }).ToList();

                            foreach (var sales in salesGroupBy)
                            {
                                context.TotalSalesTf.Add(new TotalSalesTf
                                {
                                    DateYear = sales.YearOfSaleDoc,
                                    DateMonth = sales.MonthOfSaleDoc,
                                    SalesPoints = sales.sumOfQuantity,
                                    CertificatName = sales.Fsc
                                });
                                context.SaveChanges();
                            }

                            return true;
                        }
                        else
                        {
                            errorMsg = "Przesłany rodzaj certyfikatu jest nieprawidłowy";
                            return false;
                        }
                    }
                    catch (Exception ex)
                    {
                        errorMsg = ex.Message;
                        return false;
                    }
                }
                else
                {
                    errorMsg = "Klasa DbContext ma wartość null";
                    return false;
                }
            }
        }

        public bool CalculateReportPoints(string prodTypeParam, string certTypeParam)
        {
            this.prodType = prodTypeParam;
            this.certType = certTypeParam;

            using (var context = new ApplicationDbContext(_options))
            {

                if (prodType == "TP" && certType == "FSC")
                {
                    var reportFsc = context.ReportFscTp.ToList();

                    foreach (var report in reportFsc)
                    {
                        var recordToUpd = reportFsc.Where(s => s.Id == report.Id).ToList();
                        var lastMonthPoints = reportFsc.Where(s => s.Id == report.Id - 1).Select(s => s.AmountOfPoints).SingleOrDefault();

                        if (report.OldDifferencePoints != null && report.OldDifferencePoints > 0)
                        {
                            recordToUpd.ForEach(s => s.AmountOfPoints = s.PurchasePoints + lastMonthPoints - s.SalesPoints - s.OldDifferencePoints);
                            context.SaveChanges();
                        }
                        else
                        {
                            if (lastMonthPoints == null || lastMonthPoints < 0)
                            {
                                lastMonthPoints = 0;
                            }

                            recordToUpd.ForEach(s =>
                                s.AmountOfPoints = s.PurchasePoints + lastMonthPoints - s.SalesPoints);
                            context.SaveChanges();
                        }
                    }
                    return true;
                }
                else if (prodType == "TF" && certType == "FSC")
                {
                    var reportFsc = context.ReportFscTf.ToList();

                    foreach (var report in reportFsc)
                    {
                        var recordToUpd = reportFsc.Where(s => s.Id == report.Id).ToList();
                        var lastMonthPoints = reportFsc.Where(s => s.Id == report.Id - 1).Select(s => s.AmountOfPoints).SingleOrDefault();

                        if (report.OldDifferencePoints != null && report.OldDifferencePoints > 0)
                        {
                            recordToUpd.ForEach(s =>
                                s.AmountOfPoints = s.PurchasePoints + lastMonthPoints - s.SalesPoints - s.OldDifferencePoints);
                            context.SaveChanges();
                        }
                        else
                        {
                            if (lastMonthPoints == null || lastMonthPoints < 0)
                            {
                                lastMonthPoints = 0;
                            }
                            recordToUpd.ForEach(s =>
                                s.AmountOfPoints = s.PurchasePoints + lastMonthPoints - s.SalesPoints);
                            context.SaveChanges();
                        }
                    }

                    return true;
                }
                else if (prodType == "TP" && certType == "CW")
                {
                    var reportCw = context.ReportCwTp.ToList();

                    foreach (var report in reportCw)
                    {
                        var recordToUpd = reportCw.Where(s => s.Id == report.Id).ToList();
                        var lastMonthPoints = reportCw.Where(s => s.Id == report.Id - 1).Select(s => s.AmountOfPoints).SingleOrDefault();

                        if (report.OldDifferencePoints != null && report.OldDifferencePoints > 0)
                        {
                            recordToUpd.ForEach(s =>
                                s.AmountOfPoints = s.PurchasePoints + lastMonthPoints - s.SalesPoints - s.OldDifferencePoints);
                            context.SaveChanges();
                        }
                        else
                        {
                            if (lastMonthPoints == null || lastMonthPoints < 0)
                            {
                                lastMonthPoints = 0;
                            }
                            recordToUpd.ForEach(s =>
                                s.AmountOfPoints = s.PurchasePoints + lastMonthPoints - s.SalesPoints);
                            context.SaveChanges();
                        }
                    }
                    return true;
                }
                else if (prodType == "TF" && certType == "CW")
                {
                    var reportCw = context.ReportCwTf.ToList();

                    foreach (var report in reportCw)
                    {
                        var recordToUpd = reportCw.Where(s => s.Id == report.Id).ToList();
                        var lastMonthPoints = reportCw.Where(s => s.Id == report.Id - 1)
                            .Select(s => s.AmountOfPoints).SingleOrDefault();

                        if (report.OldDifferencePoints != null && report.OldDifferencePoints > 0)
                        {
                            recordToUpd.ForEach(s =>
                                s.AmountOfPoints = s.PurchasePoints + lastMonthPoints - s.SalesPoints - s.OldDifferencePoints);
                            context.SaveChanges();
                        }
                        else
                        {
                            if (lastMonthPoints == null || lastMonthPoints < 0)
                            {
                                lastMonthPoints = 0;
                            }
                            recordToUpd.ForEach(s =>
                                s.AmountOfPoints = s.PurchasePoints + lastMonthPoints - s.SalesPoints);
                            context.SaveChanges();
                        }
                    }
                    return true;
                }
                else
                {
                    errorMsg = "Produkt lub certyfikat są nieprawidłowe";
                    return false;
                }

            }
        }

        public bool CalculatePurchuasePoints(string prodTypeParam, string certTypeParam)
        {
            this.prodType = prodTypeParam;
            this.certType = certTypeParam;

            using (var context = new ApplicationDbContext(_options))
            {
                if (context != null)
                {
                    try
                    {
                        if (prodType == "TP")
                        {
                            var totalPurchasesTp = context.TotalPurchasesTp.ToList();

                            if (certType == "FSC")
                            {
                                totalPurchasesTp.ForEach(b => b.PurchasePointsFsc = Convert.ToInt32(b.ProductWeight * b.PerformParam * b.CertificateParamFsc));
                                context.SaveChanges();
                            }
                            else if (certType == "CW")
                            {
                                totalPurchasesTp.ForEach(b => b.PurchasePointsCw = Convert.ToInt32(b.ProductWeight * b.PerformParam * b.CertificateParamCw));
                                context.SaveChanges();
                            }
                            else
                            {
                                errorMsg = "Przesłany rodzaj certyfikatu jest nieprawidłowy";
                                return false;
                            }
                            return true;
                        }
                        else if (prodType == "TF")
                        {
                            var totalPurchasesTf = context.TotalPurchasesTf.ToList();

                            if (certType == "FSC")
                            {
                                totalPurchasesTf.ForEach(b => b.PurchasePointsFsc = Convert.ToInt32(b.ProductWeight * b.PerformParam * b.CertificateParamFsc));
                                context.SaveChanges();
                            }
                            else if (certType == "CW")
                            {
                                totalPurchasesTf.ForEach(b => b.PurchasePointsCw = Convert.ToInt32(b.ProductWeight * b.PerformParam * b.CertificateParamCw));
                                context.SaveChanges();
                            }
                            else
                            {
                                errorMsg = "Przesłany rodzaj certyfikatu jest nieprawidłowy";
                                return false;
                            }
                            return true;
                        }
                        else
                        {
                            errorMsg = "Przesłany rodzaj certyfikatu jest nieprawidłowy";
                            return false;
                        }
                    }
                    catch (Exception ex)
                    {
                        errorMsg = ex.Message;
                        return false;
                    }
                }
                else
                {
                    errorMsg = "Klasa DbContext ma wartość null";
                    return false;
                }
            }
        }

        public bool AddParameters(string prodTypeParam, string certTypeParam)
        {
            this.prodType = prodTypeParam;
            this.certType = certTypeParam;

            using (var context = new ApplicationDbContext(_options))
            {
                if (context != null)
                {
                    try
                    {
                        if (prodType == "TP")
                        {
                            var totalPurchasesTp = context.TotalPurchasesTp.ToList();

                            foreach (var purchasesTp in totalPurchasesTp)
                            {
                                var certificat = context.CertificateParameters
                                    .Where(a => a.CertificateName == purchasesTp.CertificateName).SingleOrDefault();

                                var performanceParam = context.PerformanceParameters.Where(p =>
                                        p.YearOfDocument == purchasesTp.DateYear &&
                                        p.MonthOfDocument == purchasesTp.DateMonth).Select(p => p.Tlperformance)
                                    .SingleOrDefault();

                                // var certificat = context.CertificateParameters.FromSql(
                                //     "SELECT * FROM CertificateParameters WHERE CertificateName ={0}",purchasesTp.CertificateName).FirstOrDefault();
                                var purchasesWithCert = context.TotalPurchasesTp
                                    .Where(a => a.CertificateName == purchasesTp.CertificateName).ToList();

                                var recordToUpd = totalPurchasesTp
                                    .Where(s => s.Id == purchasesTp.Id).ToList();

                                if (performanceParam != null)
                                {
                                    recordToUpd.ForEach(s => s.PerformParam = performanceParam);
                                    context.SaveChanges();
                                }
                                else
                                {
                                    errorMsg = ("Nie odnaleziono współczynnika wydajności dla: Rok: " + purchasesTp.DateYear + " miesiąc: " + purchasesTp.DateMonth);
                                    return false;
                                }

                                if (certType == "FSC")
                                {
                                    purchasesWithCert.ForEach(b => b.CertificateParamFsc = certificat.ParameterFsc);
                                    context.SaveChanges();
                                }
                                else if (certType == "CW")
                                {
                                    purchasesWithCert.ForEach(b => b.CertificateParamCw = certificat.ParameterCw);
                                    context.SaveChanges();
                                }
                                else
                                {
                                    errorMsg = "Przesłany rodzaj certyfikatu jest nieprawidłowy";
                                    return false;
                                }
                            }
                            return true;
                        }
                        else if (prodType == "TF")
                        {
                            var totalPurchasesTf = context.TotalPurchasesTf.ToList();

                            foreach (var purchasesTf in totalPurchasesTf)
                            {
                                var certificat = context.CertificateParameters
                                    .Where(a => a.CertificateName == purchasesTf.CertificateName).SingleOrDefault();

                                var performanceParam = context.PerformanceParameters.Where(p =>
                                        p.YearOfDocument == purchasesTf.DateYear &&
                                        p.MonthOfDocument == purchasesTf.DateMonth).Select(p => p.Tfperformance)
                                    .SingleOrDefault();

                                // var certificat = context.CertificateParameters.FromSql(
                                //     "SELECT * FROM CertificateParameters WHERE CertificateName ={0}",purchasesTp.CertificateName).FirstOrDefault();
                                var purchasesWithCert = context.TotalPurchasesTf
                                    .Where(a => a.CertificateName == purchasesTf.CertificateName).ToList();

                                var recordToUpd = totalPurchasesTf
                                    .Where(s => s.Id == purchasesTf.Id).ToList();

                                if (performanceParam != null)
                                {
                                    recordToUpd.ForEach(a => a.PerformParam = performanceParam);
                                    context.SaveChanges();
                                }
                                else
                                {
                                    errorMsg = ("Nie odnaleziono współczynnika wydajności dla: Rok: " + purchasesTf.DateYear + " miesiąc: " + purchasesTf.DateMonth);
                                    return false;
                                }

                                if (certType == "FSC")
                                {
                                    purchasesWithCert.ForEach(b => b.CertificateParamFsc = certificat.ParameterFsc);
                                    context.SaveChanges();
                                }
                                else if (certType == "CW")
                                {
                                    purchasesWithCert.ForEach(b => b.CertificateParamCw = certificat.ParameterCw);
                                    context.SaveChanges();
                                }
                                else
                                {
                                    errorMsg = "Przesłany rodzaj certyfikatu jest nieprawidłowy";
                                    return false;
                                }


                            }
                            return true;
                        }
                        else
                        {
                            errorMsg = "Przesłany rodzaj certyfikatu jest nieprawidłowy";
                            return false;
                        }
                    }
                    catch (Exception ex)
                    {
                        errorMsg = ex.Message;
                        return false;
                    }
                }
                else
                {
                    errorMsg = "Klasa DbContext ma wartość null";
                    return false;
                }
            }
        }

        public bool ClearTables(string prodTypeParam, string certTypeParam)
        {
            this.prodType = prodTypeParam;
            this.certType = certTypeParam;

            using (var context = new ApplicationDbContext(_options))
            {
                try
                {
                    context.Database.ExecuteSqlCommand("TRUNCATE TABLE TotalPurchasesTp");
                    context.Database.ExecuteSqlCommand("TRUNCATE TABLE TotalPurchasesTf");
                    context.Database.ExecuteSqlCommand("TRUNCATE TABLE TotalSalesTp");
                    context.Database.ExecuteSqlCommand("TRUNCATE TABLE TotalSalesTf");
                    context.Database.ExecuteSqlCommand("TRUNCATE TABLE Sales");
                    context.Database.ExecuteSqlCommand("TRUNCATE TABLE Purchases");

                    if (prodType == "TP" && certType == "FSC")
                    {
                        context.Database.ExecuteSqlCommand("TRUNCATE TABLE ReportFscTp");
                    }
                    else if(prodType == "TP" && certType == "CW")
                    {
                        context.Database.ExecuteSqlCommand("TRUNCATE TABLE ReportCwTp");
                    }
                    else if (prodType == "TF" && certType == "FSC")
                    {
                        context.Database.ExecuteSqlCommand("TRUNCATE TABLE ReportFscTf");
                    }
                    else if (prodType == "TF" && certType == "CW")
                    {
                        context.Database.ExecuteSqlCommand("TRUNCATE TABLE ReportCwTf");
                    }
                    else
                    {
                        errorMsg = "Żaden raport nie został usunięty";
                        return true;
                    }
                    
                    context.SaveChanges();

                    return true;
                }
                catch (Exception ex)
                {
                    errorMsg = ex.Message;
                    return false;
                }
                
            }
        }

        public bool AddDifferenceFromPast(string prodTypeParam, string certTypeParam)
        {
            this.prodType = prodTypeParam;
            this.certType = certTypeParam;

            using (var context = new ApplicationDbContext(_options))
            {
                if (context != null)
                    try
                    {
                        if (prodType == "TP" && certType == "FSC")
                        {
                            var reportData = context.ReportFscTp.ToList();

                            foreach (var report in reportData)
                            {
                                var different = reportData.Where(w =>
                                    w.DateYear == report.DateYear - 1 &&
                                    w.DateMonth == report.DateMonth).Select(r => r.DifferencePoints).FirstOrDefault();

                                var recordToUpd = reportData.Where(w =>
                                    w.DateYear == report.DateYear &&
                                    w.DateMonth == report.DateMonth).ToList();


                                recordToUpd.ForEach(s => s.OldDifferencePoints = different);
                                context.SaveChanges();
                            }
                            return true;
                        }
                        else if (prodType == "TP" && certType == "CW")
                        {
                            var reportData = context.ReportCwTp.ToList();

                            foreach (var report in reportData)
                            {
                                var different = reportData.Where(w =>
                                    w.DateYear == report.DateYear - 1 &&
                                    w.DateMonth == report.DateMonth).Select(r => r.DifferencePoints).FirstOrDefault();

                                var recordToUpd = reportData.Where(w =>
                                     w.DateYear == report.DateYear &&
                                     w.DateMonth == report.DateMonth).ToList();

                                recordToUpd.ForEach(s => s.OldDifferencePoints = different);
                                context.SaveChanges();
                            }
                            return true;
                        }
                        else if (prodType == "TF" && certType == "FSC")
                        {
                            var reportData = context.ReportFscTf.ToList();

                            foreach (var report in reportData)
                            {
                                var different = reportData.Where(w =>
                                    w.DateYear == report.DateYear - 1 &&
                                    w.DateMonth == report.DateMonth).Select(r => r.DifferencePoints).FirstOrDefault();

                                var recordToUpd = reportData.Where(w =>
                                    w.DateYear == report.DateYear &&
                                    w.DateMonth == report.DateMonth).ToList();

                                recordToUpd.ForEach(s => s.OldDifferencePoints = different);
                                context.SaveChanges();
                            }
                            return true;
                        }
                        else if (prodType == "TF" && certType == "CW")
                        {
                            var reportData = context.ReportCwTf.ToList();

                            foreach (var report in reportData)
                            {
                                var different = reportData.Where(w =>
                                    w.DateYear == report.DateYear - 1 &&
                                    w.DateMonth == report.DateMonth).Select(r => r.DifferencePoints).FirstOrDefault();

                                var recordToUpd = reportData.Where(w =>
                                    w.DateYear == report.DateYear &&
                                    w.DateMonth == report.DateMonth).ToList();

                                recordToUpd.ForEach(s => s.OldDifferencePoints = different);
                                context.SaveChanges();
                            }
                            return true;
                        }
                        else
                        {
                            errorMsg = "Przesłany rodzaj produktu lub certyfikatu jest nieprawidłowy";
                            return false;
                        }
                    }
                    catch (Exception ex)
                    {
                        errorMsg = ex.Message;
                        return false;
                    }
                else
                {
                    errorMsg = "Klasa DbContext ma wartość null";
                    return false;
                }
            }
        }

        public bool UpdateFlutingProductWeight()  // TYLKO DLA RAPORTÓW TF
        {

            using (var context = new ApplicationDbContext(_options))
            {

                try
                {
                    var salesTf2Products = context.Sales
                        .Where(s => s.ProductIndex.Substring(0, 2) == "TF" && s.Unit == "m2").ToList();
                    var salesTf3Products = context.Sales
                        .Where(s => s.ProductIndex.Substring(0, 1) == "3" && s.Unit == "m2").ToList();
                    var salesTf5Products = context.Sales
                        .Where(s => s.ProductIndex.Substring(0, 1) == "5" && s.Unit == "m2").ToList();

                    foreach (var salesTf2 in salesTf2Products)
                    {
                        decimal gsm1 = decimal.Parse(salesTf2.ProductIndex.Substring(9, 3));
                        decimal gsm2 = decimal.Parse(salesTf2.ProductIndex.Substring(16, 3));
                        int weight = Convert.ToInt16((gsm1 + gsm2) / 1000 * salesTf2.Quantity);

                        var recordToUpd = salesTf2Products.Where(s => s.Id == salesTf2.Id).ToList();
                        recordToUpd.ForEach(s => s.Quantity = weight);
                        recordToUpd.ForEach(s => s.Unit = "kg");
                        context.SaveChanges();
                    }
                    foreach (var salesTf3 in salesTf3Products)
                    {
                        decimal gsm = decimal.Parse(salesTf3.ProductIndex.Substring(2, 4));
                        int weight = Convert.ToInt16(gsm / 1000 * salesTf3.Quantity);

                        var recordToUpd = salesTf3Products.Where(s => s.Id == salesTf3.Id).ToList();
                        recordToUpd.ForEach(s => s.Quantity = weight);
                        recordToUpd.ForEach(s => s.Unit = "kg");
                        context.SaveChanges();
                    }
                    foreach (var salesTf5 in salesTf5Products)
                    {
                        decimal gsm = decimal.Parse(salesTf5.ProductIndex.Substring(3, 4));
                        int weight = Convert.ToInt16(gsm / 1000 * salesTf5.Quantity);

                        var recordToUpd = salesTf5Products.Where(s => s.Id == salesTf5.Id).ToList();
                        recordToUpd.ForEach(s => s.Quantity = weight);
                        recordToUpd.ForEach(s => s.Unit = "kg");
                        context.SaveChanges();
                    }

                    return true;
                }
                catch (Exception ex)
                {
                    errorMsg = ex.Message;
                    return false;
                }
            }
        }

        public bool MaterialAndProductUpdate(string partProdIndexParam)
        {
            this.prodType = partProdIndexParam;

            using (var context = new ApplicationDbContext(_options))
            {
                switch (prodType)
                {
                    case "TP":
                        if (context != null)
                        {
                            try
                            {
                                var purchaseTp = context.Purchases.Where(p => p.ProductIndex.Substring(0, 2) == "TP").ToList();
                                purchaseTp.ForEach(a => a.ProductType = "4.1 Testliner");
                                purchaseTp.ForEach(a => a.ProductGroup = "P.3.2 Tektura Powlekana");

                                var salesTp = context.Sales.Where(s => s.ProductIndex.Substring(0, 2) == "TP").ToList();
                                salesTp.ForEach(b => b.ProductType = "TEKTURA_PLASKA");
                                salesTp.ForEach(b => b.ProductGroup = "P.3.2 Tektura Powlekana");

                                context.SaveChanges();

                                return true;
                            }
                            catch (Exception ex)
                            {
                                errorMsg = ex.Message;
                                return false;
                            }
                        }
                        else
                        {
                            errorMsg = "Klasa DbContext ma wartość null";
                            return false;
                        }

                    case "TF":
                        if (context != null)
                        {
                            try
                            {
                                var purchasePf = context.Purchases.Where(p => p.ProductIndex.Substring(0, 2) == "TF").ToList();
                                purchasePf.ForEach(a => a.ProductGroup = "P.4 Papier Tektura Falista");

                                var purchasePfAndFlbFhp = context.Purchases.Where(p =>
                                        p.ProductIndex.Substring(0, 2) == "PF" &&
                                        p.ProductIndex.Substring(4, 3) == "FHP" ||
                                        p.ProductIndex.Substring(0, 2) == "PF" &&
                                        p.ProductIndex.Substring(4, 3) == "FLB")
                                    .ToList();
                                purchasePfAndFlbFhp.ForEach(b => b.ProductType = "4.2 Fluting");
                                purchasePfAndFlbFhp.ForEach(b => b.ProductGroup = "P.4 Papier Tektura Falista");

                                var purchasePfNotFlbFhp = context.Purchases.Where(p =>
                                        p.ProductIndex.Substring(0, 2) == "PF" &&
                                        p.ProductIndex.Substring(4, 3) != "FHP" ||
                                        p.ProductIndex.Substring(0, 2) == "PF" &&
                                        p.ProductIndex.Substring(4, 3) != "FLB")
                                    .ToList();
                                purchasePfNotFlbFhp.ForEach(c => c.ProductType = "4.1 Testliner");
                                purchasePfNotFlbFhp.ForEach(c => c.ProductGroup = "P.4 Papier Tektura Falista");

                                var salesPf = context.Sales.Where(p => p.ProductIndex.Substring(0, 2) == "PF").ToList();
                                salesPf.ForEach(d => d.ProductType = "PAPIERY_DO_PRODUKCJI_FALI");
                                salesPf.ForEach(d => d.ProductGroup = "P.4 Papier Tektura Falista");

                                var salesTfOr35 = context.Sales.Where(p =>
                                    p.ProductIndex.Substring(0, 2) == "TF" ||
                                    p.ProductIndex.Substring(0, 1) == "3" ||
                                    p.ProductIndex.Substring(0, 1) == "5").ToList();
                                salesTfOr35.ForEach(e => e.ProductType = "TEKTURA_FALISTA");
                                salesTfOr35.ForEach(e => e.ProductGroup = "P.4 Papier Tektura Falista");

                                context.SaveChanges();

                                return true;
                            }
                            catch (Exception ex)
                            {
                                errorMsg = ex.Message;
                                return false;
                            }
                        }
                        else
                        {
                            errorMsg = "Klasa DbContext ma wartość null";
                            return false;
                        }

                    default:
                        errorMsg = "Przesłany rodzaj materiału jest nieprawidłowy";
                        return false;
                }
            }

        }

        public bool CalculateDifference(string prodTypeParam, string certTypeParam)
        {
            this.prodType = prodTypeParam;
            this.certType = certTypeParam;

            using (var context = new ApplicationDbContext(_options))
            {
                if (prodType == "TP" && certType == "FSC")
                {
                    var report = context.ReportFscTp.ToList();
                    report.ForEach(s => s.DifferencePoints = s.PurchasePoints - s.SalesPoints);
                    context.SaveChanges();
                    return true;
                }
                else if (prodType == "TP" && certType == "CW")
                {
                    var report = context.ReportCwTp.ToList();
                    report.ForEach(s => s.DifferencePoints = s.PurchasePoints - s.SalesPoints);
                    context.SaveChanges();
                    return true;
                }
                else if (prodType == "TF" && certType == "FSC")
                {
                    var report = context.ReportFscTf.ToList();
                    report.ForEach(s => s.DifferencePoints = s.PurchasePoints - s.SalesPoints);
                    context.SaveChanges();
                    return true;
                }
                else if (prodType == "TF" && certType == "CW")
                {
                    var report = context.ReportCwTf.ToList();
                    report.ForEach(s => s.DifferencePoints = s.PurchasePoints - s.SalesPoints);
                    context.SaveChanges();
                    return true;
                }
                else
                {
                    errorMsg = "Przesłany rodzaj materiału jest nieprawidłowy";
                    return false;
                }
            }
        }
    }
}