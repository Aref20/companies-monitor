using MSGCompaniesMonitor.Models;

namespace MSGCompaniesMonitor.ServiceContracts
{
    public interface IPartnersService
    {

        Task<Partner> CreateAsync(Partner partner);
        Task<Partner> EditAsync(Partner partner, int id);
        Task<Partner> DeleteAsync(int id);
        Task<List<Partner>> GetAllPartnersAsync();
        Task<Partner> GetPartnerByIDAsync(int id);
        Task<Pagination<Partner>> PaginationAsync(string? search, int page, int pageSize);
    }
}
