using CompaniesMonitor.Core.Entities;
using CompaniesMonitor.Core.RepositoryContracts;
using CompaniesMonitor.Core.ServiceContracts;

namespace CompaniesMonitor.Core.Services
{
    public class UploadedFileService : IUploadedFilesService
    {


        private readonly IUploadedFilesRepository _UploadedFilesRepository;
        public UploadedFileService(IUploadedFilesRepository UploadedFilesRepository)
        {
            _UploadedFilesRepository = UploadedFilesRepository;
        }



        public async Task<UploadedFile> DeleteAsync(int id)
        {
            try
            {
                return await _UploadedFilesRepository.DeleteAsync(id);
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
                return await _UploadedFilesRepository.GetAllFilesAsync(id);
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
                return await _UploadedFilesRepository.GetAllFilesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }


   }


