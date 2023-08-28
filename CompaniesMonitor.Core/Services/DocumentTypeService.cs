using CompaniesMonitor.Core.Entities;
using CompaniesMonitor.Core.RepositoryContracts;
using CompaniesMonitor.Core.ServiceContracts;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace CompaniesMonitor.Core.Services
{
    public class DocumentTypeService : IDocumentsTypeService
    {
        private readonly IDocumentsTypeRepository _documentsTypeRepository;

        private readonly ILogger<DocumentTypeService> _logger;  

        public DocumentTypeService(IDocumentsTypeRepository documentsTypeRepository,ILogger<DocumentTypeService> logger)
        {
            _documentsTypeRepository = documentsTypeRepository;

            _logger = logger;
        }

        public async Task<DocumentType> CreateAsync(DocumentType documentType, IFormCollection formCollection)
        {

            try
            {
                return await _documentsTypeRepository.CreateAsync(documentType, formCollection);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<DocumentType> DeleteAsync(int id)
        {

            try
            {
                return await _documentsTypeRepository.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<DocumentType> EditAsync(DocumentType documentType, int id, IFormCollection formCollection)
        {
            
            try
            {
                return await _documentsTypeRepository.EditAsync(documentType, id, formCollection);
            }
            catch (Exception ex)
            {
                throw ex;

            }
        }

        public async Task<List<DocumentType>> GetAllDocumentsTypeAsync()
        {
            try
            {
                return await _documentsTypeRepository.GetAllDocumentsTypeAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }




        public async Task<DocumentType> GetDocumentTypeByIDAsync(int id)
        {
            try
            {
                return await _documentsTypeRepository.GetDocumentTypeByIDAsync(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }

        public async Task<Pagination<DocumentType>> PaginationAsync(string? search, int page, int pageSize)
        {
            try
            {
                return await _documentsTypeRepository.PaginationAsync(search, page, pageSize);
            }
            catch (Exception ex)
            {
                throw ex;
            }
           
        }



    }
}
