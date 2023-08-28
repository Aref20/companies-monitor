using CompaniesMonitor.Core.Entities;
using CompaniesMonitor.Core.RepositoryContracts;
using CompaniesMonitor.Infrastructure.Data;
using CompaniesMonitor.Infrastructure.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace CompaniesMonitor.Infrastructure.Repository
{
    public class CompanyRepository : ICompaniesRepository
    {
        private readonly ApplicationDbContext _context;

        private readonly DbSet<Company> _DbSet;



        public CompanyRepository(ApplicationDbContext applicationDbContext)
        {
            _context = applicationDbContext;
            _DbSet = _context.Companies;
        }

        public async Task<Company> CreateAsync(Company company, IFormCollection formCollection)
        {
            await company.AssignCompanyTypeAsync(_context, formCollection);

            _DbSet.Add(company);
            await _context.SaveChangesAsync();
            return company;
        }

        public async Task<Company> DeleteAsync(int id)
        {
            var company = await _DbSet.FirstOrDefaultAsync(temp => temp.CompanyId == id);
            _DbSet.Remove(company);
            await _context.SaveChangesAsync();
            return company;

         }

        public async Task<Company> EditAsync(Company company, int id, IFormCollection formCollection)
        {
          var companyobj = await _DbSet
            .Include(cp => cp.CompaniesPartner)
            .Include(ct => ct.CompanyType)
            .FirstOrDefaultAsync(c => c.CompanyId == id);

            await company.EditCompanyTypeAsync(_context, companyobj, id, formCollection);

            companyobj.EnglishName = company.EnglishName;
            companyobj.ArabicName = company.ArabicName;
            companyobj.CloseDate = company.CloseDate;
            companyobj.CreatedDate = company.CreatedDate;
            companyobj.ArabicNotes = company.ArabicNotes;
            companyobj.EnglishNotes = company.EnglishNotes;
            companyobj.Number = company.Number;
            companyobj.CapitalJD = company.CapitalJD;

            await _context.SaveChangesAsync();
            return company;
        }

        public async Task<List<Company>> GetAllCompaniesAsync()
        {
            return await _DbSet.Include("CompanyType").ToListAsync();
        }


        public async Task<Company> GetCompanyByIDAsync(int id)
        {
            return await _DbSet.FirstOrDefaultAsync(temp => temp.CompanyId == id);
        }

        public async Task<List<SelectListItem>> GetAllCompaniesItemsAsync(int id)
        {
            return await _context.Companies.Select(temp => new SelectListItem { Value = temp.CompanyId.ToString(), Text = temp.EnglishName, Selected = temp.CompanyId == id }).ToListAsync();
        }

        public async Task<List<SelectListItem>> GetAllCompaniesItemsAsync()
        {
            return await _context.Companies.Select(temp => new SelectListItem { Value = temp.CompanyId.ToString(), Text = temp.EnglishName }).ToListAsync();
        }

        public async Task<Pagination<Company>> PaginationAsync(string? search, int page, int pageSize)
        {
            var totalRecords = await _DbSet.CountAsync();
            var data = await _DbSet.Include("CompanyType")
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();


            var paginationModel = new Pagination<Company>
            {
                Data = data,
                TotalRecords = totalRecords,
                PageSize = pageSize,
                CurrentPage = page
            };


            if (!string.IsNullOrWhiteSpace(search))
            {

                var filterdpaginationModel = new Pagination<Company>
                {
                    Data = await _DbSet.Include("CompanyType").Where(temp => temp.EnglishName.Contains(search)).Skip((page - 1) * pageSize).Take(pageSize).ToListAsync(),
                    TotalRecords = await _DbSet.Where(temp => temp.EnglishName.Contains(search)).CountAsync(),
                    PageSize = pageSize,
                    CurrentPage = page

                };

                return filterdpaginationModel;
            }

            return paginationModel;
        }



    }
}


