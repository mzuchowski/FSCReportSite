using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace FSCReportSite.Models
{
    public partial class FscReportContext : DbContext
    {
        public FscReportContext()
        {
        }

        public FscReportContext(DbContextOptions<FscReportContext> options)
            : base(options)
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

        // Unable to generate entity type for table 'dbo.CertificateParameters'. Please see the warning messages.
        // Unable to generate entity type for table 'dbo.PerformanceParameters'. Please see the warning messages.
        // Unable to generate entity type for table 'dbo.Sales'. Please see the warning messages.
        // Unable to generate entity type for table 'dbo.NOWATABELA'. Please see the warning messages.

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server = 192.168.56.25; Database = FSCREPORT; Persist Security Info=True; user ID=sa; password=zaq1@WSX");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AspNetRoleClaims>(entity =>
            {
                entity.HasIndex(e => e.RoleId);

                entity.Property(e => e.RoleId).IsRequired();

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetRoleClaims)
                    .HasForeignKey(d => d.RoleId);
            });

            modelBuilder.Entity<AspNetRoles>(entity =>
            {
                entity.HasIndex(e => e.NormalizedName)
                    .HasName("RoleNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedName] IS NOT NULL)");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name).HasMaxLength(256);

                entity.Property(e => e.NormalizedName).HasMaxLength(256);
            });

            modelBuilder.Entity<AspNetUserClaims>(entity =>
            {
                entity.HasIndex(e => e.UserId);

                entity.Property(e => e.UserId).IsRequired();

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserClaims)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserLogins>(entity =>
            {
                entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

                entity.HasIndex(e => e.UserId);

                entity.Property(e => e.LoginProvider).HasMaxLength(128);

                entity.Property(e => e.ProviderKey).HasMaxLength(128);

                entity.Property(e => e.UserId).IsRequired();

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserLogins)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserRoles>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.RoleId });

                entity.HasIndex(e => e.RoleId);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(d => d.RoleId);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUsers>(entity =>
            {
                entity.HasIndex(e => e.NormalizedEmail)
                    .HasName("EmailIndex");

                entity.HasIndex(e => e.NormalizedUserName)
                    .HasName("UserNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedUserName] IS NOT NULL)");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.NormalizedEmail).HasMaxLength(256);

                entity.Property(e => e.NormalizedUserName).HasMaxLength(256);

                entity.Property(e => e.UserName).HasMaxLength(256);
            });

            modelBuilder.Entity<AspNetUserTokens>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

                entity.Property(e => e.LoginProvider).HasMaxLength(128);

                entity.Property(e => e.Name).HasMaxLength(128);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserTokens)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<Purchases>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Batch)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Contractor)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.DateOfDocument).HasColumnType("date");

                entity.Property(e => e.Fsc)
                    .HasColumnName("FSC")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.NumberOfPurchaseDoc)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.OperationType)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ProductGroup)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ProductIndex)
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.ProductType)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Unit)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.WarehouseName)
                    .HasMaxLength(30)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ReportFscTf>(entity =>
            {
                entity.ToTable("ReportFSC_TF");

                entity.Property(e => e.Id).HasColumnName("ID");
            });

            modelBuilder.Entity<ReportFscTp>(entity =>
            {
                entity.ToTable("ReportFSC_TP");

                entity.Property(e => e.Id).HasColumnName("ID");
            });

            modelBuilder.Entity<TotalPurchasesTf>(entity =>
            {
                entity.ToTable("TotalPurchases_TF");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CertificateName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PurchasePointsCw).HasColumnName("PurchasePointsCW");

                entity.Property(e => e.PurchasePointsFsc).HasColumnName("PurchasePointsFSC");
            });

            modelBuilder.Entity<TotalPurchasesTp>(entity =>
            {
                entity.ToTable("TotalPurchases_TP");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CertificateName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CertificateParamCw).HasColumnName("CertificateParamCW");

                entity.Property(e => e.CertificateParamFsc).HasColumnName("CertificateParamFSC");

                entity.Property(e => e.PurchasePointsCw).HasColumnName("PurchasePointsCW");

                entity.Property(e => e.PurchasePointsFsc).HasColumnName("PurchasePointsFSC");
            });

            modelBuilder.Entity<TotalSalesTf>(entity =>
            {
                entity.ToTable("TotalSales_TF");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CertificatName)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TotalSalesTp>(entity =>
            {
                entity.ToTable("TotalSales_TP");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CertificatName)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });
        }
    }
}
