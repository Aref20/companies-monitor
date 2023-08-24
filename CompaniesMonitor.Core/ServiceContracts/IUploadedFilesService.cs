
using MSGCompaniesMonitor.Models;

namespace MSGCompaniesMonitor.ServiceContracts
{
    public interface IUploadedFilesService
    {

        Task<UploadedFile> DeleteAsync(int id);

    }
}
