using Microsoft.EntityFrameworkCore;
using MSGCompaniesMonitor.Models;

namespace MSGCompaniesMonitor.Data
{
    public class ApplicationDbContext : DbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) {}



        public DbSet<Company> Companies { get; set; }

        public DbSet<Partner> Partners { get; set; }

        public DbSet<CompanyType> CompaniesType { get; set; }

        public DbSet<Document> Documents { get; set; }

        public DbSet<DocumentType> DocumentsType { get; set; }

        public DbSet<CompanyPartner> CompaniesPartner { get; set; }

        



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Document>()
            .HasMany(a => a.DocumentesType)
            .WithOne(b => b.Document)
            .HasForeignKey(b => b.DocumentId);

            /*  modelBuilder.Entity<Partner>()
                   .Property(e => e.Percentage)
                   .HasComputedColumnSql("[SharedJD]/[Company.CapitalJD]");*/

            // Configure many-to-many relationship between Companies and Partners

            /*       modelBuilder.Entity<CompanyPartner>()
                       .HasKey(cp => new { cp.CompanyId, cp.PartnerId });

                   modelBuilder.Entity<CompanyPartner>()
                       .HasOne(cp => cp.Company)
                       .WithMany(c => c.CompaniesPartner)
                       .HasForeignKey(cp => cp.CompanyId);

                   modelBuilder.Entity<CompanyPartner>()
                       .HasOne(cp => cp.Partner)
                       .WithMany(p => p.CompaniesPartner)
                       .HasForeignKey(cp => cp.PartnerId);
            */




        }
    }
}
