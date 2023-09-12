using Microsoft.EntityFrameworkCore;
using Quartz.Impl;
using Quartz.Spi;
using Quartz;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using CompaniesMonitor.Infrastructure.Data;
using CompaniesMonitor.Core.RepositoryContracts;
using CompaniesMonitor.Core.ServiceContracts;
using CompaniesMonitor.Infrastructure.Repository;
using CompaniesMonitor.Core.Services;
using CompaniesMonitor.Shared.Jobs;
using CompaniesMonitor.Shared.Mail;
using CompaniesMonitor.Core.Entities;


namespace CompaniesMonitor.UI.StartupExtensions
{
    public static class ConfigureServicesExtension
    {
        public static IServiceCollection ConfigureServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Add services to the container.
            services.AddControllersWithViews();

            services.AddDbContext<ApplicationDbContext>(
                options => options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection")
                    , b => b.MigrationsAssembly("CompaniesMonitor.Infrastructure")
                    ), ServiceLifetime.Scoped);


            services.AddTransient<IDocumentsTypeRepository, DocumentTypeRepository>();
            services.AddTransient<IDocumentsRepository, DocumentRepository>();
            services.AddTransient<ICompaniesTypeRepository, CompanyTypeRepository>();
            services.AddTransient<IPartnersRepository, PartnerRepository>();
            services.AddTransient<ICompaniesRepository, CompanyRepository>();
            services.AddTransient<IUploadedFilesRepository, UploadedFileRepository>();
            services.AddTransient<ICompaniesPartnersRepository, CompanyPartnerRepository>();

            services.AddTransient<IDocumentsTypeService, DocumentTypeService>();
            services.AddTransient<IDocumentsService, DocumentService>();
            services.AddTransient<ICompaniesTypeService, CompanyTypeService>();
            services.AddTransient<IPartnersService, PartnerService>();
            services.AddTransient<ICompaniesService, CompanyService>();
            services.AddTransient<IUploadedFilesService, UploadedFileService>();
            services.AddTransient<ICompaniesPartnersService, CompanyPartnerService>();

            services.AddTransient<IEmailSender, EmailSender>();
            //Quartz Jobs 
            services.AddSingleton<IJobFactory, JobFactory>();
            services.AddSingleton<ISchedulerFactory, StdSchedulerFactory>();
            services.AddSingleton<SendEmailJob>();
            services.AddHostedService<QuartzHostedService>();

            var emailConfig = configuration
                    .GetSection("EmailConfiguration")
                    .Get<EmailConfiguration>();

            services.AddSingleton(emailConfig);

            

            services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.ConfigureApplicationCookie(options => {
                options.LoginPath = "/Account/Login";
            });


            services.AddAuthorization(options =>
            {
                options.FallbackPolicy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build(); //enforces authoriation policy (user must be authenticated) for all the action methods

                options.AddPolicy("Auth", policy =>
                {
                    policy.RequireAssertion(context =>
                    {
                        var user = context.User.IsInRole("Admin");
                        return user;
                    });
                });
            });



            services.AddHttpLogging(options =>
            {
                options.LoggingFields = Microsoft.AspNetCore.HttpLogging.HttpLoggingFields.RequestProperties | Microsoft.AspNetCore.HttpLogging.HttpLoggingFields.ResponsePropertiesAndHeaders;
            });

            return services;
        }
    }
}
