
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using MSGCompaniesMonitor.Models;

namespace MSGCompaniesMonitor.RepositoryContracts
{
    public interface IDocumentsTypeRepository
    {
        Task<DocumentType> CreateAsync(DocumentType documentType, IFormCollection formCollection);
        Task<DocumentType> EditAsync(DocumentType documentType, int id, IFormCollection formCollection);
        Task<DocumentType> DeleteAsync(int id);
        Task<List<DocumentType>> GetAllDocumentsTypeAsync();
        Task<List<SelectListItem>> GetAllDocumentsAsync(int id);
        Task<List<SelectListItem>> GetAllDocumentsAsync();
        Task<List<SelectListItem>> GetAllCompaniesAsync(int id);
        Task<List<SelectListItem>> GetAllCompaniesAsync();
        Task<DocumentType> GetDocumentTypeByIDAsync(int id);
        Task<Pagination<DocumentType>> PaginationAsync(string? search, int page, int pageSize);


    }
}
