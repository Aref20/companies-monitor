using MSGCompaniesMonitor.Models;

namespace MSGCompaniesMonitor.RepositoryContracts
{
    public interface IDocumentsRepository
    {
        Task<Document> CreateAsync(Document document);
        Task<Document> EditAsync(Document document, int id);
        Task<Document> DeleteAsync(int id);
        Task<List<Document>> GetAllDocumentsAsync();
        Task<Document> GetDocumentByIDAsync(int id);
        Task<Pagination<Document>> PaginationAsync(string? search, int page, int pageSize);



    }
}
