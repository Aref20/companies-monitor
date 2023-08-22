using MSGCompaniesMonitor.Models;

namespace MSGCompaniesMonitor.ServiceContracts
{
    public interface ICompaniesTypeService
    {

        Task<CompanyType> CreateAsync(CompanyType companyType);
        Task<CompanyType> EditAsync(CompanyType companyType, int id);
        Task<CompanyType> DeleteAsync(int id);
        Task<List<CompanyType>> GetAllCompaniesTypeAsync();
        Task<CompanyType> GetCompanyTypeByIDAsync(int id);
        Task<Pagination<CompanyType>> PaginationAsync(string? search, int page, int pageSize);
    }
}
