using CompaniesMonitor.Core.ServiceContracts;
using Microsoft.AspNetCore.Mvc;



namespace ompaniesMonitor.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICompaniesService _companyService;
        private readonly IPartnersService _partnerService;
        private readonly ICompaniesPartnersService _companyPartnerService;
        public HomeController(IPartnersService partnerService, ICompaniesPartnersService companyPartnerService, ICompaniesService companiesService)
        {
            _partnerService = partnerService;
            _companyPartnerService = companyPartnerService;
            _companyService = companiesService;

        }


        [Route("/")]
    
        public IActionResult Index()
        {
            ViewBag.Companies = _companyService.GetAllCompaniesAsync().Result.Count();
            ViewBag.Partners = _partnerService.GetAllPartnersAsync().Result.Count();
            ViewBag.CompaniesPartners = _companyPartnerService.GetAllCompaniesPartnersAsync().Result.Count();
            return View();
        }



    }
}