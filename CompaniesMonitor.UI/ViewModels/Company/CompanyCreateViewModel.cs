using CompaniesMonitor.Core.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace MSGCompaniesMonitor.ViewModels
{
    public class CompanyCreateViewModel
    {
        public Company? company { get; set; }
        public List<CompanyPartner>? companyPartners { get; set; }
        public List<SelectListItem>? Partners { get; set; }
        public List<SelectListItem>? CompaniesType { get; set; }

    }
}