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
    public class SourceDbContext : DbContext
    {
        public SourceDbContext(DbContextOptions<SourceDbContext> sourceOptions)
            :base(sourceOptions)
        { 
        }
        public virtual DbSet<PurchasesDoc> PurchasesDoc { get; set; }
        public virtual DbSet<SalesDoc> SalesDoc { get; set; }
    }
}
