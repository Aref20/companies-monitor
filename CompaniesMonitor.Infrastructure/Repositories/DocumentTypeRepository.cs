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
         

            // Create the folder if not exist
            string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Documents");
            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            // List to store the unique filenames
            List<string> uniqueFileNames = new List<string>();

            // Loop through each uploaded file and save them
            foreach (var uploadedFile in documentType.Files)
            {
                // Get file extension
                FileInfo fileInfo = new FileInfo(uploadedFile.File.FileName);

                // Generate a unique filename
                string uniqueFileName = $"{Guid.NewGuid()}{fileInfo.Extension}";

                // Add the filename to the list
                uniqueFileNames.Add(uniqueFileName);

                // Create a new UploadedFile instance
                var newUploadedFile = new UploadedFile
                {
                    File = null,
                    FileName = uniqueFileName,
                    DocumentType = documentType // Associate the uploaded file with the parent DocumentType
                };

                // Perform any other operations needed for the UploadedFile entity
                // ...

                string filePath = Path.Combine(folderPath, uniqueFileName);

                // Save the file to the server
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await uploadedFile.File.CopyToAsync(stream);
                }
            }

            // Assign the list of unique filenames to the DocumentType entity
            documentType.Files = uniqueFileNames.Select(fileName => new UploadedFile { File = null, FileName = fileName }).ToList();



            documentType.Company = await _context.Companies.FirstOrDefaultAsync(obj => obj.CompanyId == int.Parse(formCollection["Company"]));
            documentType.Document = await _context.Documents.FirstOrDefaultAsync(obj => obj.DocumentId == int.Parse(formCollection["Document"]));
            _DbSet.Add(documentType);
            await _context.SaveChangesAsync();
            return documentType;
        }

        public async Task<DocumentType> DeleteAsync(int id)
        {
         /*   var documentType = await _DbSet.FindAsync(id);

            

            if (documentType == null)
            {
                throw new DataException("Document Type Not Found");
            }

            var fileName = documentType.FileNames.ToString();
            // Delete the old file if a new one was uploaded and the old file exists
            if (!string.IsNullOrEmpty(fileName))
            {
                string oldPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Documents", fileName);
                if (System.IO.File.Exists(oldPath))
                {
                    System.IO.File.Delete(oldPath);
                }
            }

            _DbSet.Remove(documentType);
            await _context.SaveChangesAsync();
            return documentType;*/
         return null;
        }

        public async Task<DocumentType> EditAsync(DocumentType documentType, int id, IFormCollection formCollection)
        {
            /*   // Retrieve the existing DocumentType entity from the database
               var documentTypeObj = await _DbSet.FindAsync(id);


               // Get the existing document type from the database


               var oldFileName = documentTypeObj.FileNames.ToString();

               // Check if a new file was uploaded
               if (documentType.Files != null )//&& documentType.Files.Length > 0)
               {

                   FileInfo fileInfo = new FileInfo(documentType.Files.First().FileName);

                   string fileName = Guid.NewGuid().ToString(); //+ fileInfo.Extension;
                   // Process the uploaded file (e.g., save it to a directory)
                   string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Documents");

                   string filePath = Path.Combine(path, fileName);

                   try { 

                   using (var stream = new FileStream(filePath, FileMode.Create))
                   {
                       await documentType.Files.First().CopyToAsync(stream);
                   }

                   }
                        catch (Exception ex)
                   {
                       _logger.LogError(ex, "Error while uploading file and inserting data");
                       throw new DataException("Error while uploading file" + ex);
                   }

                   // Update the FileName property to the new uploaded file's name
                  // documentTypeObj.FileNames = fileName;
               }


               // Delete the old file if a new one was uploaded and the old file exists
               if (!string.IsNullOrEmpty(oldFileName) )//&& documentTypeObj.FileNames != oldFileName)
               {
                   string oldPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Documents", oldFileName);
                   if (System.IO.File.Exists(oldPath))
                   {
                       System.IO.File.Delete(oldPath);
                   }
               }

               // Update other properties


               documentTypeObj.Company = await _context.Companies.FirstOrDefaultAsync(obj => obj.CompanyId == int.Parse(formCollection["Company"]));
               documentTypeObj.Document = await _context.Documents.FirstOrDefaultAsync(obj => obj.DocumentId == int.Parse(formCollection["Document"]));
               documentTypeObj.StartDate = documentType.StartDate;
               documentTypeObj.ExpireyDate = documentType.ExpireyDate;
               documentTypeObj.Note = documentType.Note;

               // Save changes to the database
               await _context.SaveChangesAsync();

               return documentTypeObj;*/
            return null;
        }

        public async Task<List<DocumentType>> GetAllDocumentsTypeAsync()
        {
            return await _DbSet
                .Include("Company")
                .Include("Document").ToListAsync();
        }


        public async Task<DocumentType> GetDocumentTypeByIDAsync(int id)
        {
            return await _DbSet.Include("Company")
                .Include("Document")
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
    }
}
