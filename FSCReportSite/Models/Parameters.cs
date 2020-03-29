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

        public void AddPerfParam(PerformanceParameters model)
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
            }
        }

        public void AddCertParam(CertificateParameters model)
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
            }
        }

        public void EditCertParam(CertificateParameters model)
        {
            using (var context = new ApplicationDbContext(_options))
            {
                var certParamList = context.CertificateParameters.Where(p => p.CertificateName == model.CertificateName).ToList();
                certParamList.ForEach(s => s.ParameterCw = model.ParameterCw);
                certParamList.ForEach(s => s.ParameterFsc = model.ParameterFsc);

                context.SaveChanges();
            }
        }

        public bool CheckCertificateName(string certNameParam)
        {
            string certName = certNameParam;

            using (var context = new ApplicationDbContext(_options))
            {
                var result = context.CertificateParameters.Where(s => s.CertificateName == certName).FirstOrDefault();
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

       
    }
}
    
