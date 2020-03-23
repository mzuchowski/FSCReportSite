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

        [HttpGet]
        public ViewResult AddCertificateParameter()
        {
            return View();
        }

        [HttpPost]
        public ViewResult AddCertificateParameter(CertificateParameters model)
        {
            if (ModelState.IsValid)
            {
                var certParamModel = model;
                var paramSum = model.ParameterCw + model.ParameterFsc;
                Parameters AddParam = new Parameters(_options, _sourceOptions);

                if (paramSum == 1)
                {
                    if (AddParam.CheckCertificateName(certParamModel.CertificateName) == true)
                    {
                        AddParam.AddCertParam(certParamModel);
                        var result = AddParam.ShowCertParam();

                        @ViewData["Message"] = "Certyfikat " + model.CertificateName + " został dodany pomyślnie!";
                        return View("CertificateParametersForm", result);
                    }
                    else
                    {
                        @ViewData["Message"] = "Certyfikat o podanej nazwie już istnieje";
                        return View();
                    }
                }
                else
                {
                    @ViewData["Message"] = "Suma wartości współczynników FSC i CW musi być równa 1";
                    return View();
                }
            }
            else
            {
                return View();
            }
        }

        public ViewResult EditCertificateParameter(string certNameParam, float valueFscParam, float valueCwParam)
        {
            CertificateParameters currentCertParam =new CertificateParameters();
            currentCertParam.CertificateName = certNameParam;
            currentCertParam.ParameterFsc = valueFscParam;
            currentCertParam.ParameterCw = valueCwParam;

            return View(currentCertParam);
        }

        public ViewResult DeleteCertificateParameter()
        {
            return View("CertificateParametersForm");
        }
    }
}