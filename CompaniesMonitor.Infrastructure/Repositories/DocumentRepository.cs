using CompaniesMonitor.Core.Entities;
using CompaniesMonitor.Core.RepositoryContracts;
using CompaniesMonitor.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

using Microsoft.AspNetCore.Mvc.Rendering;

namespace CompaniesMonitor.Infrastructure.Repository
{
    public class DocumentRepository : IDocumentsRepository
    {
        private readonly ApplicationDbContext _context;

        private readonly DbSet<Document> _DbSet;
        public DocumentRepository(ApplicationDbContext applicationDbContext)
        {
            _context = applicationDbContext;
            _DbSet = _context.Documents;
        }

        public async Task<Document> CreateAsync(Document document)
        {
            _DbSet.Add(document);
            await _context.SaveChangesAsync();
            return document;
        }

        public async Task<Document> DeleteAsync(int id)
        {
            var documet = await _DbSet.FindAsync(id);
            _DbSet.Remove(documet);
            await _context.SaveChangesAsync();
            return documet;
        }



        public async Task<Document> EditAsync(Document document,int id)
        {
            var documentObj = await _DbSet.FindAsync(id);

            documentObj.Name = document.Name;
            await _context.SaveChangesAsync();
            return documentObj;
        }


        public async Task<List<Document>> GetAllDocumentsAsync()
        {
            return await _DbSet.ToListAsync();
        }


        public async Task<Document> GetDocumentByIDAsync(int id)
        {
            return await _DbSet.FindAsync(id);
        }


        public async Task<List<SelectListItem>> GetAllDocumentsItemsAsync()
        {
            return await _context.Documents.Select(temp => new SelectListItem { Value = temp.DocumentId.ToString(), Text = temp.Name }).ToListAsync();
        }

        public async Task<List<SelectListItem>> GetAllDocumentsItemsAsync(int id)
        {
            return await _context.Documents.Select(temp => new SelectListItem { Value = temp.DocumentId.ToString(), Text = temp.Name, Selected = temp.DocumentId == id }).ToListAsync();
        }

        public async Task<Pagination<Document>> PaginationAsync(string? search, int page, int pageSize)
        {
            var totalRecords = await _DbSet.CountAsync();
            var data = await _DbSet
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();


            var paginationModel = new Pagination<Document>
                {
                    Data = data,
                    TotalRecords = totalRecords,
                    PageSize = pageSize,
                    CurrentPage = page
                };


                if (!string.IsNullOrWhiteSpace(search))
                {
                    
                    var filterdpaginationModel = new Pagination<Document>
                    {
                        Data = await _DbSet.Where((temp => temp.Name.Contains(search)
                        || temp.DocumentId.ToString().Contains(search)


                        )).Skip((page - 1) * pageSize).Take(pageSize).ToListAsync(),
                        TotalRecords = await _DbSet.Where(temp => temp.Name.Contains(search)).CountAsync(),
                        PageSize = pageSize,
                        CurrentPage = page

                    };

                return filterdpaginationModel;
                }

            return  paginationModel;


        }


    }


   }


