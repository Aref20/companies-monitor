
using MSGCompaniesMonitor.Models;

namespace MSGCompaniesMonitor.RepositoryContracts
{
    public interface IUploadedFilesRepository
    {

        Task<UploadedFile> DeleteAsync(int id);

    }
}
