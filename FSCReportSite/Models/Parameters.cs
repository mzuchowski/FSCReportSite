using FSCReportSite.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using Microsoft.AspNetCore.Mvc;

namespace FSCReportSite.Models
{
    public class Parameters
    {
        public string errorMsg = "";
        private readonly DbContextOptions<ApplicationDbContext> _options;
        private readonly DbContextOptions<SourceDbContext> _sourceOptions;

        public Parameters(DbContextOptions<ApplicationDbContext> options, DbContextOptions<SourceDbContext> sourceOptions)
        {
            _options = options;
            _sourceOptions = sourceOptions;
        }

        public List<PerformanceParameters> ShowPerformParam()
        {
            using (var context = new ApplicationDbContext(_options))
            {
                var resultPerformParam = context.PerformanceParameters.ToList();
                return resultPerformParam;
            }
        }

        public List<CertificateParameters> ShowCertParam()
        {
            using (var context = new ApplicationDbContext(_options))
            {
                var resultCertParam = context.CertificateParameters.ToList();
                return resultCertParam;
            }
        }

        public List<PerformanceParameters> ShowPerfParam()
        {
            using (var context = new ApplicationDbContext(_options))
            {
                var resultPerfParam = context.PerformanceParameters.ToList();
                return resultPerfParam;
            }
        }

        public bool AddPerfParam(PerformanceParameters model)
        {
            try
            {
                using (var context = new ApplicationDbContext(_options))
                {
                    context.PerformanceParameters.Add(new PerformanceParameters
                    {
                        YearOfDocument = model.YearOfDocument,
                        MonthOfDocument = model.MonthOfDocument,
                        Tlperformance = model.Tlperformance,
                        Tfperformance = model.Tfperformance
                    });
                    context.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                errorMsg = ex.Message;
                return false;
            }
        }

        public bool AddCertParam(CertificateParameters model)
        {
            try
            {
                using (var context = new ApplicationDbContext(_options))
                {
                    context.CertificateParameters.Add(new CertificateParameters()
                    {
                        CertificateName = model.CertificateName,
                        ParameterFsc = model.ParameterFsc,
                        ParameterCw = model.ParameterCw
                    });
                    context.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                errorMsg = ex.Message;
                return false;
            }
        }

        public bool EditCertParam(CertificateParameters model)
        {
            try
            {
                using (var context = new ApplicationDbContext(_options))
                {
                    var certParamList = context.CertificateParameters
                        .Where(p => p.Id == model.Id).ToList();
                    certParamList.ForEach(s => s.ParameterCw = model.ParameterCw);
                    certParamList.ForEach(s => s.ParameterFsc = model.ParameterFsc);

                    context.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                errorMsg = ex.Message;
                return false;
            }
        }

        public bool EditPerfParam(PerformanceParameters model)
        {
            try
            {
                using (var context = new ApplicationDbContext(_options))
                {
                    var perfParamList = context.PerformanceParameters
                        .Where(p => p.YearOfDocument == model.YearOfDocument &&
                                    p.MonthOfDocument == model.MonthOfDocument)
                        .ToList();
                    perfParamList.ForEach(s => s.Tlperformance = model.Tlperformance);
                    perfParamList.ForEach(s => s.Tfperformance = model.Tfperformance);

                    context.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                errorMsg = ex.Message;
                return false;
            }
        }

        public bool CheckCertName(string certNameParam)
        {
            string certName = certNameParam;

            try
            {
                using (var context = new ApplicationDbContext(_options))
                {
                    var result = context.CertificateParameters.Where(s => s.CertificateName == certName)
                        .FirstOrDefault();
                    if (result == null)
                    {
                        return true;
                    }
                    else
                    { 
                        return false;
                    }
                }
            }
            catch(Exception ex)
            {
                errorMsg = ex.Message;
                return false;
            }
        }

        public bool CheckPerfDate(int yearParam, int monthParam)
        {
            int certYear = yearParam;
            int certMonth = monthParam;

            try
            {
                using (var context = new ApplicationDbContext(_options))
                {
                    var result = context.PerformanceParameters.Where(s => s.YearOfDocument == certYear &&
                                                                          s.MonthOfDocument == certMonth)
                        .FirstOrDefault();

                    if (result == null)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                errorMsg = ex.Message;
                return false;
            }
        }

        public bool DeleteCertParam(int idParam)
        {
            int recordId = idParam;

            try
            {
                using (var context = new ApplicationDbContext(_options))
                {
                    var certToDel = context.CertificateParameters.Find(recordId);
                    context.CertificateParameters.Remove(certToDel);
                    context.SaveChanges();
                    return true;
                }
            }
            catch(Exception ex)
            {
                errorMsg = ex.Message;
                return false;
            }
        }

        public bool DeletePerfParam(int idParam)
        {
            int recordId = idParam;

            try
            {
                using (var context = new ApplicationDbContext(_options))
                {
                    var perfToDel = context.PerformanceParameters.Find(recordId);
                    context.PerformanceParameters.Remove(perfToDel);
                    context.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                errorMsg = ex.Message;
                return false;
            }
        }
    }
}
    
