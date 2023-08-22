using Microsoft.AspNetCore.Mvc;
using MSGCompaniesMonitor.RepositoryContracts;
using MSGCompaniesMonitor.Models;
using MSGCompaniesMonitor.ServiceContracts;

namespace MSGCompaniesMonitor.Controllers
{
    [Route("[Controller]")]
    public class DocumentController : Controller
    {
        readonly private IDocumentsService _documentsService;
        public DocumentController(IDocumentsService documentsService)
        {
            _documentsService = documentsService;
        }


        [HttpGet]
        [Route("[Action]")]
        public async Task<IActionResult> Index(string? search,int page = 1, int pageSize = 6)
        {
            ViewBag.searchvalue = search;
            return View( await _documentsService.PaginationAsync(search,page,pageSize));
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
        public async Task<IActionResult> Create(Document document)
        {
            
            if (!ModelState.IsValid)
            {
                ViewBag.ShowToast = true;
                ViewBag.ToastMessage = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage));
                ViewBag.ToastType = "error";
                return View();
            }
            await _documentsService.CreateAsync(document);
            return RedirectToAction("Index", "Document");
        }

        [HttpGet]
        [Route("[Action]/{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            
            var document = await _documentsService.GetDocumentByIDAsync(id);

            if (document == null)
            {
                return NotFound();
            }

            return View(document);
        }

        [HttpPost]
        [Route("[Action]/{id}")]
        public async Task<IActionResult> Edit(Document document, int id)
        {
            
            if (!ModelState.IsValid)
            {
                ViewBag.ShowToast = true;
                ViewBag.ToastMessage = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage));
                ViewBag.ToastType = "error";
                return View();
            }
            await _documentsService.EditAsync(document, id);
            return RedirectToAction("Index", "Document");
        }

        [HttpGet]
        [Route("[Action]/{id}")]
        public async Task<IActionResult> Delete(int id, bool t = false)
        {
            var document = await _documentsService.GetDocumentByIDAsync(id);

            if (document == null)
            {
                return NotFound();
            }

            return View(document);
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
            await _documentsService.DeleteAsync(id);
            return RedirectToAction("Index", "Document");
        }



    }
}
