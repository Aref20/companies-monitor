using CompaniesMonitor.Core.Entities;

namespace CompaniesMonitor.Core.RepositoryContracts
{
    public interface IUploadedFilesRepository
    {

        Task<UploadedFile> DeleteAsync(int id);
        Task<List<UploadedFile>> GetAllFilesAsync(int id);
        Task<List<UploadedFile>> GetAllFilesAsync();

    }
}
