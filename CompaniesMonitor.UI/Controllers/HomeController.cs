using Microsoft.AspNetCore.Mvc;



namespace ompaniesMonitor.UI.Controllers
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