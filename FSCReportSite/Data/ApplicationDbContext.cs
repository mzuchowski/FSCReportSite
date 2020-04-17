using System;
using System.Collections.Generic;
using System.Text;
using FSCReportSite.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FSCReportSite.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            :base(options)
        { 
        }
        public virtual DbSet<Purchases> Purchases { get; set; }
        public virtual DbSet<ReportFscTf> ReportFscTf { get; set; }
        public virtual DbSet<ReportFscTp> ReportFscTp { get; set; }
        public virtual DbSet<ReportCwTf> ReportCwTf { get; set; }
        public virtual DbSet<ReportCwTp> ReportCwTp { get; set; }
        public virtual DbSet<TotalPurchasesTf> TotalPurchasesTf { get; set; }
        public virtual DbSet<TotalPurchasesTp> TotalPurchasesTp { get; set; }
        public virtual DbSet<TotalSalesTf> TotalSalesTf { get; set; }
        public virtual DbSet<TotalSalesTp> TotalSalesTp { get; set; }
        public virtual DbSet<CertificateParameters> CertificateParameters { get; set; }
        public virtual DbSet<PerformanceParameters> PerformanceParameters { get; set; }
        public virtual DbSet<Sales> Sales { get; set; }

    }
}
