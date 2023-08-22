using Microsoft.AspNetCore.Mvc;
using MSGCompaniesMonitor.ServiceContracts;
using MSGCompaniesMonitor.Models;



namespace MSGCompaniesMonitor.Controllers
{
    [Route("[Controller]")]
    public class CompanyTypeController : Controller
    {
        readonly private ICompaniesTypeService _companiesTypeService;
        public CompanyTypeController(ICompaniesTypeService companiesTypeService)
        {
            _companiesTypeService = companiesTypeService;
        }


        [HttpGet]
        [Route("[Action]")]
        public async Task<IActionResult> Index(string? search,int page = 1, int pageSize = 6)
        {
            ViewBag.searchvalue = search;
            return View( await _companiesTypeService.PaginationAsync(search,page,pageSize));
        }

        [HttpGet]
        [Route("[Action]")]
        public IActionResult Create()
        {
            ViewBag.ShowToast = false;
            return View();
        }

        [HttpPost]
        [Route("[Action]")]
        public async Task<IActionResult> Create(CompanyType companyType)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.ShowToast = true;
                ViewBag.ToastMessage = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage));
                ViewBag.ToastType = "error";
                return View();
            }
            await _companiesTypeService.CreateAsync(companyType);
            return RedirectToAction("Index", "CompanyType");
        }

        [HttpGet]
        [Route("[Action]/{ID}")]
        public async Task<IActionResult> Edit(int id)
        {
            var companyType = await _companiesTypeService.GetCompanyTypeByIDAsync(id);

            if (companyType == null)
            {
                return NotFound();
            }

            return View(companyType);
        }

        [HttpPost]
        [Route("[Action]/{id}")]
        public async Task<IActionResult> Edit(CompanyType companyType, int id)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.ShowToast = true;
                ViewBag.ToastMessage = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage));
                ViewBag.ToastType = "error";
                return View();
            }
            await _companiesTypeService.EditAsync(companyType, id);
            return RedirectToAction("Index", "CompanyType");
        }

        [HttpGet]
        [Route("[Action]/{id}")]
        public async Task<IActionResult> Delete(int id, bool t = false)
        {
            var companyType = await _companiesTypeService.GetCompanyTypeByIDAsync(id);

            if (companyType == null)
            {
                return NotFound();
            }

            return View(companyType);
        }

        [HttpPost]
        [Route("[Action]/{id}")]
        public async Task<IActionResult> Delete(int id)
        {

            if (!ModelState.IsValid)
            {
                ViewBag.ShowToast = true;
                ViewBag.ToastMessage = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage));
                ViewBag.ToastType = "error";
                return View();
            }
            await _companiesTypeService.DeleteAsync(id);
            return RedirectToAction("Index", "CompanyType");
        }



    }
}
