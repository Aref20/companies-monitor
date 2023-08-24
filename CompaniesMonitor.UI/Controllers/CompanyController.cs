
using Microsoft.AspNetCore.Mvc;
using MSGCompaniesMonitor.ServiceContracts;
using MSGCompaniesMonitor.Models;
using MSGCompaniesMonitor.Mail;
using MSGCompaniesMonitor.ViewModels;



namespace MSGCompaniesMonitor.Controllers
{
    [Route("[Controller]")]
    public class CompanyController : Controller
    {
        readonly private ICompaniesService _companiesService;

        public CompanyController(ICompaniesService companiesService, IEmailSender emailSender)
        {
            _companiesService = companiesService;
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
        public async Task<IActionResult> Create(Company company)
        {
            var viewModel = await GetCompanyCreateModelAsync(company);
            return View(viewModel);
        }

        [HttpPost]
        [Route("[Action]")]
        public async Task<IActionResult> Create(Company company, IFormCollection formCollection)
        {
            
            if (ModelState.IsValid)
            {
                Company companyObj = await _companiesService.CreateAsync(company, formCollection);
                return RedirectToAction("Index", "Company");
            }
            ShowTostMessage("error");
            var viewModel = await GetCompanyCreateModelAsync(company);
            return View(viewModel);

        }

        
        [HttpGet]
        [Route("[Action]/{id}")]
        public async Task<IActionResult> Edit(int id)
        {

            var company = await _companiesService.GetCompanyByIDAsync(id);

            var viewModel = await GetCompanyEditModelAsync(company);
            return View(viewModel);
        }

        [HttpPost]
        [Route("[Action]/{id}")]
        public async Task<IActionResult> Edit(Company company, int id, IFormCollection formCollection)
        {

            if (ModelState.IsValid)
            {
                await _companiesService.EditAsync(company, id, formCollection);
                return RedirectToAction("Index", "Company");
            }
            ShowTostMessage("error");
            var viewModel = await GetCompanyEditModelAsync(company);
            return View(viewModel);

        }

        [HttpGet]
        [Route("[Action]/{id}")]
        public async Task<IActionResult> Delete(Company company , int id, bool t = false)
        {
            var companyObj = await _companiesService.GetCompanyByIDAsync(id);
            var viewModel = await GetCompanyDeleteModelAsync(companyObj);
            return View(viewModel);
        }

        [HttpPost]
        [Route("[Action]/{id}")]
        public async Task<IActionResult> Delete(Company company, int id)
        {
            

            if (ModelState.IsValid)
            {
                await _companiesService.DeleteAsync(id);
                return RedirectToAction("Index", "Company");
            }

            ShowTostMessage("error");
            var viewModel = await GetCompanyDeleteModelAsync(company);
            return View(viewModel);

        }



        //private methods
        private async Task<CompanyDeleteViewModel> GetCompanyDeleteModelAsync(Company company)
        {

            var viewModel = new CompanyDeleteViewModel
            {
                company = await _companiesService.GetCompanyByIDAsync(company.CompanyId),
                Partners = await _companiesService.GetAllPartnerssAsItemsAsync(company.CompanyId),
                CompaniesType = await _companiesService.GetAllCompaniesTypeAsItemsAsync(company.CompanyTypeId),
            };
            return viewModel;
        }

        private async Task<CompanyEditViewModel> GetCompanyEditModelAsync(Company company)
        {
            var companyObj = new Company
            {
                EnglishName = company.EnglishName,
                ArabicName = company.ArabicName,
                Number = company.Number,
                CapitalJD = company.CapitalJD,
                CreatedDate = company.CreatedDate,
                CloseDate = company.CloseDate,
                EnglishNotes = company.EnglishNotes,
                ArabicNotes = company.ArabicNotes,
            };
            var viewModel = new CompanyEditViewModel
            {
                company = companyObj,
                Partners = await _companiesService.GetAllPartnerssAsItemsAsync(company.CompanyId),
                CompaniesType = await _companiesService.GetAllCompaniesTypeAsItemsAsync(company.CompanyTypeId),
            };
            return viewModel;
        }


        private async Task<CompanyCreateViewModel> GetCompanyCreateModelAsync(Company company)
        {

            var companyPartnersObj = new List<CompanyPartner>()
            {
                new CompanyPartner()
                {
                    CompanyId = company.CompanyId,
                    PartnerId = 0, 
                    SharedJD = 0,
                }
            };
            var viewModel = new CompanyCreateViewModel
            {
                companyPartners = companyPartnersObj,
                company = null,
                Partners = await _companiesService.GetAllPartnerssAsItemsAsync(),
                CompaniesType = await _companiesService.GetAllCompaniesTypeAsItemsAsync(),
            };
            return viewModel;
        }

        private async void ShowTostMessage(string type)
        {
            TempData["ShowToast"] = true;
            TempData["ToastMessage"] = ModelState.Values
                                    .SelectMany(v => v.Errors.Select(e => e.ErrorMessage))
                                    .ToList();
            TempData["ToastType"] = type;
        }




    }
}
