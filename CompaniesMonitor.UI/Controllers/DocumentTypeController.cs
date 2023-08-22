using Microsoft.AspNetCore.Mvc;
using MSGCompaniesMonitor.ServiceContracts;
using MSGCompaniesMonitor.Models;
using MSGCompaniesMonitor.ViewModels;

namespace MSGCompaniesMonitor.Controllers
{
    [Route("[Controller]")]
    public class DocumentTypeController : Controller
    {
        readonly private IDocumentsTypeService _documentsTypeService;

        public DocumentTypeController(IDocumentsTypeService documentsTypeService)
        {
            _documentsTypeService = documentsTypeService;
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
        public async Task<IActionResult> Create(DocumentType documentType)
        {
            var viewModel = await GetDocumentTypeCreateViewModelAsync();
            return View(viewModel);
        }

        [HttpPost]
        [Route("[Action]")]
        public async Task<IActionResult> Create(DocumentType documentType, IFormCollection formCollection)
        {
            if (ModelState.IsValid)
            {
                await _documentsTypeService.CreateAsync(documentType, formCollection);
                return RedirectToAction("Index");
            }

            var viewModel = await GetDocumentTypeCreateViewModelAsync();
            viewModel.ShowToast = true;
            viewModel.ToastMessage = ModelState.Values
                                    .SelectMany(v => v.Errors.Select(e => e.ErrorMessage))
                                    .ToList();
            viewModel.ToastType = "error";

            return View(viewModel);
        }

        [HttpGet]
        [Route("[Action]/{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            var documentType = await _documentsTypeService.GetDocumentTypeByIDAsync(id);
            if (documentType == null) return NotFound();

            var viewModel = await GetDocumentTypeEditViewModelAsync(documentType);
            return View(viewModel);
        }

        [HttpPost]
        [Route("[Action]/{id}")]
        public async Task<IActionResult> Edit(DocumentType documentType, int id, IFormCollection formCollection)
        {
            var documentType2 = await _documentsTypeService.GetDocumentTypeByIDAsync(id);
            if (documentType2 == null) return NotFound();

            if (ModelState.IsValid)
            {
                await _documentsTypeService.EditAsync(documentType, id, formCollection);
                return RedirectToAction("Index");
            }

            var viewModel = await GetDocumentTypeEditViewModelAsync(documentType2);
            viewModel.ShowToast = true;
            viewModel.ToastMessage = ModelState.Values
                                    .SelectMany(v => v.Errors.Select(e => e.ErrorMessage))
                                    .ToList();
            viewModel.ToastType = "error";

            return View(viewModel);
        }

        [HttpGet]
        [Route("[Action]/{id}")]
        public async Task<IActionResult> Delete(int id, bool t = false)
        {
            var documentType = await _documentsTypeService.GetDocumentTypeByIDAsync(id);
            if (documentType == null) return NotFound();

            var viewModel = await GetDocumentTypeDeleteViewModelAsync(documentType);
            return View(viewModel);
        }

        [HttpPost]
        [Route("[Action]/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var documentType = await _documentsTypeService.GetDocumentTypeByIDAsync(id);
            if (!ModelState.IsValid) return await HandleModelStateErrors(documentType);

            await _documentsTypeService.DeleteAsync(id);
            return RedirectToAction("Index");
        }


        private async Task<DocumentTypeCreateViewModel> GetDocumentTypeCreateViewModelAsync()
        {

            var viewModel = new DocumentTypeCreateViewModel
            {
                File = null,
                Documents = await _documentsTypeService.GetAllDocumentsAsync(),
                Companies = await _documentsTypeService.GetAllCompaniesAsync(),
                ShowToast = false
            };
            return viewModel;
        }

        private async Task<DocumentTypeEditViewModel> GetDocumentTypeEditViewModelAsync(DocumentType documentType)
        {
            var viewModel = new DocumentTypeEditViewModel
            {
                File = null,
                DocumentType = documentType,

                FileName = documentType.FileName,
                Documents = await _documentsTypeService.GetAllDocumentsAsync(documentType.DocumentId),
                Companies = await _documentsTypeService.GetAllCompaniesAsync(documentType.CompanyId)
            };
            return viewModel;
        }

        private async Task<DocumentTypeDeleteViewModel> GetDocumentTypeDeleteViewModelAsync(DocumentType documentType)
        {
            var viewModel = new DocumentTypeDeleteViewModel
            {
                File = null,
                DocumentType = documentType,
                FileName = documentType.FileName,
                Documents = await _documentsTypeService.GetAllDocumentsAsync(documentType.DocumentId),
                Companies = await _documentsTypeService.GetAllCompaniesAsync(documentType.CompanyId)
            };
            return viewModel;
        }

        private async Task<IActionResult> HandleModelStateErrors(DocumentType documentType)
        {
            var viewModel = await GetDocumentTypeDeleteViewModelAsync(documentType);
            viewModel.ShowToast = true;
            viewModel.ToastMessage = ModelState.Values
                                    .SelectMany(v => v.Errors.Select(e => e.ErrorMessage))
                                    .ToList();
            viewModel.ToastType = "error";
            return View(viewModel);
        }
    }
}
