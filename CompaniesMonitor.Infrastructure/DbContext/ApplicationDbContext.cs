using CompaniesMonitor.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace CompaniesMonitor.Infrastructure.Data
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
            this.SeedUsers(modelBuilder);
            this.SeedRoles(modelBuilder);
            this.SeedUserRoles(modelBuilder);




        }


        private void SeedUsers(ModelBuilder builder)
        {
            User user = new User()
            {
                Id = "b74ddd14-6340-4840-95c2-db12554843e1",
                Name = "Admin",
                LockoutEnabled = false,
                UserName = "admin@mail.com",
                NormalizedUserName = "ADMIN",
                Email = "admin@mail.com",
            };

            PasswordHasher<User> passwordHasher = new PasswordHasher<User>();

            user.PasswordHash = "AQAAAAIAAYagAAAAEBWlgwFExXEnUnP3scGEKf/7yB9+4AgPQ22jHtG/X1rcYbPw/LhU6okV4AKn/TqqwA==";//passwordHasher.HashPassword(user, "Aaaa@1111");

            builder.Entity<User>().HasData(user);
        }

        private void SeedRoles(ModelBuilder builder)
        {
            builder.Entity<IdentityRole>().HasData(
                new IdentityRole() { Id = "fab4fac1-c546-41de-aebc-a14da6895711", Name = "Admin", ConcurrencyStamp = "1", NormalizedName = "Admin" },
                new IdentityRole() { Id = "c7b013f0-5201-4317-abd8-c211f91b7330", Name = "User", ConcurrencyStamp = "2", NormalizedName = "Human Resource" }
                );
        }

        private void SeedUserRoles(ModelBuilder builder)
        {
            builder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>() { RoleId = "fab4fac1-c546-41de-aebc-a14da6895711", UserId = "b74ddd14-6340-4840-95c2-db12554843e1" }
                );
        }
    }
}
