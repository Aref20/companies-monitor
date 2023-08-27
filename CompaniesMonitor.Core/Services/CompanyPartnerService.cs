using MSGCompaniesMonitor.Models;
using MSGCompaniesMonitor.RepositoryContracts;
using MSGCompaniesMonitor.ServiceContracts;

namespace MSGCompaniesMonitor.Services
{
    public class CompanyPartnerService : ICompaniesPartnersService
    {
        private readonly ICompaniesPartnersRepository _companyPartnerRepository;
        public CompanyPartnerService(ICompaniesPartnersRepository companyPartnerRepository)
        {
            _companyPartnerRepository = companyPartnerRepository;
        }

        public async Task<CompanyPartner> CreateAsync(CompanyPartner companyPartner)
        {
            try
            {

                return await _companyPartnerRepository.CreateAsync(companyPartner);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<CompanyPartner> DeleteAsync(int id)
        {

            try
            {
                return await _companyPartnerRepository.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public async Task<CompanyPartner> EditAsync(CompanyPartner companyPartner, int id)
        {

            try
            {
                return await _companyPartnerRepository.EditAsync(companyPartner, id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public async Task<List<CompanyPartner>> GetAllCompaniesPartnersAsync()
        {
            try
            {
                return await _companyPartnerRepository.GetAllCompaniesPartnersAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        public async Task<CompanyPartner> GetCompanyPartnerByIDAsync(int id)
        {
            try
            {
                return await _companyPartnerRepository.GetCompanyPartnerByIDAsync(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public async Task<Pagination<CompanyPartner>> PaginationAsync(string? search, int page, int pageSize)
        {
            try
            {
                return await _companyPartnerRepository.PaginationAsync(search, page, pageSize);
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }
    }
}
