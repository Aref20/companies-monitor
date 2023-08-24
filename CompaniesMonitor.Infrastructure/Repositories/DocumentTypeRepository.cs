﻿using Microsoft.EntityFrameworkCore;
using MSGCompaniesMonitor.Data;
using MSGCompaniesMonitor.RepositoryContracts;
using MSGCompaniesMonitor.Models;
using System.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;


namespace MSGCompaniesMonitor.Repository
{
    public class DocumentTypeRepository : IDocumentsTypeRepository
    {
        private readonly ApplicationDbContext _context;

        private readonly DbSet<DocumentType> _DbSet;

        private readonly ILogger<DocumentTypeRepository> _logger;
        public DocumentTypeRepository(ApplicationDbContext applicationDbContext,ILogger<DocumentTypeRepository> logger)
        {
            _context = applicationDbContext;
            _DbSet = _context.DocumentsType;
            _logger = logger;
        }
        
        public async Task<DocumentType> CreateAsync(DocumentType documentType, IFormCollection formCollection)
        {

            documentType.Files = formCollection.Files.Select(file => new UploadedFile { File = file, FileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}", DocumentType = documentType ,DocumentTypeId = documentType.Id }).ToList();

            documentType.Company = await _context.Companies.FirstOrDefaultAsync(obj => obj.CompanyId == int.Parse(formCollection["documentType.Company"]));
            documentType.Document = await _context.Documents.FirstOrDefaultAsync(obj => obj.DocumentId == int.Parse(formCollection["documentType.Document"]));
            _DbSet.Add(documentType);

            if (await _context.SaveChangesAsync() != 0)
            {
                await AddFilesAsync(documentType.Files);
            }
            return documentType;
        }

        public async Task<DocumentType> DeleteAsync(int id)
        {
            var documentType = await _DbSet.FindAsync(id);

            List<UploadedFile> files = await _context.UploadedFiles.Where(obj => obj.DocumentTypeId == documentType.Id).ToListAsync();

            if (documentType == null)
            {
                throw new DataException("Document Type Not Found");
            }

            _DbSet.Remove(documentType);

            if (await _context.SaveChangesAsync() != 0)
            {
                await DeleteFilesAsync(files);
            }
                
            return documentType;
 
        }

        public async Task<DocumentType> EditAsync(DocumentType documentType, int id, IFormCollection formCollection)
        {
           
               // Retrieve the existing DocumentType entity from the database
               var documentTypeObj = await _DbSet.FindAsync(id);

               IEnumerable<UploadedFile> recivedFiles = formCollection.Files.Select(file => new UploadedFile
               {
                    File = file,
                    FileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}",
                    DocumentType = documentType,
                    DocumentTypeId = documentType.Id
               }).ToList();

               documentTypeObj.Files = documentTypeObj.Files?.Concat(recivedFiles).ToList();


               documentTypeObj.Company = await _context.Companies.FirstOrDefaultAsync(obj => obj.CompanyId == int.Parse(formCollection["documentType.Company"]));
               documentTypeObj.Document = await _context.Documents.FirstOrDefaultAsync(obj => obj.DocumentId == int.Parse(formCollection["documentType.Document"]));
               documentTypeObj.StartDate = documentType.StartDate;
               documentTypeObj.ExpireyDate = documentType.ExpireyDate;
               documentTypeObj.Note = documentType.Note;
               documentTypeObj.Amount = documentType.Amount;

                // Save changes to the database
                if (await _context.SaveChangesAsync() != 0  )
                {
                if(recivedFiles.Count() != 0)
                    await AddFilesAsync(recivedFiles);
                }

            return documentTypeObj;


        }

        public async Task<List<DocumentType>> GetAllDocumentsTypeAsync()
        {
            return await _DbSet
                .Include("Company")
                .Include("Document")
                .Include("Files")
                .ToListAsync();
        }


        public async Task<DocumentType> GetDocumentTypeByIDAsync(int id)
        {
            return await _DbSet.Include("Company")
                .Include("Document")
                .Include("Files")
                .FirstOrDefaultAsync(temp => temp.Id == id);
        }

        public async Task<List<SelectListItem>> GetAllDocumentsAsync(int id)
        {
  
            return await _context.Documents.Select(temp => new SelectListItem 
            { Value = temp.DocumentId.ToString(), Text = temp.Name, Selected = temp.DocumentId == id})
                .ToListAsync();
        }

        public async Task<List<SelectListItem>> GetAllDocumentsAsync()
        {
            return await _context.Documents.Select(temp => new SelectListItem 
            { Value = temp.DocumentId.ToString(), Text = temp.Name })
                .ToListAsync();
        }


        public async Task<Pagination<DocumentType>> PaginationAsync(string? search, int page, int pageSize)
        {
            var totalRecords = await _DbSet.Include("Company")
                .Include("Document").CountAsync();

            var data = await _DbSet.Include("Company")
                .Include("Document")
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();


            var paginationModel = new Pagination<DocumentType>
            {
                Data = data,
                TotalRecords = totalRecords,
                PageSize = pageSize,
                CurrentPage = page
            };


            if (!string.IsNullOrWhiteSpace(search))
            {

                var filterdpaginationModel = new Pagination<DocumentType>
                {
                    Data = await _DbSet.Where(temp => temp.Document.Name.Contains(search)).Skip((page - 1) * pageSize).Take(pageSize).ToListAsync(),
                    TotalRecords = await _DbSet.Where(temp => temp.Document.Name.Contains(search)).CountAsync(),
                    PageSize = pageSize,
                    CurrentPage = page

                };

                return filterdpaginationModel;
            }

            return paginationModel;


        }

        public async Task<List<SelectListItem>> GetAllCompaniesAsync(int id)
        {
            return await _context.Companies.Select(temp => new SelectListItem { Value = temp.CompanyId.ToString(), Text = temp.EnglishName, Selected = temp.CompanyId == id }).ToListAsync();
        }

        public async Task<List<SelectListItem>> GetAllCompaniesAsync()
        {
            return await _context.Companies.Select(temp => new SelectListItem { Value = temp.CompanyId.ToString(), Text = temp.EnglishName}).ToListAsync();
        }

        private async Task DeleteFilesAsync(List<UploadedFile> files)
        {
            // Loop through each uploaded file and delete them
            foreach (var file in files)
            {
                var fileName = file.FileName;
                // Delete the old file if it exists
                if (!string.IsNullOrEmpty(fileName))
                {
                    try
                    {
                        string oldPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Documents", fileName);
                        if (System.IO.File.Exists(oldPath))
                        {
                            await Task.Run(() => System.IO.File.Delete(oldPath)); // Run asynchronously
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Error while deleting file");
                    }
                }
            }
        }

        private async Task AddFilesAsync(IEnumerable<UploadedFile> files)
        {
            // Create the folder if not exist
            string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Documents");
            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            // Loop through each uploaded file and save them
            foreach (var uploadedFile in files)
            {
                string filePath = Path.Combine(folderPath, uploadedFile.FileName);
                try
                {
                    // Save the file to the server
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await uploadedFile.File.CopyToAsync(stream);
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error while uploading file and inserting data");
                }
            }
        }

        public async Task<List<UploadedFile>> GetAllFilesAsync(int id)
        {
            return await _context.UploadedFiles.Where(obj => obj.DocumentTypeId == id).ToListAsync();
        }

        public async Task<List<UploadedFile>> GetAllFilesAsync()
        {
            return await _context.UploadedFiles.ToListAsync();
        }
    }
}
