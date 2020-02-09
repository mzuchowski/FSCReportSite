using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FSCReportSite.Models;
using Microsoft.AspNetCore.Authorization;

namespace FSCReportSite.Controllers
{
    public class HomeController : Controller
    {
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
    }
}
