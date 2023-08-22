using Microsoft.AspNetCore.Mvc;
using MSGCompaniesMonitor.RepositoryContracts;
using MSGCompaniesMonitor.Models;



namespace MSGCompaniesMonitor.Controllers
{
    [Route("[Controller]")]
    public class PartnerController : Controller
    {
        readonly private IPartnersRepository _partnersRepository;
        public PartnerController(IPartnersRepository partnersRepository)
        {
            _partnersRepository = partnersRepository;
        }


        [HttpGet]
        [Route("[Action]")]
        public async Task<IActionResult> Index(string? search,int page = 1, int pageSize = 6)
        {
            ViewBag.searchvalue = search;
            return View(await _partnersRepository.PaginationAsync(search,page,pageSize));
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
        public async Task<IActionResult> Create(Partner partner)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.ShowToast = true;
                ViewBag.ToastMessage = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage));
                ViewBag.ToastType = "error";
                return View();
            }
            await _partnersRepository.CreateAsync(partner);
            return RedirectToAction("Index", "Partner");
        }

        [HttpGet]
        [Route("[Action]/{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            var partner = await _partnersRepository.GetPartnerByIDAsync(id);

            if (partner == null)
            {
                return NotFound();
            }

            return View(partner);
        }

        [HttpPost]
        [Route("[Action]/{id}")]
        public async Task<IActionResult> Edit(Partner partner, int id)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.ShowToast = true;
                ViewBag.ToastMessage = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage));
                ViewBag.ToastType = "error";
                return View();
            }
            await _partnersRepository.EditAsync(partner, id);
            return RedirectToAction("Index", "Partner");
        }

        [HttpGet]
        [Route("[Action]/{id}")]
        public async Task<IActionResult> Delete(int id, bool t = false)
        {
            var obj = await _partnersRepository.GetPartnerByIDAsync(id);

            if (obj == null)
            {
                return NotFound();
            }

            return View(obj);
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
            await _partnersRepository.DeleteAsync(id);
            return RedirectToAction("Index", "Partner");
        }



    }
}
