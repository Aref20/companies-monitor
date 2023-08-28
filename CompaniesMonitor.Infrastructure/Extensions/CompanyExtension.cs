using CompaniesMonitor.Core.Entities;
using CompaniesMonitor.Infrastructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace CompaniesMonitor.Infrastructure.Extensions
{
    public static class CompanyExtension
    {




        public static async Task<Company> AssignCompanyTypeAsync(this Company company, ApplicationDbContext context,  IFormCollection formCollection)
        {
            int CompanyTypeID = int.Parse(formCollection["CompanyType"]);

            company.CompanyType = await context.CompaniesType.FirstOrDefaultAsync(obj => obj.Id == CompanyTypeID);

            return company;
        }



        public static async Task<Company> EditCompanyTypeAsync(this Company company, ApplicationDbContext context,Company companyobj, int ID, IFormCollection formCollection)
        {


            companyobj.CompanyType = await context.CompaniesType.FirstOrDefaultAsync(obj => obj.Id == int.Parse(formCollection["CompanyType"]));

            return companyobj;
        }

    }
}
