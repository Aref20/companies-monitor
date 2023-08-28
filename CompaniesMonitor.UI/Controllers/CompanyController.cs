using CompaniesMonitor.Core.Entities;
using CompaniesMonitor.Core.ServiceContracts;
using Microsoft.AspNetCore.Mvc;


namespace CompaniesMonitor.UI.Controllers
{
    [Route("[Controller]")]
    public class CompanyController : Controller
    {
        readonly private ICompaniesService _companiesService;
        readonly private ICompaniesTypeService _companiesTypeService;

        public CompanyController(ICompaniesService companiesService, ICompaniesTypeService companiesTypeService)
        {
            _companiesService = companiesService;
            _companiesTypeService = companiesTypeService;
        }


        [HttpGet]
        [Route("[Action]")]
        public async Task<IActionResult> Index(string? search,int page = 1, int pageSize = 6)
        {
            ViewBag.searchvalue = search;
            var paginationModel = await _companiesService.PaginationAsync(search, page, pageSize);
            return View(paginationModel);
        }

        [HttpGet]
        [Route("[Action]")]
        public async Task<IActionResult> Create()
        {
            ViewBag.CompaniesType = await _companiesTypeService.GetAllCompaniesTypeItemsAsync();
            return View();
        }

        [HttpPost]
        [Route("[Action]")]
        public async Task<IActionResult> Create(Company company, IFormCollection formCollection)
        {
            ViewBag.CompaniesType = await _companiesTypeService.GetAllCompaniesTypeItemsAsync();
            try
            {
                if (ModelState.IsValid)
                {
                    Company companyObj = await _companiesService.CreateAsync(company, formCollection);
                    TempData["ShowToast"] = true;
                    TempData["ToastMessage"] = "Record Inserted Successfully";
                    return RedirectToAction("Index", "Company");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.InnerException.Message);
            }
            TempData["ShowToast"] = true;
            ViewBag.ToastMessage = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage));
            return View(company);

        }

        
        [HttpGet]
        [Route("[Action]/{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            var company = await _companiesService.GetCompanyByIDAsync(id);

            if(company == null) return NotFound();
            
            ViewBag.CompaniesType = await _companiesTypeService.GetAllCompaniesTypeItemsAsync((int)company.CompanyTypeId);

            return View(company);
        }

        [HttpPost]
        [Route("[Action]/{id}")]
        public async Task<IActionResult> Edit(Company company, int id, IFormCollection formCollection)
        {
            var companyObj = await _companiesService.GetCompanyByIDAsync(id);
            ViewBag.CompaniesType = await _companiesTypeService.GetAllCompaniesTypeItemsAsync((int)companyObj.CompanyTypeId);
            try
            {
                if (ModelState.IsValid)
                {
                    await _companiesService.EditAsync(company, id, formCollection);
                    TempData["ShowToast"] = true;
                    TempData["ToastMessage"] = "Record Updated Successfully";
                    return RedirectToAction("Index", "Company");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.InnerException.Message);
            }

            TempData["ShowToast"] = true;
            ViewBag.ToastMessage = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage));

            return View(companyObj);

        }

        [HttpGet]
        [Route("[Action]/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var company = await _companiesService.GetCompanyByIDAsync(id);
            if (company == null) return NotFound();
            ViewBag.CompaniesType = await _companiesTypeService.GetAllCompaniesTypeItemsAsync((int)company.CompanyTypeId);

            return View(company);
        }

        [HttpPost]
        [Route("[Action]/{id}")]
        public async Task<IActionResult> Delete(Company company, int id)
        {
            var companyObj = await _companiesService.GetCompanyByIDAsync(id);
            ViewBag.CompaniesType = await _companiesTypeService.GetAllCompaniesTypeItemsAsync((int)companyObj.CompanyTypeId);
            try
            {
                if (ModelState.IsValid)
                {
                    await _companiesService.DeleteAsync(id);
                    TempData["ShowToast"] = true;
                    TempData["ToastMessage"] = "Record Updated Successfully";
                    return RedirectToAction("Index", "Company");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.InnerException.Message);
            }

            TempData["ShowToast"] = true;
            ViewBag.ToastMessage = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage));

            return View(companyObj);


        }








    }
}
