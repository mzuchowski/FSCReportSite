using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FSCReportSite.Data;
using FSCReportSite.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
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

        [HttpGet]
        public ViewResult PerformanceParametersForm()
        {
            Parameters paramList = new Parameters(_options, _sourceOptions);
            var result = paramList.ShowPerfParam();
            return View("PerformanceParametersForm",result);
        }

        [HttpGet]
        public ViewResult CertificateParametersForm()
        {
            Parameters paramList = new Parameters(_options,_sourceOptions);
            var result =paramList.ShowCertParam();
            return View("CertificateParametersForm", result);
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
                var paramSum = model.ParameterCw + model.ParameterFsc;
                Parameters AddParam = new Parameters(_options, _sourceOptions);

                if (paramSum == 1)
                {
                    if (AddParam.CheckCertName(model.CertificateName) == true)
                    {
                        AddParam.AddCertParam(model);
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

        [HttpGet]
        public ViewResult AddPerformanceParameter()
        {
            return View();
        }

        [HttpPost]
        public ViewResult AddPerformanceParameter(PerformanceParameters model)
        {
            if (ModelState.IsValid)
            {
                Parameters AddParam = new Parameters(_options, _sourceOptions);

                if (model.Tlperformance <= 1 && model.Tfperformance <= 1)
                {
                    if (AddParam.CheckPerfDate(Convert.ToInt32(model.YearOfDocument), Convert.ToInt32(model.MonthOfDocument)) == true) //ZMIENIĆ NA SPRAWDZANIE ROKU I MIESIACA
                    {
                        var addStatus =AddParam.AddPerfParam(model);
                        var result = AddParam.ShowPerfParam();

                        if (addStatus)
                        {
                            @ViewData["Message"] = "Współczynnik wydajności dla Rok: " + model.YearOfDocument +
                                                   " Miesiąc: " + model.MonthOfDocument + " został dodany pomyślnie!";
                            return View("PerformanceParametersForm", result);
                        }
                        else
                        {
                            string error = AddParam.errorMsg;
                            @ViewData["Message"] = "Wystąpił problem: " + error;
                            return View("PerformanceParametersForm", result);
                        }
                    }
                    else
                    {
                        @ViewData["Message"] = "Współczynnik dla podanego roku i miesiąca już istnieje!";
                        return View();
                    }
                }
                else
                {
                    @ViewData["Message"] = "Wartość współczynnika nie może przekraczać 1";
                    return View();
                }
            }
            else
            {
                return View();
            }
        }

        public ViewResult EditCertificateParameter(int idParam, string certNameParam, float valueFscParam, float valueCwParam)
        {
            CertificateParameters currentCertParam = new CertificateParameters
            {
                Id = idParam,
                CertificateName = certNameParam,
                ParameterFsc = valueFscParam,
                ParameterCw = valueCwParam
            };
            return View("EditCertificateParameter",currentCertParam);
        }

        public ViewResult EditPerformanceParameter(int yearParam, int monthParam, float valueTpParam, float valueTfParam)
        {
            PerformanceParameters currentPerfParam = new PerformanceParameters
            {
                YearOfDocument = yearParam,
                MonthOfDocument = monthParam,
                Tlperformance = valueTpParam,
                Tfperformance = valueTfParam
            };
            return View("EditPerformanceParameter", currentPerfParam);
        }

        [HttpPost]
        public ViewResult SaveChangeCertificateParameter(CertificateParameters model)
        {
            var paramSum =model.ParameterFsc + model.ParameterCw;

            if (paramSum == 1)
            {
                Parameters EditCertParam = new Parameters(_options, _sourceOptions);
                var editStatus =EditCertParam.EditCertParam(model);
                var result = EditCertParam.ShowCertParam();

                if (editStatus)
                {
                    @ViewData["Message"] = "Certyfikat " + model.CertificateName + " został zaktualizowany pomyślnie";
                    return View("CertificateParametersForm", result);
                }
                else
                {
                    string error = EditCertParam.errorMsg;
                    @ViewData["Message"] = "Wystąpił problem: " + error;
                    return View("CertificateParametersForm", result);
                }
            }
            else
            {
                @ViewData["Message"] = "Suma wartości współczynników FSC i CW musi być równa 1";
                return View("EditCertificateParameter",model);
            }
        }

        [HttpPost]
        public ViewResult SaveChangePerformanceParameter(PerformanceParameters model)
        {

            if (model.Tlperformance <= 1 && model.Tfperformance <=1)
            {
                Parameters EditPerfParam = new Parameters(_options, _sourceOptions);
                var editStatus = EditPerfParam.EditPerfParam(model);
                var result = EditPerfParam.ShowPerfParam();

                if (editStatus)
                {
                    @ViewData["Message"] = "Współczynnik dla: " + model.YearOfDocument + "." + model.MonthOfDocument + " został zaktualizowany pomyślnie";
                    return View("PerformanceParametersForm", result);
                }
                else
                {
                    string error = EditPerfParam.errorMsg;
                    @ViewData["Message"] = "Wystąpił problem: " + error;
                    return View("PerformanceParametersForm", result);
                }
            }
            else
            {
                @ViewData["Message"] = "Wartość współczynnika nie możę przekraczać 1";
                return View("EditPerformanceParameter", model);
            }
        }

        [HttpGet]
        public ViewResult DeleteCertificateParameter(int id, string certNameParam, float valueFscParam, float valueCwParam)
        {
            
            List<CertificateParameters> value = new List<CertificateParameters>()
            {
                new CertificateParameters()
                {
                    Id = id,
                    CertificateName = certNameParam,
                    ParameterFsc = valueFscParam,
                    ParameterCw = valueCwParam
                }
            };

            return View("DeleteCertificateParameter",value);
        }

        [HttpPost]
        public ViewResult DeleteCertificateParameter(int idParam, string nameParam)
        {
            Parameters CertParam = new Parameters(_options,_sourceOptions);
            var delParamStatus = CertParam.DeleteCertParam(idParam);
            var result = CertParam.ShowCertParam();

            if (delParamStatus)
            {
                @ViewData["Message"] = "Certyfikat " + nameParam + " został usunięty";
                return View("CertificateParametersForm", result);
            }
            else
            {
                string error = CertParam.errorMsg;
                @ViewData["Message"] = "Wystąpił problem: "+ error;
                return View("CertificateParametersForm", result);
            }
        }

        [HttpGet]
        public ViewResult DeletePerformanceParameter(int idParam, int yearParam, int monthParam, float valueTpParam, float valueTfParam)
        {
            List<PerformanceParameters> value = new List<PerformanceParameters>()
            {
                new PerformanceParameters()
                {
                    Id = idParam,
                    YearOfDocument = yearParam,
                    MonthOfDocument = monthParam,
                    Tlperformance = valueTpParam,
                    Tfperformance = valueTfParam
                }
            };

            return View("DeletePerformanceParameter", value);
        }

        [HttpPost]
        public ViewResult DeletePerformanceParameter(int idParam, int yearParam, int monthParam)
        {
            Parameters PerfParam = new Parameters(_options, _sourceOptions);
            var delParamStatus = PerfParam.DeletePerfParam(idParam);
            var result = PerfParam.ShowPerfParam();

            if (delParamStatus)
            {
                @ViewData["Message"] = "Certyfikat " + yearParam + "," + monthParam + " został usunięty";
                return View("PerformanceParametersForm", result);
            }
            else
            {
                string error = PerfParam.errorMsg;
                @ViewData["Message"] = "Wystąpił problem: " + error;
                return View("PerformanceParametersForm", result);
            }
        }
    }
}