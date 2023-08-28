using CompaniesMonitor.Core.Entities;
using CompaniesMonitor.Core.RepositoryContracts;
using CompaniesMonitor.Core.ServiceContracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CompaniesMonitor.Core.Services
{
    public class CompanyService : ICompaniesService
    {
        private readonly ICompaniesRepository _companyRepository;

        public CompanyService(ICompaniesRepository companyRepository)
        {

            _companyRepository = companyRepository;

        }
        
        public async Task<Company> CreateAsync(Company company , IFormCollection formCollection)
        {
            try
            {
                
                return await _companyRepository.CreateAsync(company, formCollection);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Company> DeleteAsync(int id)
        {

            try
            {
                
                return await _companyRepository.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public async Task<Company> EditAsync(Company company, int id, IFormCollection formCollection)
        {

            try
            {
                var companyObj = await _companyRepository.EditAsync(company, id, formCollection);

                return companyObj;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public async Task<List<Company>> GetAllCompaniesAsync()
        {
            try 
            { 

                return await _companyRepository.GetAllCompaniesAsync();
             }
            catch (Exception ex)
            {
                throw ex;
            }
}


        public async Task<Company> GetCompanyByIDAsync(int id)
        {
            try
            {
                return await _companyRepository.GetCompanyByIDAsync(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public async Task<List<SelectListItem>> GetAllCompaniesItemsAsync(int id)
        {
            try
            {
                return await _companyRepository.GetAllCompaniesItemsAsync(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public async Task<List<SelectListItem>> GetAllCompaniesItemsAsync()
        {
            try
            {
                return await _companyRepository.GetAllCompaniesItemsAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public async Task<Pagination<Company>> PaginationAsync(string? search, int page, int pageSize)
        {
            try
            {
                return await _companyRepository.PaginationAsync(search, page, pageSize);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


    }


   }


