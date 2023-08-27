using MSGCompaniesMonitor.Models;


namespace MSGCompaniesMonitor.RepositoryContracts
{
    public interface ICompaniesPartnersRepository
    {
        Task<CompanyPartner> CreateAsync(CompanyPartner companyPartner);
        Task<CompanyPartner> EditAsync(CompanyPartner companyPartner, int id);
        Task<CompanyPartner> DeleteAsync(int id);
        Task<List<CompanyPartner>> GetAllCompaniesPartnersAsync();
        Task<CompanyPartner> GetCompanyPartnerByIDAsync(int id);
        Task<Pagination<CompanyPartner>> PaginationAsync(string? search, int page, int pageSize);
    }
}
