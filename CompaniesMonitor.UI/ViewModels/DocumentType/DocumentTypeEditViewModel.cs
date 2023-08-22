using Microsoft.AspNetCore.Mvc.Rendering;
using MSGCompaniesMonitor.Models;

namespace MSGCompaniesMonitor.ViewModels
{

    public class DocumentTypeEditViewModel : DocumentType
    {
        public DocumentType DocumentType { get; set; }
        public string FileName { get; set; }
        public List<SelectListItem> Documents { get; set; }
        public List<SelectListItem> Companies { get; set; }
        public bool ShowToast { get; set; }
        public IEnumerable<string> ToastMessage { get; set; }
        public string ToastType { get; set; }
    }
}
