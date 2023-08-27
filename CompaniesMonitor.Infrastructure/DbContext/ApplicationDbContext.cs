using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MSGCompaniesMonitor.Models;
using System;

namespace MSGCompaniesMonitor.Data
{
    public class ApplicationDbContext :  IdentityDbContext<User>
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) {}



        public DbSet<Company> Companies { get; set; }

        public DbSet<Partner> Partners { get; set; }

        public DbSet<CompanyType> CompaniesType { get; set; }

        public DbSet<Document> Documents { get; set; }

        public DbSet<DocumentType> DocumentsType { get; set; }

        public DbSet<CompanyPartner> CompaniesPartner { get; set; }

        public DbSet<UploadedFile> UploadedFiles { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Document>()
            .HasMany(a => a.DocumentesType)
            .WithOne(b => b.Document)
            .HasForeignKey(b => b.DocumentId);

            modelBuilder.Entity<Company>()
            .HasOne(ct => ct.CompanyType)
            .WithMany(c => c.Companies)
            .HasForeignKey(ct => ct.CompanyTypeId);
                

            modelBuilder.Entity<DocumentType>()
            .HasMany(a => a.Files)
            .WithOne(b => b.DocumentType)
            .HasForeignKey(b => b.DocumentTypeId);

            modelBuilder.Entity<Document>()
             .HasIndex(d => d.Name)
             .IsUnique();

            modelBuilder.Entity<CompanyType>()
             .HasIndex(d => d.Name)
             .IsUnique();

            modelBuilder.Entity<Partner>()
             .HasIndex(d => d.EnglishName).IncludeProperties(d => d.ArabicName)
             .IsUnique();

            modelBuilder.Entity<Company>()
             .HasIndex(d => d.EnglishName).IncludeProperties(d => d.ArabicName)
             .IsUnique();


            base.OnModelCreating(modelBuilder);

        }
    }
}
