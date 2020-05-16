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
                rep.ClearTables("TP","FSC") == true &
                rep.ImportData() == true &&
                rep.MaterialAndProductUpdate("TP") == true &&
                rep.GroupPurchasesAndSales("TP") == true &&
                rep.AddParameters("TP", "FSC") == true &&
                rep.CalculatePurchuasePoints("TP", "FSC") == true &&
                rep.AddRowsToReport("TP", "FSC") == true &&
                rep.AddDataToReport("TP", "FSC") == true &&
                rep.CalculateDifference("TP", "FSC") ==true &&
                rep.AddDifferenceFromPast("TP", "FSC") == true &&
                rep.CalculateReportPoints("TP", "FSC") == true
            )
            {
                var result = rep.ShowReportFscTp();
                @ViewData["Message"] = "Raport gotowy";

                return View("SolidBoardReportFSC",result);
            }
            else
            {
                @ViewData["Message"] = "Wystapił problem przy tworzeniu raportu: " + rep.errorMsg;
                return View("SolidBoardReportFSC");
            }
        }

        public ViewResult CreateReportFscTf()
        {
            Reports rep = new Reports(_options, _sourceOptions);
            if (
                rep.ClearTables("TF", "FSC") == true &&
                rep.ImportData() == true &&
                rep.MaterialAndProductUpdate("TF") == true &&
                rep.UpdateFlutingProductWeight() ==true &&
                rep.GroupPurchasesAndSales("TF") == true &&
                rep.AddParameters("TF", "FSC") == true &&
                rep.CalculatePurchuasePoints("TF", "FSC") == true &&
                rep.AddRowsToReport("TF", "FSC") == true &&
                rep.AddDataToReport("TF", "FSC") == true &&
                rep.CalculateDifference("TF", "FSC") == true &&
                rep.AddDifferenceFromPast("TF", "FSC") == true &&
                rep.CalculateReportPoints("TF", "FSC") == true
            )
            {
                var result = rep.ShowReportFscTf();
                @ViewData["Message"] = "Raport gotowy";

                return View("CorrugatedBoardReportFSC", result);
            }
            else
            {
                @ViewData["Message"] = "Wystapił problem przy tworzeniu raportu: " + rep.errorMsg;
                return View("CorrugatedBoardReportFSC");
            }
        }

        public ViewResult CreateReportCwTp()
        {
            Reports rep = new Reports(_options, _sourceOptions);
            if (
                rep.ClearTables("TP", "CW") == true &&
                rep.ImportData() == true &&
                rep.MaterialAndProductUpdate("TP") == true &&
                rep.GroupPurchasesAndSales("TP") == true &&
                rep.AddParameters("TP", "CW") == true &&
                rep.CalculatePurchuasePoints("TP", "CW") == true &&
                rep.AddRowsToReport("TP", "CW") == true &&
                rep.AddDataToReport("TP", "CW") == true &&
                rep.CalculateDifference("TP", "CW") == true &&
                rep.AddDifferenceFromPast("TP", "CW") == true &&
                rep.CalculateReportPoints("TP", "CW") == true
            )
            {
                var result = rep.ShowReportCwTp();
                @ViewData["Message"] = "Raport gotowy";

                return View("SolidBoardReportCW", result);
            }
            else
            {
                @ViewData["Message"] = "Wystapił problem przy tworzeniu raportu: " + rep.errorMsg;
                return View("SolidBoardReportCW");
            }
        }

        public ViewResult CreateReportCwTf()
        {
            Reports rep = new Reports(_options, _sourceOptions);
            if (
                rep.ClearTables("TF", "CW") == true &&
                rep.ImportData() == true &&
                rep.MaterialAndProductUpdate("TF") == true &&
                rep.UpdateFlutingProductWeight() == true &&
                rep.GroupPurchasesAndSales("TF") == true &&
                rep.AddParameters("TF", "CW") == true &&
                rep.CalculatePurchuasePoints("TF", "CW") == true &&
                rep.AddRowsToReport("TF", "CW") == true &&
                rep.AddDataToReport("TF", "CW") == true &&
                rep.CalculateDifference("TF", "CW") == true &&
                rep.AddDifferenceFromPast("TF", "CW") == true &&
                rep.CalculateReportPoints("TF", "CW") == true
            )
            {
                var result = rep.ShowReportCwTf();
                @ViewData["Message"] = "Raport gotowy";

                return View("CorrugatedBoardReportCW", result);
            }
            else
            {
                @ViewData["Message"] = "Wystapił problem przy tworzeniu raportu: " + rep.errorMsg;
                return View("CorrugatedBoardReportCW");
            }
        }

        public ViewResult ShowLastRepFscTp()
        {
            Reports rep = new Reports(_options, _sourceOptions);
            List<ReportFscTp> result = rep.ShowReportFscTp();

            return View("SolidBoardReportFSC",result);
        }

        public ViewResult ShowLastRepFscTf()
        {
            Reports rep = new Reports(_options, _sourceOptions);
            List<ReportFscTf> result = rep.ShowReportFscTf();

            return View("CorrugatedBoardReportFSC", result);
        }

        public ViewResult ShowLastRepCwTp()
        {
            Reports rep = new Reports(_options, _sourceOptions);
            List<ReportCwTp> result = rep.ShowReportCwTp();

            return View("SolidBoardReportCW", result);
        }

        public ViewResult ShowLastRepCwTf()
        {
            Reports rep = new Reports(_options, _sourceOptions);
            List<ReportCwTf> result = rep.ShowReportCwTf();

            return View("CorrugatedBoardReportCW", result);
        }
    }
}