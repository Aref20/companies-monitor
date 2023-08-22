
using Microsoft.AspNetCore.Mvc;
using MSGCompaniesMonitor.ServiceContracts;
using MSGCompaniesMonitor.Models;
using MSGCompaniesMonitor.Mail;



namespace MSGCompaniesMonitor.Controllers
{
    [Route("[Controller]")]
    public class CompanyController : Controller
    {
        readonly private ICompaniesService _companiesService;

        readonly private IEmailSender _emailSender;

        public CompanyController(ICompaniesService companiesService, IEmailSender emailSender)
        {
            _companiesService = companiesService;
            _emailSender = emailSender;
        }


        [HttpGet]
        [Route("[Action]")]
        public async Task<IActionResult> Index(string? search,int page = 1, int pageSize = 6)
        {
            ViewBag.searchvalue = search;
            return View(await _companiesService.PaginationAsync(search,page,pageSize));
        }

        [HttpGet]

        [Route("[Action]")]
        public async Task<IActionResult> Create()
        {
           // var message = new Message(new string[] { "aalhamad45@gmail.com" }, "Test email", "This is the content from our email.");
          //  _emailSender.SendEmail(message);
            ViewBag.Documentes = await _companiesService.GetAllDocumentsAsItemsAsync();
            ViewBag.Partners = await _companiesService.GetAllPartnerssAsItemsAsync();
            ViewBag.CompaniesType = await _companiesService.GetAllCompaniesTypeAsItemsAsync();
            return View();
        }

        [HttpPost]
        [Route("[Action]")]
        public async Task<IActionResult> Create(Company company, IFormCollection formCollection)
        {
            ViewBag.Partners = await _companiesService.GetAllPartnerssAsItemsAsync();
            ViewBag.CompaniesType = await _companiesService.GetAllCompaniesTypeAsItemsAsync();
            Company companyObj = await _companiesService.CreateAsync(company, formCollection);
            if (!ModelState.IsValid)
            {
                ViewBag.ShowToast = true;
                ViewBag.ToastMessage = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage));
                ViewBag.ToastType = "error";
                return View();
            }
                return RedirectToAction("Index", "Company");
  
        }

        
        [HttpGet]
        
        [Route("[Action]/{id}")]
        public async Task<IActionResult> Edit(int id)
        {

            var company = await _companiesService.GetCompanyByIDAsync(id);

            if (company == null) return NotFound();


            ViewBag.Partners = await _companiesService.GetAllPartnerssAsItemsAsync(id);
            ViewBag.CompaniesType = await _companiesService.GetAllCompaniesTypeAsItemsAsync(company.CompanyTypeId);

            return View(company);
        }

        [HttpPost]
        [Route("[Action]/{id}")]
        public async Task<IActionResult> Edit(Company company, int id, IFormCollection formCollection)
        {
            ViewBag.Partners = await _companiesService.GetAllPartnerssAsItemsAsync(id);
            ViewBag.CompaniesType = await _companiesService.GetAllCompaniesTypeAsItemsAsync(company.CompanyTypeId);
            Company companyObj = await _companiesService.EditAsync(company,id,formCollection);
            if (!ModelState.IsValid || company == null)
            {
                ViewBag.ShowToast = true;
                ViewBag.ToastMessage = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage));
                ViewBag.ToastType = "error";
                return View();
            }

                return RedirectToAction("Index", "Company");
        }

        [HttpGet]
        [Route("[Action]/{id}")]
        public async Task<IActionResult> Delete(Company company , int id, bool t = false)
        {
            ViewBag.Partners = await _companiesService.GetAllPartnerssAsItemsAsync(id);
            ViewBag.CompaniesType = await _companiesService.GetAllCompaniesTypeAsItemsAsync(company.CompanyTypeId);
            var companyObj = await _companiesService.GetCompanyByIDAsync(id);

            if (companyObj == null)
            {
                return NotFound();
            }

            return View(companyObj);
        }

        [HttpPost]
        [Route("[Action]/{id}")]
        public async Task<IActionResult> Delete(Company company, int id)
        {
            ViewBag.Partners = await _companiesService.GetAllPartnerssAsItemsAsync(id);
            ViewBag.CompaniesType = await _companiesService.GetAllCompaniesTypeAsItemsAsync(company.CompanyTypeId);

          /*  if (!ModelState.IsValid)
            {
                ViewBag.ShowToast = true;
                ViewBag.ToastMessage = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage));
                ViewBag.ToastType = "error";
                return View();
            }*/
            await _companiesService.DeleteAsync(id);
            return RedirectToAction("Index", "Company");
        }






    }
}
