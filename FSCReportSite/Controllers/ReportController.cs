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

        public ViewResult CorrugatedBoardReportFSC()
        {
            return View();
        }
    }
}