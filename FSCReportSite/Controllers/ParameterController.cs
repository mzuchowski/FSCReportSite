using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FSCReportSite.Data;
using FSCReportSite.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;

namespace FSCReportSite.Controllers
{
    public class ParameterController : Controller
    {
        private readonly DbContextOptions<ApplicationDbContext> _options;
        private readonly DbContextOptions<SourceDbContext> _sourceOptions;

        public ParameterController(DbContextOptions<ApplicationDbContext> options, DbContextOptions<SourceDbContext> sourceOptions)
        {
            _options = options;
            _sourceOptions = sourceOptions;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ViewResult PerformanceParameterForm()
        {
            Parameters showParamList = new Parameters(_options, _sourceOptions);
            var result = showParamList.ShowCertParam();
            return View(result);
        }

        [HttpPost]
        public ActionResult PerformanceParameterForm(PerformanceParameters model)
        {
            var performParamModel = model;

            Parameters PerformParam = new Parameters(_options,_sourceOptions);
            PerformParam.AddPerformParam(performParamModel);

            @ViewData["Message"] = "Parametr dodany pomyślnie!";
            return View();
        }

        [HttpGet]
        public ViewResult CertificateParametersForm()
        {
            Parameters paramList = new Parameters(_options,_sourceOptions);
            var result =paramList.ShowCertParam();
            return View("CertificateParametersForm", result);
           // return View();
        }

        [HttpPost]
        public ActionResult CertificateParametersForm(PerformanceParameters model)
        {
            var performParamModel = model;

            Parameters AddParam = new Parameters(_options, _sourceOptions);
            AddParam.AddPerformParam(performParamModel);
            var result = AddParam.ShowCertParam();

            @ViewData["Message"] = "Parametr dodany pomyślnie!";
            return View("CertificateParametersForm",result);
        }

        public ViewResult AddCertificateParameters()
        {
            return View();
        }
    }
}