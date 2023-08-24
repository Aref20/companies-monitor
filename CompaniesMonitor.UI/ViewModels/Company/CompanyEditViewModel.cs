using Microsoft.AspNetCore.Mvc.Rendering;
using MSGCompaniesMonitor.Models;

namespace MSGCompaniesMonitor.ViewModels
{
    public class CompanyEditViewModel
    {

        public Company company { get; set; }
        public List<SelectListItem>? Partners { get; set; }
        public List<SelectListItem>? CompaniesType { get; set; }

    }
}