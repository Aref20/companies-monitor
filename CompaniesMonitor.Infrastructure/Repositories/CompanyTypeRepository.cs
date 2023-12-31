﻿using CompaniesMonitor.Core.Entities;
using CompaniesMonitor.Core.RepositoryContracts;
using CompaniesMonitor.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Http;

namespace CompaniesMonitor.Infrastructure.Repository
{
    public class CompanyTypeRepository : ICompaniesTypeRepository
    {


        private readonly ApplicationDbContext _context;

        private readonly DbSet<CompanyType> _DbSet;

        public CompanyTypeRepository(ApplicationDbContext applicationDbContext)
        {

            _context = applicationDbContext;
            _DbSet = _context.CompaniesType;

        }
        
        public async Task<CompanyType> CreateAsync(CompanyType companyType)
        {
            _DbSet.Add(companyType);
            await _context.SaveChangesAsync();
            return companyType;
        }

        public async Task<CompanyType> DeleteAsync(int id)
        {   
            var companyType = await _DbSet.FindAsync(id);
            _DbSet.Remove(companyType);
            await _context.SaveChangesAsync();
            return companyType;
        }



        public async Task<CompanyType> EditAsync(CompanyType companyType, int id)
        {
            var companyTypeObj = await _DbSet.FindAsync(id);

            companyTypeObj.Name = companyType.Name;
            await _context.SaveChangesAsync();
            return companyTypeObj;

        }
        public async Task<List<CompanyType>> GetAllCompaniesTypeAsync()
        {
            return await _DbSet.ToListAsync();
        }


        public async Task<CompanyType> GetCompanyTypeByIDAsync(int id)
        {
            return await _DbSet.FindAsync(id);
        }

        public async Task<List<SelectListItem>> GetAllCompaniesTypeItemsAsync(int id)
        {
            return await _context.CompaniesType.Select(temp => new SelectListItem { Value = temp.Id.ToString(), Text = temp.Name, Selected = temp.Id == id }).ToListAsync();
        }

        public async Task<List<SelectListItem>> GetAllCompaniesTypeItemsAsync()
        {
            return await _context.CompaniesType.Select(temp => new SelectListItem { Value = temp.Id.ToString(), Text = temp.Name }).ToListAsync();
        }
        public async Task<Pagination<CompanyType>> PaginationAsync(string? search, int page, int pageSize)
        {
            var totalRecords = await _DbSet.CountAsync();
            var data = await _DbSet
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();


            var paginationModel = new Pagination<CompanyType>
                {
                    Data = data,
                    TotalRecords = totalRecords,
                    PageSize = pageSize,
                    CurrentPage = page
                };


                if (!string.IsNullOrWhiteSpace(search))
                {
                    
                    var filterdpaginationModel = new Pagination<CompanyType>
                    {
                        Data = await _DbSet.Where((temp => temp.Name.Contains(search)
                        || temp.Id.ToString().Contains(search) 
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


