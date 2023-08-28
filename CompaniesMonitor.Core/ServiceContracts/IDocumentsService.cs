using CompaniesMonitor.Core.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CompaniesMonitor.Core.ServiceContracts
{
    public interface IDocumentsService
    {
        Task<Document> CreateAsync(Document document);
        Task<Document> EditAsync(Document document, int id);
        Task<Document> DeleteAsync(int id);
        Task<List<Document>> GetAllDocumentsAsync();
        Task<Document> GetDocumentByIDAsync(int id);
        Task<List<SelectListItem>> GetAllDocumentsItemsAsync(int id);
        Task<List<SelectListItem>> GetAllDocumentsItemsAsync();
        Task<Pagination<Document>> PaginationAsync(string? search, int page, int pageSize);



    }
}
