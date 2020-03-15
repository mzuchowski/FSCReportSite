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
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Remotion.Linq.Parsing.Structure.IntermediateModel;

namespace FSCReportSite.Controllers
{
    public class HomeController : Controller
    {
        private readonly DbContextOptions<ApplicationDbContext> _options;
        private readonly DbContextOptions<SourceDbContext> _sourceOptions;

        public HomeController(DbContextOptions<ApplicationDbContext> options, DbContextOptions<SourceDbContext> sourceOptions)
        {
            _options = options;
            _sourceOptions = sourceOptions;
        }

        //  public HomeController(DbContextOptions<SourceDbContext> sourceOptions)
        // {
        //     _sourceOptions = sourceOptions;
        // }

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
                return View();
        }

        public ViewResult CertificateParametersForm()
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

        
        public ViewResult test1()
        {
            Reports rep = new Reports(_options, _sourceOptions);
            if (

              /*  ClearTables() == true &&
                ImportData() == true &&
                MaterialAndProductUpdate("TP") == true &&
                GroupPurchasesAndSales("TP") == true &&
                AddParameters("TP", "FSC") == true &&
                CalculatePurchuasePoints("TP", "FSC") == true &&
                AddDataToReport("TP", "FSC") == true &&
                CalculateDifference("TP", "FSC") ==true &&
                AddDifferenceFromPast("TP", "FSC") == true &&
                CalculateReportPoints("TP", "FSC") == true
                */
            rep.CalculatePurchuasePoints("TP", "FSC")==true
               )
            {
                
                @ViewData["Message"] = "Zaktualizowano Materiały";
                return View("MyAccount");
            }
            else
            {
                @ViewData["Message"] = "Niepowodzenie - aktualizacja materiałów "+ rep.ErrorMsg;
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
