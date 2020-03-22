using FSCReportSite.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public void AddPerformParam(PerformanceParameters model)
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
    }
}
    
