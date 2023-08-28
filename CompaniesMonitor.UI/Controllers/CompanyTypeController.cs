using CompaniesMonitor.Core.Entities;
using CompaniesMonitor.Core.ServiceContracts;
using Microsoft.AspNetCore.Mvc;




namespace CompaniesMonitor.UI.Controllers
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
            return View();
        }

        [HttpPost]
        [Route("[Action]")]
        public async Task<IActionResult> Create(CompanyType companyType)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _companiesTypeService.CreateAsync(companyType);
                    TempData["ShowToast"] = true;
                    TempData["ToastMessage"] = "Record Inserted Successfully";
                    return RedirectToAction("Index", "CompanyType");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.InnerException.Message);
            }
            TempData["ShowToast"] = true;
            ViewBag.ToastMessage = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage));
            return View();
        }

        [HttpGet]
        [Route("[Action]/{ID}")]
        public async Task<IActionResult> Edit(int id)
        {
            var companyType = await _companiesTypeService.GetCompanyTypeByIDAsync(id);

            if (companyType == null) return NotFound();
          
            return View(companyType);
        }

        [HttpPost]
        [Route("[Action]/{id}")]
        public async Task<IActionResult> Edit(CompanyType companyType, int id)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _companiesTypeService.EditAsync(companyType, id);
                    TempData["ShowToast"] = true;
                    TempData["ToastMessage"] = "Record Updated Successfully";
                    return RedirectToAction("Index", "CompanyType");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.InnerException.Message);

            }
            TempData["ShowToast"] = true;
            ViewBag.ToastMessage = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage));
            return View();
        }

        [HttpGet]
        [Route("[Action]/{id}")]
        public async Task<IActionResult> Delete(int id, bool t = false)
        {
            var companyType = await _companiesTypeService.GetCompanyTypeByIDAsync(id);

            if (companyType == null) return NotFound();
            

            return View(companyType);
        }

        [HttpPost]
        [Route("[Action]/{id}")]
        public async Task<IActionResult> Delete(int id)
        {

            try
            {

                if (ModelState.IsValid)
                {
                    await _companiesTypeService.DeleteAsync(id);
                    TempData["ShowToast"] = true;
                    TempData["ToastMessage"] = "Record Deleted Successfully";
                    return RedirectToAction("Index", "CompanyType");

                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.InnerException.Message);

            }
            TempData["ShowToast"] = true;
            ViewBag.ToastMessage = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage));
            return View();
        }



    }
}
