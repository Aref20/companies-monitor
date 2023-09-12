using CompaniesMonitor.Core.Entities;
using CompaniesMonitor.Core.ServiceContracts;
using Microsoft.AspNetCore.Mvc;


namespace CompaniesMonitor.UI.Controllers
{
    [Route("[controller]")]
    public class CompanyPartnerController : Controller
    {
        readonly private ICompaniesPartnersService _companiesPartnersService;
        private readonly ICompaniesService _companiesService;
        private readonly IPartnersService _partnersService;
        public CompanyPartnerController(ICompaniesPartnersService companiesPartnersService, ICompaniesService companiesService, IPartnersService partnersService)
        {
            _companiesPartnersService = companiesPartnersService;
            _companiesService = companiesService;
            _partnersService = partnersService;
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
        public async Task<IActionResult> Create()
        {
            ViewBag.Companies = await _companiesService.GetAllCompaniesItemsAsync();
            ViewBag.Partners = await _partnersService.GetAllPartnersItemsAsync();
            return View();
        }


        [HttpPost]
        [Route("[Action]")]
        public async Task<IActionResult> Create(CompanyPartner companyPartner, IFormCollection formCollection)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _companiesPartnersService.CreateAsync(companyPartner, formCollection);
                    TempData["ShowToast"] = true;
                    TempData["ToastMessage"] = "Record Inserted Successfully";
                    return RedirectToAction("Index", "CompanyPartner");
                }
            }
            catch (Exception ex)
            {

                    ModelState.AddModelError("", ex.Message);
            }
            ViewBag.Companies = await _companiesService.GetAllCompaniesItemsAsync();
            ViewBag.Partners = await _partnersService.GetAllPartnersItemsAsync();
            TempData["ShowToast"] = true;
            ViewBag.ToastMessage = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage));
            return View();
        }

        [HttpGet]
        [Route("[Action]/{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            var companypartner = await _companiesPartnersService.GetCompanyPartnerByIDAsync(id);
            if (companypartner == null) return NotFound();

            ViewBag.Companies = await _companiesService.GetAllCompaniesItemsAsync(companypartner.CompanyId);
            ViewBag.Partners = await _partnersService.GetAllPartnersItemsAsync(companypartner.PartnerId);

            

            return View(companypartner);
        }

        [HttpPost]
        [Route("[Action]/{id}")]
        public async Task<IActionResult> Edit(CompanyPartner companyPartner, int id ,IFormCollection formCollection)
        {
            var partnerObj = await _companiesPartnersService.GetCompanyPartnerByIDAsync(id);


            if (partnerObj == null) return NotFound();
            try
            {
                if (ModelState.IsValid)
                {
                    await _companiesPartnersService.EditAsync(companyPartner, id, formCollection);
                    TempData["ShowToast"] = true;
                    TempData["ToastMessage"] = "Record Updated Successfully";
                    return RedirectToAction("Index", "CompanyPartner");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);

            }
            ViewBag.Companies = await _companiesService.GetAllCompaniesItemsAsync(partnerObj.CompanyId);
            ViewBag.Partners = await _partnersService.GetAllPartnersItemsAsync(partnerObj.PartnerId);
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
            ViewBag.Companies = await _companiesService.GetAllCompaniesItemsAsync(partner.CompanyId);
            ViewBag.Partners = await _partnersService.GetAllPartnersItemsAsync(partner.PartnerId);
            

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
                    return RedirectToAction("Index", "CompanyPartner");

                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.InnerException.Message);

            }
            ViewBag.Companies = await _companiesService.GetAllCompaniesItemsAsync(partner.CompanyId);
            ViewBag.Partners = await _partnersService.GetAllPartnersItemsAsync(partner.PartnerId);
            TempData["ShowToast"] = true;
            ViewBag.ToastMessage = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage));
            return View(partner);
        }


    }
}
