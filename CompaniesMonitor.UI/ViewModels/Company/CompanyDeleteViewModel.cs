using Microsoft.AspNetCore.Mvc.Rendering;
using MSGCompaniesMonitor.Models;

namespace MSGCompaniesMonitor.ViewModels
{
    public class CompanyDeleteViewModel : Company
    {
        public Company company { get; set; }
        public bool? ShowToast { get; set; }
        public List<string>? ToastMessage { get; set; }
        public string? ToastType { get; set; }
        public List<SelectListItem>? Partners { get; set; }
        public List<SelectListItem>? CompaniesType { get; set; }

    }
}