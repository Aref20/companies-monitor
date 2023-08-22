using Microsoft.AspNetCore.Mvc;
using MSGCompaniesMonitor.Data;
using MSGCompaniesMonitor.DTO;
using MSGCompaniesMonitor.RepositoryContracts;


namespace MSGCompaniesMonitor.Controllers
{
    public class HomeController : Controller
    {


        [Route("/")]
    
        public IActionResult Index()
        {
            return View();
        }



    }
}