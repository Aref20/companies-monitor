using CompaniesMonitor.Core.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CompaniesMonitor.Core.RepositoryContracts
{
    public interface ICompaniesRepository
    {

        Task<Company> CreateAsync(Company company, IFormCollection formCollection);
        Task<Company> EditAsync(Company company, int ID, IFormCollection formCollection);
        Task<Company> DeleteAsync(int id);
        Task<List<Company>> GetAllCompaniesAsync();
        Task<Company> GetCompanyByIDAsync(int id);
        Task<List<SelectListItem>> GetAllCompaniesItemsAsync(int id);
        Task<List<SelectListItem>> GetAllCompaniesItemsAsync();
        Task<Pagination<Company>> PaginationAsync(string? search, int page, int pageSize);
    }
}
