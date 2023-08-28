using CompaniesMonitor.Core.Entities;
using CompaniesMonitor.Core.RepositoryContracts;
using CompaniesMonitor.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;




namespace CompaniesMonitor.Infrastructure.Repository
{
    public class UploadedFileRepository : IUploadedFilesRepository
    {


        private readonly ApplicationDbContext _context;

        private readonly DbSet<UploadedFile> _DbSet;

        public UploadedFileRepository(ApplicationDbContext applicationDbContext)
        {

            _context = applicationDbContext;
            _DbSet = _context.UploadedFiles;

        }

        public async Task<List<UploadedFile>> GetAllFilesAsync(int id)
        {
            return await _DbSet.Where(obj => obj.DocumentTypeId == id).ToListAsync();
        }

        public async Task<List<UploadedFile>> GetAllFilesAsync()
        {
            return await _DbSet.ToListAsync();
        }

        public async Task<UploadedFile> DeleteAsync(int id)
        {
            var file = await _DbSet.FindAsync(id);
            _DbSet.Remove(file);

          if( await _context.SaveChangesAsync() != 0)
            {
                var fileName = file.FileName.ToString();
                // Delete the old file if a new one was uploaded and the old file exists
                if (!string.IsNullOrEmpty(fileName))
                {
                    try
                    {
                        string oldPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Documents", fileName);
                        if (System.IO.File.Exists(oldPath))
                        {
                            System.IO.File.Delete(oldPath);
                        }
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }

                }
            }
            return file;
        }
    }


   }


