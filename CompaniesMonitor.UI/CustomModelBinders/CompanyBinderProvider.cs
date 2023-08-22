using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using MSGCompaniesMonitor.Models;

namespace MSGCompaniesMonitor.CustomModelBinders
{
    public class CompanyBinderProvider : IModelBinderProvider
    {
        public IModelBinder? GetBinder(ModelBinderProviderContext context)
        {
            if(context.Metadata.ModelType == typeof(Company))
            {
                return new BinderTypeModelBinder(typeof(CompanyModelBinder));
            }   
            return null;
        }
    }
}
