using CompaniesMonitor.Core.Entities;
using CompaniesMonitor.Core.RepositoryContracts;
using CompaniesMonitor.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CompaniesMonitor.Infrastructure.Repository
{
    public class PartnerRepository : IPartnersRepository
    {
        private readonly ApplicationDbContext _context;

        private readonly DbSet<Partner> _DbSet;
        public PartnerRepository(ApplicationDbContext applicationDbContext)
        {
            _context = applicationDbContext;
            _DbSet = _context.Partners;
        }

        public async Task<Partner> CreateAsync(Partner partner)
        {
            _DbSet.Add(partner);
            await _context.SaveChangesAsync();
            return partner;
        }

        public async Task<Partner> DeleteAsync(int id)
        {
            var partner = await _DbSet.FindAsync(id);
            _DbSet.Remove(partner);
            await _context.SaveChangesAsync();
            return partner;
        }



        public async Task<Partner> EditAsync(Partner partner,int id)
        {
            var partnerObj = await _DbSet.FindAsync(id);

            partnerObj.ArabicName = partner.ArabicName;
            partnerObj.EnglishName = partner.ArabicName;
            partnerObj.Nationality = partner.Nationality;

            await _context.SaveChangesAsync();
            return partnerObj;
        }


        public async Task<List<Partner>> GetAllPartnersAsync()
        {
            return await _DbSet.ToListAsync();
        }


        public async Task<Partner> GetPartnerByIDAsync(int id)
        {
            return await _DbSet.FindAsync(id);
        }


        public async Task<List<SelectListItem>> GetAllPartnersItemsAsync()
        {
            return await _context.Partners.Select(temp => new SelectListItem { Value = temp.PartnerId.ToString(), Text = temp.EnglishName }).ToListAsync();
        }

        public async Task<List<SelectListItem>> GetAllPartnersItemsAsync(int id)
        {
            return await _context.Partners.Select(temp => new SelectListItem { Value = temp.PartnerId.ToString(), Text = temp.EnglishName, Selected = temp.PartnerId == id}).ToListAsync();
        }

        public async Task<Pagination<Partner>> PaginationAsync(string? search, int page, int pageSize)
        {
            var totalRecords = await _DbSet.CountAsync();
            var data = await _DbSet
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();


            var paginationModel = new Pagination<Partner>
                {
                    Data = data,
                    TotalRecords = totalRecords,
                    PageSize = pageSize,
                    CurrentPage = page
                };


                if (!string.IsNullOrWhiteSpace(search))
                {
                    
                    var filterdpaginationModel = new Pagination<Partner>
                    {
                        Data = await _DbSet.Where(temp => (temp.EnglishName.Contains(search) || temp.PartnerId.ToString().Contains(search)
                        || temp.ArabicName.Contains(search) || temp.Nationality.Contains(search))).Skip((page - 1) * pageSize).Take(pageSize).ToListAsync(),
                        TotalRecords = await _DbSet.Where(temp => temp.EnglishName.Contains(search)).CountAsync(),
                        PageSize = pageSize,
                        CurrentPage = page

                    };

                return filterdpaginationModel;
                }

            return  paginationModel;


        }
    }


   }


