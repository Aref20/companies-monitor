using CompaniesMonitor.Core.Entities;

namespace CompaniesMonitor.Core.ServiceContracts
{
    public interface IUploadedFilesService
    {
        Task<List<UploadedFile>> GetAllFilesAsync(int id);
        Task<List<UploadedFile>> GetAllFilesAsync();
        Task<UploadedFile> DeleteAsync(int id);

    }
}
