using CompaniesMonitor.Core.Entities;
using Microsoft.AspNetCore.Http;


namespace CompaniesMonitor.Core.ServiceContracts
{
    public interface IDocumentsTypeService
    {
        Task<DocumentType> CreateAsync(DocumentType documentType, IFormCollection formCollection);
        Task<DocumentType> EditAsync(DocumentType documentType, int id, IFormCollection formCollection);
        Task<DocumentType> DeleteAsync(int id);
        Task<List<DocumentType>> GetAllDocumentsTypeAsync();
        Task<DocumentType> GetDocumentTypeByIDAsync(int id);
        Task<Pagination<DocumentType>> PaginationAsync(string? search, int page, int pageSize);
    }
}
