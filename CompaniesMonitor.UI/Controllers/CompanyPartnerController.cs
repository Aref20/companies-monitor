using Microsoft.AspNetCore.Mvc;
using MSGCompaniesMonitor.Models;
using MSGCompaniesMonitor.RepositoryContracts;
using MSGCompaniesMonitor.ServiceContracts;

namespace CompaniesMonitor.UI.Controllers
{
    public class CompanyPartnerController : Controller
    {
        readonly private ICompaniesPartnersService _companiesPartnersService;
        public CompanyPartnerController(ICompaniesPartnersService companiesPartnersService)
        {
            _companiesPartnersService = companiesPartnersService;
        }


        [HttpGet]
        [Route("[Action]")]
        public async Task<IActionResult> Index(string? search, int page = 1, int pageSize = 6)
        {
            ViewBag.searchvalue = search;
            return View(await _companiesPartnersService.PaginationAsync(search, page, pageSize));
        }


        [HttpGet]
        [Route("[Action]")]
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [Route("[Action]")]
        public async Task<IActionResult> Create(CompanyPartner companyPartner)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _companiesPartnersService.CreateAsync(companyPartner);
                    TempData["ShowToast"] = true;
                    TempData["ToastMessage"] = "Record Inserted Successfully";
                    return RedirectToAction("Index", "CompanyPartner");
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
        public async Task<IActionResult> Edit(int id)
        {
            var partner = await _companiesPartnersService.GetCompanyPartnerByIDAsync(id);

            if (partner == null) return NotFound();

            return View(partner);
        }

        [HttpPost]
        [Route("[Action]/{id}")]
        public async Task<IActionResult> Edit(CompanyPartner companyPartner, int id)
        {
            var partnerObj = await _companiesPartnersService.GetCompanyPartnerByIDAsync(id);

            if (partnerObj == null) return NotFound();
            try
            {
                if (ModelState.IsValid)
                {
                    await _companiesPartnersService.EditAsync(companyPartner, id);
                    TempData["ShowToast"] = true;
                    TempData["ToastMessage"] = "Record Updated Successfully";
                    return RedirectToAction("Index", "Partner");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.InnerException.Message);

            }
            TempData["ShowToast"] = true;
            ViewBag.ToastMessage = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage));
            return View(partnerObj);
        }

        [HttpGet]
        [Route("[Action]/{id}")]
        public async Task<IActionResult> Delete(int id, bool t = false)
        {
            var partner = await _companiesPartnersService.GetCompanyPartnerByIDAsync(id);

            if (partner == null) return NotFound();

            return View(partner);
        }

        [HttpPost]
        [Route("[Action]/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var partner = await _companiesPartnersService.GetCompanyPartnerByIDAsync(id);

            if (partner == null) return NotFound();
            try
            {

                if (ModelState.IsValid)
                {
                    await _companiesPartnersService.DeleteAsync(id);
                    TempData["ShowToast"] = true;
                    TempData["ToastMessage"] = "Record Deleted Successfully";
                    return RedirectToAction("Index", "Partner");

                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.InnerException.Message);

            }
            TempData["ShowToast"] = true;
            ViewBag.ToastMessage = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage));
            return View(partner);
        }


    }
}
