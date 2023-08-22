using Microsoft.AspNetCore.Http;


using MSGCompaniesMonitor.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using MSGCompaniesMonitor.RepositoryContracts;
using MSGCompaniesMonitor.ServiceContracts;
namespace MSGCompaniesMonitor.Services
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
            if(company == null || formCollection == null)
            {
                return null;
            }
            
            try
            {
                
                return await _companyRepository.CreateAsync(company, formCollection);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<Company> DeleteAsync(int id)
        {
            if (id == 0)
            {
                return null;
            }

            try
            {
                
                return await _companyRepository.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }



        public async Task<Company> EditAsync(Company company, int id, IFormCollection formCollection)
        {

            if (await _companyRepository.GetCompanyByIDAsync(id) ==null)
            {
                return null;
            }

            try
            {
                var companyObj = await _companyRepository.EditAsync(company, id, formCollection);


                return companyObj;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<SelectListItem>> GetAllPartnerssAsItemsAsync()
        {
            return await _companyRepository.GetAllPartnerssAsItemsAsync();
        }

        public async Task<List<SelectListItem>> GetAllPartnerssAsItemsAsync(int? id)
        {
            return await _companyRepository.GetAllPartnerssAsItemsAsync(id);
        }

        public async Task<List<SelectListItem>> GetAllCompaniesTypeAsItemsAsync(int? id)
        {
            return await _companyRepository.GetAllCompaniesTypeAsItemsAsync(id);
        }

        public async Task<List<SelectListItem>> GetAllCompaniesTypeAsItemsAsync()
        {
            return await _companyRepository.GetAllCompaniesTypeAsItemsAsync();
        }


        public async Task<List<Company>> GetAllCompaniesAsync()
        {
            return await _companyRepository.GetAllCompaniesAsync();
        }


        public async Task<Company> GetCompanyByIDAsync(int id)
        {

            return await _companyRepository.GetCompanyByIDAsync(id);
        }

        public async Task<Pagination<Company>> PaginationAsync(string? search, int page, int pageSize)
        {
           return await _companyRepository.PaginationAsync(search, page, pageSize);

        }

        public async Task<List<SelectListItem>> GetAllDocumentsAsItemsAsync()
        {
            return await _companyRepository.GetAllDocumentsAsItemsAsync();
        }

        public async Task<List<SelectListItem>> GetAllDocumentsAsItemsAsync(int? id)
        {
            return await _companyRepository.GetAllDocumentsAsItemsAsync(id);
        }
    }


   }


