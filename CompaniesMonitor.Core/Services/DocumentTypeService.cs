using MSGCompaniesMonitor.ServiceContracts;
using MSGCompaniesMonitor.Models;
using MSGCompaniesMonitor.RepositoryContracts;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace MSGCompaniesMonitor.Services
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
            if (documentType == null)
            {
                throw new ArgumentNullException(nameof(documentType));
            }
            try
            {

                return await _documentsTypeRepository.CreateAsync(documentType, formCollection);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<DocumentType> DeleteAsync(int id)
        {
            var documentType = await _documentsTypeRepository.GetDocumentTypeByIDAsync(id);
            if (documentType == null)
            {
                throw new ArgumentNullException(nameof(documentType));
            }

            try
            {
                return await _documentsTypeRepository.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<DocumentType> EditAsync(DocumentType documentType, int id, IFormCollection formCollection)
        {
            

            if (await _documentsTypeRepository.GetDocumentTypeByIDAsync(id) == null)
            {
                throw new ArgumentNullException(nameof(documentType));
            }

            try
            {
                return await _documentsTypeRepository.EditAsync(documentType, id, formCollection);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message + "Edit Servcie Error");
                return null;

            }
        }

        public async Task<List<DocumentType>> GetAllDocumentsTypeAsync()
        {
            return await _documentsTypeRepository.GetAllDocumentsTypeAsync();
        }

        public async Task<List<SelectListItem>> GetAllDocumentsAsync(int id)
        {
            return await _documentsTypeRepository.GetAllDocumentsAsync(id);
        }

        public async Task<List<SelectListItem>> GetAllDocumentsAsync()
        {
            return await _documentsTypeRepository.GetAllDocumentsAsync();
        }


        public async Task<DocumentType> GetDocumentTypeByIDAsync(int id)
        {
            return await _documentsTypeRepository.GetDocumentTypeByIDAsync(id);
        }

        public async Task<Pagination<DocumentType>> PaginationAsync(string? search, int page, int pageSize)
        {
            return await _documentsTypeRepository.PaginationAsync(search,page,pageSize);
        }

        public async Task<List<SelectListItem>> GetAllCompaniesAsync(int id)
        {
            return await _documentsTypeRepository.GetAllCompaniesAsync(id);
        }

        public async Task<List<SelectListItem>> GetAllCompaniesAsync()
        {
            return await  _documentsTypeRepository.GetAllCompaniesAsync();
        }

        public async Task<List<UploadedFile>> GetAllFilesAsync(int id)
        {
            return await _documentsTypeRepository.GetAllFilesAsync(id);
        }

        public async Task<List<UploadedFile>> GetAllFilesAsync()
        {
            return await _documentsTypeRepository.GetAllFilesAsync();
        }
    }
}
