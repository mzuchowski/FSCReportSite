using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using FSCReportSite.Data;
using Microsoft.AspNetCore.Mvc;
using FSCReportSite.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Expressions;

namespace FSCReportSite.Controllers
{
    public class HomeController : Controller
    {
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
                    var xyz = abc.Purchases.First();
                    ViewData["Message"] = "Your contact page." + xyz.Contractor;
                    return View(xyz);
                }
                else
                {
                    return View();
                }
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

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public bool DataUpdate()
        {
            using (var context = new ApplicationDbContext(_options))
            {

                if (context != null)
                {
                    var purchase = context.Purchases.FromSql("SELECT * FROM PURCHASES WHERE LEFT(ProductIndex,2)='PF'").First();
                    purchase.Fsc = "test2020";
                    context.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public void test1()
        {
            if (DataUpdate())
            {
                @ViewData["Message"] = "Zaktualizowano";
            }
            else
            {
                @ViewData["Message"] = "Niepowodzenie";
            }
        }
    }
}
