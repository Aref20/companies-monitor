using CompaniesMonitor.Core.Entities;
using CompaniesMonitor.Core.ServiceContracts;
using Microsoft.AspNetCore.Mvc;



namespace CompaniesMonitor.UI.Controllers
{
    [Route("[Controller]")]
    public class DocumentTypeController : Controller
    {
        readonly private IDocumentsTypeService _documentsTypeService;
        readonly private IUploadedFilesService _uploadedFilesService;
        readonly private IDocumentsService _documentsService;
        readonly private ICompaniesService _companiesService;

        public DocumentTypeController(IDocumentsTypeService documentsTypeService,IUploadedFilesService uploadedFilesService, IDocumentsService documentsService , ICompaniesService companiesService)
        {
            _documentsTypeService = documentsTypeService;
            _uploadedFilesService = uploadedFilesService;
            _documentsService = documentsService;
            _companiesService = companiesService;
        }

        [HttpGet]
        [Route("[Action]")]
        public async Task<IActionResult> Index(string? search, int page = 1, int pageSize = 6)
        {
            ViewBag.searchvalue = search;
            var paginationModel = await _documentsTypeService.PaginationAsync(search, page, pageSize);
            return View(paginationModel);
        }

        [HttpGet]
        [Route("[Action]")]
        public async Task<IActionResult> Create()
        {
            ViewBag.Documents = await _documentsService.GetAllDocumentsItemsAsync();
            ViewBag.Companies = await _companiesService.GetAllCompaniesItemsAsync();
            return View();
        }

        [HttpPost]
        [Route("[Action]")]
        public async Task<IActionResult> Create(DocumentType documentType, IFormCollection formCollection)
        {

            ViewBag.Documents = await _documentsService.GetAllDocumentsItemsAsync();
            ViewBag.Companies = await _companiesService.GetAllCompaniesItemsAsync();
           
            try
            {
                if (ModelState.IsValid)
                {
                    var documentTypeObj = await _documentsTypeService.CreateAsync(documentType, formCollection);
                    TempData["ShowToast"] = true;
                    TempData["ToastMessage"] = "Record Inserted Successfully";
                    return RedirectToAction("Index", "DocumentType");
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
            var documentType = await _documentsTypeService.GetDocumentTypeByIDAsync(id);
            if (documentType == null) return NotFound();

            ViewBag.UploadedFiles = await _uploadedFilesService.GetAllFilesAsync(documentType.Id);
            ViewBag.Documents = await _documentsService.GetAllDocumentsItemsAsync(documentType.DocumentId);
            ViewBag.Companies = await _companiesService.GetAllCompaniesItemsAsync(documentType.CompanyId);
            return View(documentType);
        }

        [HttpPost]
        [Route("[Action]/{id}")]
        public async Task<IActionResult> Edit(DocumentType documentType, int id, IFormCollection formCollection)
        {
            var documentTypeObj = await _documentsTypeService.GetDocumentTypeByIDAsync(id);
            if (documentTypeObj == null) return NotFound();

            ViewBag.UploadedFiles = await _uploadedFilesService.GetAllFilesAsync(documentTypeObj.Id);
            ViewBag.Documents = await _documentsService.GetAllDocumentsItemsAsync(documentTypeObj.DocumentId);
            ViewBag.Companies = await _companiesService.GetAllCompaniesItemsAsync(documentTypeObj.CompanyId);
            try
            {
                if (ModelState.IsValid)
                {
                    await _documentsTypeService.EditAsync(documentType, id, formCollection);
                    TempData["ShowToast"] = true;
                    TempData["ToastMessage"] = "Record Updated Successfully";
                    return RedirectToAction("Index", "DocumentType");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.InnerException.Message);
            }

            TempData["ShowToast"] = true;
            ViewBag.ToastMessage = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage));

            return View(documentTypeObj);
        }

        [HttpGet]
        [Route("[Action]/{id}")]
        public async Task<IActionResult> Delete(int id, bool t = false)
        {
            var documentType = await _documentsTypeService.GetDocumentTypeByIDAsync(id);
            if (documentType == null) return NotFound();

            ViewBag.UploadedFiles = await _uploadedFilesService.GetAllFilesAsync(documentType.Id);
            ViewBag.Documents = await _documentsService.GetAllDocumentsItemsAsync(documentType.DocumentId);
            ViewBag.Companies = await _companiesService.GetAllCompaniesItemsAsync(documentType.CompanyId);
            return View(documentType);
        }

        [HttpPost]
        [Route("[Action]/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var documentType = await _documentsTypeService.GetDocumentTypeByIDAsync(id);
            if (documentType == null) return NotFound();

            ViewBag.UploadedFiles = await _uploadedFilesService.GetAllFilesAsync(documentType.Id);
            ViewBag.Documents = await _documentsService.GetAllDocumentsItemsAsync(documentType.DocumentId);
            ViewBag.Companies = await _companiesService.GetAllCompaniesItemsAsync(documentType.CompanyId);
            try
            {
                if (ModelState.IsValid)
                {
                    await _documentsTypeService.DeleteAsync(id);
                    TempData["ShowToast"] = true;
                    TempData["ToastMessage"] = "Record Updated Successfully";
                    return RedirectToAction("Index", "DocumentType");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.InnerException.Message);
            }

            TempData["ShowToast"] = true;
            ViewBag.ToastMessage = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage));

            return View(documentType);
        }


        public async Task<IActionResult> DeleteFile(int id)
        {
            await _uploadedFilesService.DeleteAsync(id);
            // Get the current URL and redirect back to it
            string currentUrl = Request.Headers["Referer"].ToString();
            return Redirect(currentUrl);
        }
    }
}
