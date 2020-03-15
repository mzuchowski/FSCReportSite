using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FSCReportSite.Data;
using FSCReportSite.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FSCReportSite.Controllers
{
    public class ReportController : Controller
    {
        private readonly DbContextOptions<ApplicationDbContext> _options;
        private readonly DbContextOptions<SourceDbContext> _sourceOptions;

        public ReportController(DbContextOptions<ApplicationDbContext> options, DbContextOptions<SourceDbContext> sourceOptions)
        {
            _options = options;
            _sourceOptions = sourceOptions;
        }

        public ViewResult SolidBoardReportCW()
        {
            return View();
        }

        public ViewResult SolidBoardReportFSC()
        {
            return View();
        }
        public ViewResult CorrugatedBoardReportCW()
        {
            return View();
        }

        public ViewResult CorrugatedBoardReportFsc()
        {
            return View();
        }

        public ViewResult CreateReportFscTp()
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
                rep.CalculatePurchuasePoints("TP", "FSC") == true
            )
            {

                @ViewData["Message"] = "Zaktualizowano Materiały";

                return View("MyAccount");
            }
            else
            {
                @ViewData["Message"] = "Niepowodzenie - aktualizacja materiałów " + rep.ErrorMsg;
                return View("MyAccount");
            }
        }

        public ViewResult ShowLastRepFscTp()
        {
            Reports rep = new Reports(_options, _sourceOptions);

            List<ReportFscTp> result = rep.ShowReportFscTp();
            

            return View("SolidBoardReportFSC",result);
        }
    }
}