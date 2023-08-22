﻿
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using MSGCompaniesMonitor.Models;

namespace MSGCompaniesMonitor.ServiceContracts
{
    public interface ICompaniesService
    {

        Task<Company> CreateAsync(Company company, IFormCollection formCollection);
        Task<Company> EditAsync(Company company, int id, IFormCollection formCollection);
        Task<Company> DeleteAsync(int id);
        Task<List<Company>> GetAllCompaniesAsync();
        Task<List<SelectListItem>> GetAllPartnerssAsItemsAsync();
        Task<List<SelectListItem>> GetAllPartnerssAsItemsAsync(int? id);
        Task<List<SelectListItem>> GetAllCompaniesTypeAsItemsAsync(int? id);
        Task<List<SelectListItem>> GetAllCompaniesTypeAsItemsAsync();
        Task<List<SelectListItem>> GetAllDocumentsAsItemsAsync();
        Task<List<SelectListItem>> GetAllDocumentsAsItemsAsync(int? id);
        Task<Company> GetCompanyByIDAsync(int id);
        Task<Pagination<Company>> PaginationAsync(string? search, int page, int pageSize);
    }
}
