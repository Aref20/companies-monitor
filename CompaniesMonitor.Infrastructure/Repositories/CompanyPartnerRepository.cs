using Microsoft.EntityFrameworkCore;
using MSGCompaniesMonitor.Data;
using MSGCompaniesMonitor.Models;
using MSGCompaniesMonitor.RepositoryContracts;


namespace MSGCompaniesMonitor.Repository
{
    public class CompanyPartnerRepository : ICompaniesPartnersRepository
    {
        private readonly ApplicationDbContext _context;

        private readonly DbSet<CompanyPartner> _DbSet;
        public CompanyPartnerRepository(ApplicationDbContext applicationDbContext)
        {
            _context = applicationDbContext;
            _DbSet = _context.CompaniesPartner;
        }

        public async Task<CompanyPartner> CreateAsync(CompanyPartner companyPartner)
        {
            _DbSet.Add(companyPartner);
            await _context.SaveChangesAsync();
            return companyPartner;
        }

        public async Task<CompanyPartner> DeleteAsync(int id)
        {
            var companyPartner = await _DbSet.FindAsync(id);
            _DbSet.Remove(companyPartner);
            await _context.SaveChangesAsync();
            return companyPartner;
        }



        public async Task<CompanyPartner> EditAsync(CompanyPartner companyPartner, int id)
        {
            var companyPartnerObj = await _DbSet.FindAsync(id);

            companyPartnerObj.Partner = companyPartner.Partner;
            companyPartnerObj.Company = companyPartner.Company;
            companyPartnerObj.SharedJD = companyPartner.SharedJD;
            companyPartnerObj.Percentage = companyPartner.Percentage;


            await _context.SaveChangesAsync();
            return companyPartnerObj;
        }


        public async Task<List<CompanyPartner>> GetAllCompaniesPartnersAsync()
        {
            return await _DbSet.ToListAsync();
        }


        public async Task<CompanyPartner> GetCompanyPartnerByIDAsync(int id)
        {
            return await _DbSet.FindAsync(id);
        }

        public async Task<Pagination<CompanyPartner>> PaginationAsync(string? search, int page, int pageSize)
        {
            var totalRecords = await _DbSet.CountAsync();
            var data = await _DbSet
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Include(temp => temp.Company)
                .Include(temp => temp.Partner)
                .ToListAsync();


            var paginationModel = new Pagination<CompanyPartner>
            {
                Data = data,
                TotalRecords = totalRecords,
                PageSize = pageSize,
                CurrentPage = page
            };


            if (!string.IsNullOrWhiteSpace(search))
            {

                var filterdpaginationModel = new Pagination<CompanyPartner>
                {
                    Data = await _DbSet.Where(temp => temp.Company.EnglishName.Contains(search)).Skip((page - 1) * pageSize).Take(pageSize).ToListAsync(),
                    TotalRecords = await _DbSet.Where(temp => temp.Company.EnglishName.Contains(search)).CountAsync(),
                    PageSize = pageSize,
                    CurrentPage = page

                };

                return filterdpaginationModel;
            }

            return paginationModel;


        }
    }
}
