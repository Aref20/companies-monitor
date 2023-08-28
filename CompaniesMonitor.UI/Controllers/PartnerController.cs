using CompaniesMonitor.Core.Entities;
using CompaniesMonitor.Core.ServiceContracts;
using Microsoft.AspNetCore.Mvc;


namespace ompaniesMonitor.UI.Controllers
{
    [Route("[Controller]")]
    public class PartnerController : Controller
    {
        readonly private IPartnersService _partnersService;
        public PartnerController(IPartnersService partnersService)
        {
            _partnersService = partnersService;
        }


        [HttpGet]
        [Route("[Action]")]
        public async Task<IActionResult> Index(string? search,int page = 1, int pageSize = 6)
        {
            ViewBag.searchvalue = search;
            return View(await _partnersService.PaginationAsync(search,page,pageSize));
        }


        [HttpGet]
        [Route("[Action]")]
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [Route("[Action]")]
        public async Task<IActionResult> Create(Partner partner)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _partnersService.CreateAsync(partner);
                    TempData["ShowToast"] = true;
                    TempData["ToastMessage"] = "Record Inserted Successfully";
                    return RedirectToAction("Index", "Partner");
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
            var partner = await _partnersService.GetPartnerByIDAsync(id);

            if (partner == null) return NotFound();

            return View(partner);
        }

        [HttpPost]
        [Route("[Action]/{id}")]
        public async Task<IActionResult> Edit(Partner partner, int id)
        {
            var partnerObj = await _partnersService.GetPartnerByIDAsync(id);

            if (partnerObj == null) return NotFound();
            try
            {
                if (ModelState.IsValid)
                {
                    await _partnersService.EditAsync(partner, id);
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
            var partner = await _partnersService.GetPartnerByIDAsync(id);

            if (partner == null) return NotFound();
            
            return View(partner);
        }

        [HttpPost]
        [Route("[Action]/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var partner = await _partnersService.GetPartnerByIDAsync(id);

            if (partner == null) return NotFound();
            try
            {

                if (ModelState.IsValid)
                {
                    await _partnersService.DeleteAsync(id);
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
