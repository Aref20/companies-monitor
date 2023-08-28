using CompaniesMonitor.Core.ServiceContracts;
using CompaniesMonitor.UI.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;


namespace MSGCompaniesMonitor.Filters.ActionFilters
{
    public class CompanyActionFilter : IActionFilter
    {
        readonly private ICompaniesService _companiesService;

        public CompanyActionFilter(ICompaniesService companiesService)
        {
            _companiesService = companiesService;
        }

        public async Task OnActionExecutingAsync(ActionExecutingContext context)
        {
            if (context.RouteData.Values.TryGetValue("id", out var idValue) && idValue != null)
            {
                int companyId = Convert.ToInt32(idValue);
                var company = await _companiesService.GetCompanyByIDAsync(companyId);

                if (context.Controller is CompanyController companyController)
                {
                    //companyController.ViewBag.CompaniesType = await _companiesService.GetAllCompaniesTypeAsItemsAsync();
                }
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            // This method is executed after the action is executed.
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            throw new NotImplementedException();
        }
    }
}
