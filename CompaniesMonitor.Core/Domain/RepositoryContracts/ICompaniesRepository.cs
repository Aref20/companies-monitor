
using Microsoft.AspNetCore.Http;
using MSGCompaniesMonitor.Models;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace MSGCompaniesMonitor.RepositoryContracts
{
    public interface ICompaniesRepository
    {

        Task<Company> CreateAsync(Company company, IFormCollection formCollection);
        Task<Company> EditAsync(Company company, int ID, IFormCollection formCollection);
        Task<Company> DeleteAsync(int id);
        Task<List<Company>> GetAllCompaniesAsync();
        Task<List<SelectListItem>> GetAllPartnerssAsItemsAsync();
        Task<List<SelectListItem>> GetAllPartnerssAsItemsAsync(int? id);
        Task<List<SelectListItem>> GetAllCompaniesTypeAsItemsAsync(int? id);
        Task<List<SelectListItem>> GetAllCompaniesTypeAsItemsAsync();
        Task<List<SelectListItem>> GetAllDocumentsAsItemsAsync();
        Task<List<SelectListItem>> GetAllDocumentsAsItemsAsync(int? id);
        Task<Company> GetCompanyByIDAsync(int id);
        Task<Pagination<Company>> PaginationAsync(string? search, int page, int pageSize);
    }
}
