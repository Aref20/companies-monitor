
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

builder.Services.AddScoped<IDocumentsTypeService, DocumentTypeService>();
builder.Services.AddScoped<IDocumentsService, DocumentService>();
builder.Services.AddScoped<ICompaniesTypeService, CompanyTypeService>();
builder.Services.AddScoped<IPartnersService, PartnerService>();
builder.Services.AddScoped<ICompaniesService, CompanyService>();

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

app.UseAuthorization();
app.MapControllers();



app.Run();
