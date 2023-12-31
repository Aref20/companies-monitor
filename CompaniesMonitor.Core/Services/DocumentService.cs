﻿using CompaniesMonitor.Core.Entities;
using CompaniesMonitor.Core.RepositoryContracts;
using CompaniesMonitor.Core.ServiceContracts;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CompaniesMonitor.Core.Services
{
    public class DocumentService : IDocumentsService
    {
        private readonly IDocumentsRepository _documentsRepository;

        public DocumentService(IDocumentsRepository documentsRepository)
        {
            _documentsRepository = documentsRepository;

        }

        public async Task<Document> CreateAsync(Document document)
        {


            try
            {
                return await _documentsRepository.CreateAsync(document);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Document> DeleteAsync(int id)
        {

            try
            {
                return await _documentsRepository.DeleteAsync(id);
            }
            catch (Exception ex)
            {
               throw ex;
            }
        }



        public async Task<Document> EditAsync(Document document, int id)
        {



            try
            {
                return await _documentsRepository.EditAsync(document, id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public async Task<List<Document>> GetAllDocumentsAsync()
        {
            try
            {
                return await _documentsRepository.GetAllDocumentsAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }


        public async Task<Document> GetDocumentByIDAsync(int id)
        {
            try
            {
                return await _documentsRepository.GetDocumentByIDAsync(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }

        public async Task<List<SelectListItem>> GetAllDocumentsItemsAsync(int id)
        {
            try
            {
                return await _documentsRepository.GetAllDocumentsItemsAsync(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public async Task<List<SelectListItem>> GetAllDocumentsItemsAsync()
        {
            try
            {
                return await _documentsRepository.GetAllDocumentsItemsAsync();
            }

            catch (Exception ex)
            {
                throw ex;
            }

        }
        public async Task<Pagination<Document>> PaginationAsync(string? search, int page, int pageSize)
        {
            try
            {
                return await _documentsRepository.PaginationAsync(search, page, pageSize);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            

        }
    }


   }


