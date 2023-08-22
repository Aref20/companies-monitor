using MSGCompaniesMonitor.ServiceContracts;
using MSGCompaniesMonitor.Models;
using MSGCompaniesMonitor.RepositoryContracts;

namespace MSGCompaniesMonitor.Services
{
    public class DocumentService : IDocumentsService
    {
        private readonly IDocumentsRepository _documentsRepository;

        public DocumentService(IDocumentsRepository documentsRepository)
        {
            _documentsRepository = documentsRepository;

        }

        public async Task<Document> CreateAsync(Document document)
        {
            if (document == null)
            {
                throw new ArgumentNullException(nameof(document));
            }

            try
            {

                return await _documentsRepository.CreateAsync(document);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<Document> DeleteAsync(int ID)
        {
            var document = await _documentsRepository.GetDocumentByIDAsync(ID);
            if (document == null)
            {
                throw new ArgumentNullException(nameof(document));
            }

            try
            {
                return await _documentsRepository.DeleteAsync(ID);
            }
            catch (Exception ex)
            {
                return null;
            }
        }



        public async Task<Document> EditAsync(Document document, int ID)
        {
            var companyTypeObj = await _documentsRepository.GetDocumentByIDAsync(ID);
            if (companyTypeObj == null || ID == 0)
            {
                throw new ArgumentNullException(nameof(companyTypeObj));
            }


            try
            {
                return await _documentsRepository.EditAsync(document, ID);
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        public async Task<List<Document>> GetAllDocumentsAsync()
        {
            return await _documentsRepository.GetAllDocumentsAsync();
        }


        public async Task<Document> GetDocumentByIDAsync(int ID)
        {
            return await _documentsRepository.GetDocumentByIDAsync(ID);
        }

        public async Task<Pagination<Document>> PaginationAsync(string? search, int page, int pageSize)
        {

            return await _documentsRepository.PaginationAsync(search, page, pageSize);

        }
    }


   }


