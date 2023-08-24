
using Microsoft.EntityFrameworkCore;
using MSGCompaniesMonitor.Data;
using MSGCompaniesMonitor.RepositoryContracts;
using MSGCompaniesMonitor.Repository;
using MSGCompaniesMonitor.ServiceContracts;
using MSGCompaniesMonitor.Services;
using MSGCompaniesMonitor.Mail;
using MSGCompaniesMonitor.Jobs;
using Quartz.Impl;
using Quartz.Spi;
using Quartz;
using MSGCompaniesMonitor.Service;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using MSGCompaniesMonitor.Models;
using Microsoft.AspNetCore.Authorization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<ApplicationDbContext>(
    options => options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")
        , b => b.MigrationsAssembly("CompaniesMonitor.Infrastructure") 
        ),ServiceLifetime.Scoped);


builder.Services.AddScoped<IDocumentsTypeRepository, DocumentTypeRepository>();
builder.Services.AddScoped<IDocumentsRepository, DocumentRepository>();
builder.Services.AddScoped<ICompaniesTypeRepository, CompanyTypeRepository>();
builder.Services.AddScoped<IPartnersRepository, PartnerRepository>();
builder.Services.AddScoped<ICompaniesRepository, CompanyRepository>();
builder.Services.AddScoped<IUploadedFilesRepository, UploadedFileRepository>();

builder.Services.AddScoped<IDocumentsTypeService, DocumentTypeService>();
builder.Services.AddScoped<IDocumentsService, DocumentService>();
builder.Services.AddScoped<ICompaniesTypeService, CompanyTypeService>();
builder.Services.AddScoped<IPartnersService, PartnerService>();
builder.Services.AddScoped<ICompaniesService, CompanyService>();
builder.Services.AddScoped<IUploadedFilesService, UploadedFileService>();

//Quartz Jobs 
builder.Services.AddSingleton<IJobFactory, JobFactory>();
builder.Services.AddSingleton<ISchedulerFactory, StdSchedulerFactory>();
builder.Services.AddScoped<SendEmailJob>();
builder.Services.AddHostedService<QuartzHostedService>();

var emailConfig = builder.Configuration
        .GetSection("EmailConfiguration")
        .Get<EmailConfiguration>();
builder.Services.AddSingleton(emailConfig);

builder.Services.AddScoped<IEmailSender, EmailSender>();

builder.Services.AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();


    builder.Services.AddAuthorization(options =>
    {
        options.FallbackPolicy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build(); //enforces authoriation policy (user must be authenticated) for all the action methods
    });

    builder.Services.ConfigureApplicationCookie(options => {
        options.LoginPath = "/Account/Login";
    });

    builder.Services.AddHttpLogging(options =>
    {
        options.LoggingFields = Microsoft.AspNetCore.HttpLogging.HttpLoggingFields.RequestProperties | Microsoft.AspNetCore.HttpLogging.HttpLoggingFields.ResponsePropertiesAndHeaders;
    });

/*builder.Services.AddControllersWithViews(options =>
{
    options.ModelBinderProviders.Insert(0, new CompanyBinderProvider());
});
*/
var app = builder.Build();



// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication(); 
app.UseAuthorization();
app.MapControllers();



app.Run();
