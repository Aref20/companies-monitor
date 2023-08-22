using MSGCompaniesMonitor.ServiceContracts;
using MSGCompaniesMonitor.Models;
using MSGCompaniesMonitor.RepositoryContracts;

namespace MSGCompaniesMonitor.Services
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
            if (companyType == null)
            {
                throw new ArgumentNullException(nameof(companyType));
            }

            try
            {

                return await _companiesTypeService.CreateAsync(companyType);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<CompanyType> DeleteAsync(int id)
        {
            var companyType = await _companiesTypeService.GetCompanyTypeByIDAsync(id);
            if (companyType == null)
            {
                throw new ArgumentNullException(nameof(companyType));
            }

            try
            {
                return await _companiesTypeService.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }



        public async Task<CompanyType> EditAsync(CompanyType companyType, int id)
        {
            var companyTypeObj = await _companiesTypeService.GetCompanyTypeByIDAsync(id);
            if (companyTypeObj == null || id == 0)
            {
                throw new ArgumentNullException(nameof(companyTypeObj));
            }


            try
            {
                return await _companiesTypeService.EditAsync(companyType,id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        public async Task<List<CompanyType>> GetAllCompaniesTypeAsync()
        {
            return await _companiesTypeService.GetAllCompaniesTypeAsync();
        }


        public async Task<CompanyType> GetCompanyTypeByIDAsync(int id)
        {
            if(id == 0)
            {
                throw new ArgumentNullException(nameof(id));
            }
            return await _companiesTypeService.GetCompanyTypeByIDAsync(id);
        }

        public async Task<Pagination<CompanyType>> PaginationAsync(string? search, int page, int pageSize)
        {
            return await _companiesTypeService.PaginationAsync(search, page, pageSize);


        }
    }


   }


