using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using FSCReportSite.Data;
using Microsoft.AspNetCore.Mvc;
using FSCReportSite.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace FSCReportSite.Controllers
{
    public class HomeController : Controller
    {
        public string prodType="";

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
                    return false;
                }
                
            }
        }

        public bool MaterialAndProductUpdate(string partProdIndex)
        {
            this.prodType = partProdIndex;

            using (var context = new ApplicationDbContext(_options))
            {
                switch (prodType)
                {
                    case "TP":
                        if (context != null)
                        {
                            try
                            {
                                var purchaseTp = context.Purchases
                                    .FromSql("SELECT * FROM Purchases WHERE LEFT(ProductIndex,2)='TP'").ToList();
                                purchaseTp.ForEach(a => a.ProductType = "4.1 Testliner");
                                purchaseTp.ForEach(a => a.ProductGroup = "P.3.2 Tektura Powlekana");

                                var salesTp = context.Sales
                                    .FromSql("SELECT * FROM Sales WHERE LEFT(ProductIndex,2)='TP'").ToList();
                                salesTp.ForEach(b => b.ProductType = "TEKTURA_PLASKA");
                                salesTp.ForEach(b => b.ProductGroup= "P.3.2 Tektura Powlekana");

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
                            return false;
                        }

                    case "TF":
                        if (context != null)
                        {
                            try
                            {
                                var purchasePf = context.Purchases
                                    .FromSql("SELECT * FROM Purchases WHERE LEFT(ProductIndex,2)='PF'").ToList();
                                purchasePf.ForEach(a => a.ProductGroup = "P.4 Papier Tektura Falista");

                                var purchasePfAndFlbFhp = context.Purchases
                                    .FromSql("SELECT * FROM Purchases WHERE LEFT(ProductIndex,2)='PF' AND SUBSTRING(ProductIndex,4,3)='FHP' OR LEFT(ProductIndex, 2) = 'PF' AND SUBSTRING(ProductIndex, 4, 3)='FHP'").ToList();
                                purchasePfAndFlbFhp.ForEach(b => b.ProductType = "4.2 Fluting");

                                var purchasePfNotFlbFhp = context.Purchases
                                    .FromSql("SELECT * FROM Purchases WHERE LEFT(ProductIndex,2)='PF' AND SUBSTRING(ProductIndex,4,3)<>'FHP'AND SUBSTRING(ProductIndex,4,3) <> 'FHP'").ToList();
                                purchasePfNotFlbFhp.ForEach(c => c.ProductType = "4.1 Testliner");

                                var salesPf = context.Sales
                                    .FromSql("SELECT * FROM Sales WHERE LEFT(ProductIndex,2)='PF'").ToList();
                                salesPf.ForEach(d => d.ProductType = "PAPIERY_DO_PRODUKCJI_FALI");
                                salesPf.ForEach(d => d.ProductGroup = "P.4 Papier Tektura Falista");

                                var salesTfOr35 = context.Sales
                                    .FromSql("SELECT * FROM Sales WHERE LEFT(ProductIndex,2) ='TF' OR LEFT(ProductIndex, 1) = '3' OR LEFT(ProductIndex, 1) = '5'").ToList();
                                salesTfOr35.ForEach(e => e.ProductType = "TEKTURA_FALISTA");
                                salesTfOr35.ForEach(e => e.ProductGroup = "P.4 Papier Tektura Falista");

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
                            return false;
                        }

                    default:
                        return false;
                }
            }

        }

        public bool DataUpdate() //PRZENIEŚĆ DO INNEGO CONTROLERA --TESTTOWA FUNCKCJA
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
                    return false;
                }
            }
        }

        public ViewResult test1()
        {
            if (ClearTables()==true)
            {
                @ViewData["Message"] = "Zaktualizowano Materiały";
                return View("MyAccount");
            }
            else
            {
                @ViewData["Message"] = "Niepowodzenie - aktualizacja materiałów";
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
