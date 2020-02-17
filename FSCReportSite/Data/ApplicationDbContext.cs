using System;
using System.Collections.Generic;
using System.Text;
using FSCReportSite.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FSCReportSite.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            :base(options)
        { 
        }
        public virtual DbSet<AspNetRoleClaims> AspNetRoleClaims { get; set; }
        public virtual DbSet<AspNetRoles> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaims> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogins> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUserRoles> AspNetUserRoles { get; set; }
        public virtual DbSet<AspNetUsers> AspNetUsers { get; set; }
        public virtual DbSet<AspNetUserTokens> AspNetUserTokens { get; set; }
        public virtual DbSet<Purchases> Purchases { get; set; }
        public virtual DbSet<ReportFscTf> ReportFscTf { get; set; }
        public virtual DbSet<ReportFscTp> ReportFscTp { get; set; }
        public virtual DbSet<TotalPurchasesTf> TotalPurchasesTf { get; set; }
        public virtual DbSet<TotalPurchasesTp> TotalPurchasesTp { get; set; }
        public virtual DbSet<TotalSalesTf> TotalSalesTf { get; set; }
        public virtual DbSet<TotalSalesTp> TotalSalesTp { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AspNetUserLogins>()
                .HasKey(c => new { c.LoginProvider, c.ProviderKey });

            modelBuilder.Entity<AspNetUserRoles>()
                .HasKey(c => new { c.UserId, c.RoleId });

            modelBuilder.Entity<AspNetUserTokens>()
                .HasKey(c => new { c.UserId, c.LoginProvider, c.Name });
        }

    }
}
