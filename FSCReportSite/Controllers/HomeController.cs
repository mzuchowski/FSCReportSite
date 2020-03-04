using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using FSCReportSite.Data;
using Microsoft.AspNetCore.Mvc;
using FSCReportSite.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Remotion.Linq.Parsing.Structure.IntermediateModel;

namespace FSCReportSite.Controllers
{
    public class HomeController : Controller
    {
        public string prodType = "";
        public string certType = "";
        public string ErrorMsg = "";

        private readonly DbContextOptions<ApplicationDbContext> _options;

        public HomeController(DbContextOptions<ApplicationDbContext> options)
        {
            _options = options;
        }

        public IActionResult Index()
        {
            return View();
        }
        [Authorize]
        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page."; // FUNCTION TO DELETE

            return View();
        }

        public ViewResult CustomerOrdersReport()
        {
            using (var abc = new ApplicationDbContext(_options))
            {

                if (abc != null)
                {
                    var xyz = abc.Purchases.Where(s => s.Id == 1);
                     ViewData["Message"] = "Your contact page." + xyz;
                    return View(xyz);
                }

                //ViewData["Message"] = "Brak wartości";

                return View();
            }
        }

        public ViewResult CertificateParametersForm()
        {
            return View();
        }

        public ViewResult CorrugatedBoardReportCW()
        {
            return View();
        }

        public ViewResult CorrugatedBoardReportFSC()
        {
            return View();
        }

        public ViewResult ManageAccounts()
        {

            ViewData["Message"] = "Your contact page.";
            return View();
        }

        public ViewResult MyAccount()
        {
            return View();
        }

        public ViewResult PerformanceFactorForm()
        {
            return View();
        }

        public ViewResult SolidBoardReportCW()
        {
            return View();
        }

        public ViewResult SolidBoardReportFSC()
        {
            return View();
        }

        public bool ImportData()
        {
            using (var context = new ApplicationDbContext(_options))
            {
                if (context != null)
                {
                    try
                    {
                        var salesDoc = context.Database.ExecuteSqlCommand("ImportDocData");

                        return true;
                    }
                    catch
                    {
                        return false;
                    }
                }
                else
                {
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
                                new {p.YearOfDocument, p.MonthOfDocument, p.Fsc}).Select(p =>
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
                            ErrorMsg = "Przesłany rodzaj certyfikatu jest nieprawidłowy";
                            return false;
                        }
                    }
                    catch (Exception ex)
                    {
                        ErrorMsg = ex.Message;
                        return false;
                    }
                }
                else
                {
                    ErrorMsg = "Klasa DbContext ma wartość null";
                    return false;
                }
            }
        }

        public bool CalculatePoints(string prodTypeParam, string certTypeParam)
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
                                    totalPurchasesTp.ForEach(b => b.PurchasePointsFsc = Convert.ToInt32(b.ProductWeight*b.PerformParam*b.CertificateParamFsc));
                                    context.SaveChanges();
                                }
                                else if (certType == "CW")
                                {
                                   totalPurchasesTp.ForEach(b => b.PurchasePointsCw = Convert.ToInt32(b.ProductWeight * b.PerformParam * b.CertificateParamCw));
                                    context.SaveChanges();
                                }
                                else
                                {
                                    ErrorMsg = "Przesłany rodzaj certyfikatu jest nieprawidłowy";
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
                                    totalPurchasesTf.ForEach(b => b.CertificateParamCw = Convert.ToInt32(b.ProductWeight * b.PerformParam * b.CertificateParamCw));
                                    context.SaveChanges();
                                }
                                else
                                {
                                    ErrorMsg = "Przesłany rodzaj certyfikatu jest nieprawidłowy";
                                    return false;
                                }
                            return true;
                        }
                        else
                        {
                            ErrorMsg = "Przesłany rodzaj certyfikatu jest nieprawidłowy";
                            return false;
                        }
                    }
                    catch (Exception ex)
                    {
                        ErrorMsg = ex.Message;
                        return false;
                    }
                }
                else
                {
                    ErrorMsg = "Klasa DbContext ma wartość null";
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
                                    .Where(a => a.CertificateName == purchasesTp.CertificateName).FirstOrDefault();

                                var performanceParam = context.PerformanceParameters.Where(p =>
                                        p.YearOfDocument == purchasesTp.DateYear &&
                                        p.MonthOfDocument == purchasesTp.DateMonth).Select(p => p.Tlperformance)
                                    .FirstOrDefault();

                                // var certificat = context.CertificateParameters.FromSql(
                                //     "SELECT * FROM CertificateParameters WHERE CertificateName ={0}",purchasesTp.CertificateName).FirstOrDefault();
                                var purchasesWithCert = context.TotalPurchasesTp
                                    .Where(a => a.CertificateName == purchasesTp.CertificateName).ToList();

                                purchasesWithCert.ForEach(a => a.PerformParam = performanceParam);
                                context.SaveChanges();

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
                                    ErrorMsg = "Przesłany rodzaj certyfikatu jest nieprawidłowy";
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
                                    .Where(a => a.CertificateName == purchasesTf.CertificateName).FirstOrDefault();

                                var performanceParam = context.PerformanceParameters.Where(p =>
                                        p.YearOfDocument == purchasesTf.DateYear &&
                                        p.MonthOfDocument == purchasesTf.DateMonth).Select(p => p.Tfperformance)
                                    .FirstOrDefault();

                                // var certificat = context.CertificateParameters.FromSql(
                                //     "SELECT * FROM CertificateParameters WHERE CertificateName ={0}",purchasesTp.CertificateName).FirstOrDefault();
                                var purchasesWithCert = context.TotalPurchasesTf
                                    .Where(a => a.CertificateName == purchasesTf.CertificateName).ToList();

                                purchasesWithCert.ForEach(a => a.PerformParam = performanceParam);
                                context.SaveChanges();

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
                                    ErrorMsg = "Przesłany rodzaj certyfikatu jest nieprawidłowy";
                                    return false;
                                }

                                
                            }
                            return true;
                        }
                        else
                        {
                            ErrorMsg = "Przesłany rodzaj certyfikatu jest nieprawidłowy";
                            return false;
                        }
                    }
                    catch(Exception ex)
                    {
                        ErrorMsg = ex.Message;
                        return false;
                    }
                }
                else
                {
                    ErrorMsg = "Klasa DbContext ma wartość null";
                    return false;
                }

            }
        }

        public bool ClearTables()
        {
            using (var context = new ApplicationDbContext(_options))
            {
                if (context != null)
                {
                    try
                    {
                        context.Database.ExecuteSqlCommand("TRUNCATE TABLE ReportFSC_TP");
                        context.Database.ExecuteSqlCommand("TRUNCATE TABLE ReportFSC_TF");
                        context.Database.ExecuteSqlCommand("TRUNCATE TABLE TotalPurchases_TP");
                        context.Database.ExecuteSqlCommand("TRUNCATE TABLE TotalPurchases_TF");
                        context.Database.ExecuteSqlCommand("TRUNCATE TABLE TotalSales_TP");
                        context.Database.ExecuteSqlCommand("TRUNCATE TABLE TotalSales_TF");
                        context.Database.ExecuteSqlCommand("TRUNCATE TABLE Sales");
                        context.Database.ExecuteSqlCommand("TRUNCATE TABLE Purchases");
                        context.SaveChanges();

                        return true;
                    }
                    catch
                    {
                        return false;
                    }
                }
                else
                {
                    ErrorMsg = "Klasa DbContext ma wartość null";
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
                                //var purchaseTp = context.Purchases
                                //    .FromSql("SELECT * FROM Purchases WHERE LEFT(ProductIndex,2)='TP'").ToList();

                                var purchaseTp = context.Purchases.Where(p => p.ProductIndex.Substring(0, 2) == "TP").ToList();
                                purchaseTp.ForEach(a => a.ProductType = "4.1 Testliner");
                                purchaseTp.ForEach(a => a.ProductGroup = "P.3.2 Tektura Powlekana");

                                //var salesTp = context.Sales
                                //    .FromSql("SELECT * FROM Sales WHERE LEFT(ProductIndex,2)='TP'").ToList();

                                var salesTp = context.Sales.Where(s => s.ProductIndex.Substring(0, 2) == "TP").ToList();
                                salesTp.ForEach(b => b.ProductType = "TEKTURA_PLASKA");
                                salesTp.ForEach(b => b.ProductGroup= "P.3.2 Tektura Powlekana");

                                context.SaveChanges();

                                return true;
                            }
                            catch(Exception ex)
                            {
                                ErrorMsg = ex.Message;
                                return false;
                            }
                        }
                        else
                        {
                            ErrorMsg = "Klasa DbContext ma wartość null";
                            return false;
                        }

                    case "TF":
                        if (context != null)
                        {
                            try
                            {
                                //var purchasePf = context.Purchases
                                //    .FromSql("SELECT * FROM Purchases WHERE LEFT(ProductIndex,2)='PF'").ToList();

                                var purchasePf = context.Purchases.Where(p => p.ProductIndex.Substring(0, 2) == "TF").ToList();
                                purchasePf.ForEach(a => a.ProductGroup = "P.4 Papier Tektura Falista");

                                //var purchasePfAndFlbFhp = context.Purchases
                                //    .FromSql("SELECT * FROM Purchases WHERE LEFT(ProductIndex,2)='PF' AND SUBSTRING(ProductIndex,4,3)='FHP' OR LEFT(ProductIndex, 2) = 'PF' AND SUBSTRING(ProductIndex, 4, 3)='FLB'").ToList();

                                var purchasePfAndFlbFhp = context.Purchases.Where(p =>
                                        p.ProductIndex.Substring(0, 2) == "PF" &&
                                        p.ProductIndex.Substring(4, 3) == "FHP" ||
                                        p.ProductIndex.Substring(0, 2) == "PF" &&
                                        p.ProductIndex.Substring(4, 3) == "FLB")
                                    .ToList();
                                purchasePfAndFlbFhp.ForEach(b => b.ProductType = "4.2 Fluting");
                                purchasePfAndFlbFhp.ForEach(b => b.ProductGroup = "P.4 Papier Tektura Falista");

                                //var purchasePfNotFlbFhp = context.Purchases
                                //  .FromSql("SELECT * FROM Purchases WHERE LEFT(ProductIndex,2)='PF' AND SUBSTRING(ProductIndex,4,3)<>'FHP'AND SUBSTRING(ProductIndex,4,3) <> 'FHP'").ToList();

                                var purchasePfNotFlbFhp = context.Purchases.Where(p =>
                                        p.ProductIndex.Substring(0, 2) == "PF" &&
                                        p.ProductIndex.Substring(4, 3) != "FHP" ||
                                        p.ProductIndex.Substring(0, 2) == "PF" &&
                                        p.ProductIndex.Substring(4, 3) != "FLB")
                                    .ToList();
                                purchasePfNotFlbFhp.ForEach(c => c.ProductType = "4.1 Testliner");
                                purchasePfNotFlbFhp.ForEach(c => c.ProductGroup = "P.4 Papier Tektura Falista");

                                //var salesPf = context.Sales
                                //    .FromSql("SELECT * FROM Sales WHERE LEFT(ProductIndex,2)='PF'").ToList();

                                var salesPf = context.Sales.Where(p => p.ProductIndex.Substring(0, 2) == "PF").ToList();
                                salesPf.ForEach(d => d.ProductType = "PAPIERY_DO_PRODUKCJI_FALI");
                                salesPf.ForEach(d => d.ProductGroup = "P.4 Papier Tektura Falista");

                                //var salesTfOr35 = context.Sales
                                //    .FromSql("SELECT * FROM Sales WHERE LEFT(ProductIndex,2) ='TF' OR LEFT(ProductIndex, 1) = '3' OR LEFT(ProductIndex, 1) = '5'").ToList();

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
                                ErrorMsg = ex.Message;
                                return false;
                            }
                        }
                        else
                        {
                            ErrorMsg = "Klasa DbContext ma wartość null";
                            return false;
                        }

                    default:
                        ErrorMsg = "Przesłany rodzaj materiału jest nieprawidłowy";
                        return false;
                }
            }

        }

        public bool DataUpdate() //PRZENIEŚĆ DO INNEGO CONTROLERA --TESTTOWA FUNCKCJA do usuniecia
        {
            using (var context = new ApplicationDbContext(_options))
            {

                if (context != null)
                {
                    var purchase = context.Purchases.FromSql("SELECT * FROM PURCHASES WHERE LEFT(ProductIndex,2)='PF'").ToList();
                    purchase.ForEach( a => a.Fsc = "test2020");
                    context.SaveChanges();
                    return true;
                }
                else
                {
                    ErrorMsg = "Klasa DbContext ma wartość null";
                    return false;
                }
            }
        }

        public ViewResult test1()
        {
            if (GroupPurchasesAndSales("TP")== true)
            {
                @ViewData["Message"] = "Zaktualizowano Materiały";
                return View("MyAccount");
            }
            else
            {
                @ViewData["Message"] = "Niepowodzenie - aktualizacja materiałów "+ErrorMsg;
                return View("MyAccount");
            }
        }
    

[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
