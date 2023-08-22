using Microsoft.AspNetCore.Mvc.Rendering;
using MSGCompaniesMonitor.Models;

namespace MSGCompaniesMonitor.ViewModels
{
    public class DocumentTypeCreateViewModel : DocumentType
    {
        public List<SelectListItem> Documents { get; set; }
        public List<SelectListItem> Companies { get; set; }
        public bool ShowToast { get; set; }
        public List<string> ToastMessage { get; set; }
        public string ToastType { get; set; }


    }
}
