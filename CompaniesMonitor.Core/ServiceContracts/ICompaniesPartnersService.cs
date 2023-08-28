using CompaniesMonitor.Core.Entities;
using Microsoft.AspNetCore.Http;

namespace CompaniesMonitor.Core.ServiceContracts
{
    public interface ICompaniesPartnersService
    {
        Task<CompanyPartner> CreateAsync(CompanyPartner companyPartner, IFormCollection formCollection);
        Task<CompanyPartner> EditAsync(CompanyPartner companyPartner, int id, IFormCollection formCollection);
        Task<CompanyPartner> DeleteAsync(int id);
        Task<List<CompanyPartner>> GetAllCompaniesPartnersAsync();
        Task<CompanyPartner> GetCompanyPartnerByIDAsync(int id);
        Task<Pagination<CompanyPartner>> PaginationAsync(string? search, int page, int pageSize);
    }
}
