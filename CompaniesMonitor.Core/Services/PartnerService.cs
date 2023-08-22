using MSGCompaniesMonitor.ServiceContracts;
using MSGCompaniesMonitor.Models;
using MSGCompaniesMonitor.RepositoryContracts;

namespace MSGCompaniesMonitor.Services
{
    public class PartnerService : IPartnersService
    {
        private readonly IPartnersRepository _partnersRepository;
        public PartnerService(IPartnersRepository partnersRepository)
        {
            _partnersRepository = partnersRepository;
        }

        public async Task<Partner> CreateAsync(Partner partner)
        {
            if (partner == null)
            {
                throw new ArgumentNullException(nameof(partner));
            }

            try
            {

                return await _partnersRepository.CreateAsync(partner);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<Partner> DeleteAsync(int id)
        {
            var partner = await _partnersRepository.GetPartnerByIDAsync(id);
            if (partner == null)
            {
                throw new ArgumentNullException(nameof(partner));
            }

            try
            {
                return await _partnersRepository.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }



        public async Task<Partner> EditAsync(Partner partner,int id)
        {
            var partnerObj = await _partnersRepository.GetPartnerByIDAsync(id);
            if (partnerObj == null || id == 0)
            {
                throw new ArgumentNullException(nameof(partnerObj));
            }


            try
            {
                return await _partnersRepository.EditAsync(partner, id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        public async Task<List<Partner>> GetAllPartnersAsync()
        {
            return await _partnersRepository.GetAllPartnersAsync();
        }


        public async Task<Partner> GetPartnerByIDAsync(int ID)
        {
            return await _partnersRepository.GetPartnerByIDAsync(ID);
        }

        public async Task<Pagination<Partner>> PaginationAsync(string? search, int page, int pageSize)
        {
           return await _partnersRepository.PaginationAsync(search, page, pageSize);

        }
    }


   }


