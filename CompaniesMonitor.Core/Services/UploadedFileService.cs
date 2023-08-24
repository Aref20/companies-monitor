

using MSGCompaniesMonitor.Models;
using MSGCompaniesMonitor.RepositoryContracts;
using MSGCompaniesMonitor.ServiceContracts;




namespace MSGCompaniesMonitor.Service
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
          return await _UploadedFilesRepository.DeleteAsync(id);
        }
    }


   }


