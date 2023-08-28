using CompaniesMonitor.Core.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CompaniesMonitor.Core.RepositoryContracts
{
    public interface IPartnersRepository
    {

        Task<Partner> CreateAsync(Partner partner);
        Task<Partner> EditAsync(Partner partner, int id);
        Task<Partner> DeleteAsync(int id);
        Task<List<Partner>> GetAllPartnersAsync();
        Task<Partner> GetPartnerByIDAsync(int id);
        Task<List<SelectListItem>> GetAllPartnersItemsAsync(int id);
        Task<List<SelectListItem>> GetAllPartnersItemsAsync();
        Task<Pagination<Partner>> PaginationAsync(string? search, int page, int pageSize);
    }
}
