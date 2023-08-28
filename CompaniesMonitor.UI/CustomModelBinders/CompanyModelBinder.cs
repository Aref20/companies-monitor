using CompaniesMonitor.Core.Entities;
using CompaniesMonitor.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc.ModelBinding;


namespace MSGCompaniesMonitor.CustomModelBinders
{
    public class CompanyModelBinder : IModelBinder
    {
        private readonly ApplicationDbContext _db;
        public CompanyModelBinder(ApplicationDbContext db)
        {
            _db = db;
        }
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext == null)
            {
                throw new ArgumentNullException(nameof(bindingContext));
            }


            Company company = new Company
            {
                EnglishName = bindingContext.ValueProvider.GetValue("EnglishName").FirstValue,
                ArabicName = bindingContext.ValueProvider.GetValue("ArabicName").FirstValue,
                Number = bindingContext.ValueProvider.GetValue("Number").FirstValue,
                CreatedDate = DateTime.Parse(bindingContext.ValueProvider.GetValue("CreatedDate").FirstValue),
                CloseDate = DateTime.Parse(bindingContext.ValueProvider.GetValue("CloseDate").FirstValue),
                CapitalJD = double.Parse(bindingContext.ValueProvider.GetValue("CapitalJD").FirstValue),
                EnglishNotes = bindingContext.ValueProvider.GetValue("EnglishNotes").FirstValue,
                ArabicNotes = bindingContext.ValueProvider.GetValue("ArabicNotes").FirstValue,
             //   CompaniesPartner = CompaniesPartner,
             //   CompanyType = CompanyType
               /* DocumentesType = arg.DocumentesType*/

            };

            
            bindingContext.Result = ModelBindingResult.Success(company);
            return Task.CompletedTask;


        }
    }
}
