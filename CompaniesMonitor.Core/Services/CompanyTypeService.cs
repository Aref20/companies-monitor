using CompaniesMonitor.Core.Entities;
using CompaniesMonitor.Core.RepositoryContracts;
using CompaniesMonitor.Core.ServiceContracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CompaniesMonitor.Core.Services
{
    public class CompanyTypeService : ICompaniesTypeService
    {
        private readonly ICompaniesTypeRepository _companiesTypeService;


        public CompanyTypeService(ICompaniesTypeRepository companiesTypeService)
        {

            _companiesTypeService = companiesTypeService;

        }
        
        public async Task<CompanyType> CreateAsync(CompanyType companyType)
        {

            try
            {

              return await _companiesTypeService.CreateAsync(companyType);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<CompanyType> DeleteAsync(int id)
        {

            try
            {
                return await _companiesTypeService.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public async Task<CompanyType> EditAsync(CompanyType companyType, int id)
        {

            try
            {
                return await _companiesTypeService.EditAsync(companyType,id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public async Task<List<CompanyType>> GetAllCompaniesTypeAsync()
        {
            try { 
                return await _companiesTypeService.GetAllCompaniesTypeAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public async Task<List<SelectListItem>> GetAllCompaniesTypeItemsAsync(int id)
        {
            try
            {
                return await _companiesTypeService.GetAllCompaniesTypeItemsAsync(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<SelectListItem>> GetAllCompaniesTypeItemsAsync()
        {
            try

            {
                return await _companiesTypeService.GetAllCompaniesTypeItemsAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

            public async Task<CompanyType> GetCompanyTypeByIDAsync(int id)
        {
            try
            {
                return await _companiesTypeService.GetCompanyTypeByIDAsync(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Pagination<CompanyType>> PaginationAsync(string? search, int page, int pageSize)
        {
            try 
            {                
                return await _companiesTypeService.PaginationAsync(search, page, pageSize);
             }
            catch (Exception ex)
            {
                throw ex;
            }



        }
    }


   }


