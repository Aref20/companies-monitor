using CompaniesMonitor.Core.Entities;
using CompaniesMonitor.Core.RepositoryContracts;
using CompaniesMonitor.Core.ServiceContracts;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CompaniesMonitor.Core.Services
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

            try
            {

                return await _partnersRepository.CreateAsync(partner);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Partner> DeleteAsync(int id)
        {

            try
            {
                return await _partnersRepository.DeleteAsync(id);
            }
            catch (Exception ex)
            {
               throw ex;
            }
        }



        public async Task<Partner> EditAsync(Partner partner,int id)
        {

            try
            {
                return await _partnersRepository.EditAsync(partner, id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public async Task<List<Partner>> GetAllPartnersAsync()
        {
            try
            {
                return await _partnersRepository.GetAllPartnersAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }


        public async Task<Partner> GetPartnerByIDAsync(int id)
        {
            try
            {
                return await _partnersRepository.GetPartnerByIDAsync(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }

        public async Task<List<SelectListItem>> GetAllPartnersItemsAsync()
        {
            try
            {
                return await _partnersRepository.GetAllPartnersItemsAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
           
        }

        public async Task<List<SelectListItem>> GetAllPartnersItemsAsync(int id)
        {
            try
            {
                return await _partnersRepository.GetAllPartnersItemsAsync(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Pagination<Partner>> PaginationAsync(string? search, int page, int pageSize)
        {
            try
            {
                return await _partnersRepository.PaginationAsync(search, page, pageSize);
            }
            catch (Exception ex)
            {
                throw ex;
            }
           

        }
    }


   }


