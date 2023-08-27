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
            return View();
        }

        [HttpPost]
        [Route("[Action]")]
        public async Task<IActionResult> Create(Document document)
        {

            try 
            { 
                if (ModelState.IsValid)
                {
                    await _documentsService.CreateAsync(document);
                    TempData["ShowToast"] = true;
                    TempData["ToastMessage"] = "Record Inserted Successfully";
                    return RedirectToAction("Index", "Document");
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
            try
            {
                if (ModelState.IsValid)
                {
                    await _documentsService.EditAsync(document, id);
                    TempData["ShowToast"] = true;
                    TempData["ToastMessage"] = "Record Updated Successfully";
                    return RedirectToAction("Index", "Document");
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
            var document = await _documentsService.GetDocumentByIDAsync(id);

            if (document == null) return NotFound();

            return View(document);
        }

        [HttpPost]
        [Route("[Action]/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    await _documentsService.DeleteAsync(id);
                    TempData["ShowToast"] = true;
                    TempData["ToastMessage"] = "Record Deleted Successfully";
                    return RedirectToAction("Index", "Document");

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
