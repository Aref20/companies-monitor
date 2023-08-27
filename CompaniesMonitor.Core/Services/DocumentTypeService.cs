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

        public async Task<List<SelectListItem>> GetAllDocumentsAsync(int id)
        {
            try
            {
                return await _documentsTypeRepository.GetAllDocumentsAsync(id); 
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }

        public async Task<List<SelectListItem>> GetAllDocumentsAsync()
        {
            try
            {
                return await _documentsTypeRepository.GetAllDocumentsAsync();
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

        public async Task<List<SelectListItem>> GetAllCompaniesAsync(int id)
        {
            try
            {
                return await _documentsTypeRepository.GetAllCompaniesAsync(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
              
        }

        public async Task<List<SelectListItem>> GetAllCompaniesAsync()
        {
            try
            {
                return await _documentsTypeRepository.GetAllCompaniesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }

        public async Task<List<UploadedFile>> GetAllFilesAsync(int id)
        {
            try
            {
                return await _documentsTypeRepository.GetAllFilesAsync(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            

        }

        public async Task<List<UploadedFile>> GetAllFilesAsync()
        {
            try
            {
                return await _documentsTypeRepository.GetAllFilesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
           
        }
    }
}
