using CompaniesMonitor.Core.Entities;
using CompaniesMonitor.Core.ServiceContracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Drawing.Printing;

namespace CompaniesMonitor.UI.Controllers
{
    [Route("[Controller]")]
    public class NotificationController : Controller
    {
        readonly private IDocumentsTypeService _documentsTypeService;

        public NotificationController(IDocumentsTypeService documentsTypeService)
        {
            _documentsTypeService = documentsTypeService;
        }

        [Route("[Action]")]
        public async Task<IActionResult> Index(string? search, int page = 1, int pageSize = 6)
        {
            // Calculate the start and end dates for the upcoming week or less
            DateTime today = DateTime.Today;
            DateTime nextWeekStart = today;
            DateTime nextWeekEnd = today.AddDays(7);

            ViewBag.searchvalue = search;

            Pagination<DocumentType> paginationModel = await _documentsTypeService.PaginationAsync(search, page, pageSize);

            // Filter documents that will expire in the upcoming week or less
            var filtered = paginationModel.Data
                .Where(doc => doc.ExpireyDate >= nextWeekStart && doc.ExpireyDate <= nextWeekEnd);

            var filterdpaginationModel = new Pagination<DocumentType>
            {
                Data = filtered.ToList(),
                TotalRecords = paginationModel.TotalRecords,
                PageSize = pageSize,
                CurrentPage = page
            };

            return View(filterdpaginationModel);
        }
    }
}
