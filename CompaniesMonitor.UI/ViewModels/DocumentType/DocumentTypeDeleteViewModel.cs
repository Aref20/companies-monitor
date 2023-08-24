using Microsoft.AspNetCore.Mvc.Rendering;
using MSGCompaniesMonitor.Models;

namespace MSGCompaniesMonitor.ViewModels
{
    public class DocumentTypeDeleteViewModel 
    {
        public IEnumerable<UploadedFile> uploadedFiles { get; set; }
        public DocumentType documentType { get; set; }
        public string FileName { get; set; }
        public List<SelectListItem> Documents { get; set; }
        public List<SelectListItem> Companies { get; set; }
        public bool ShowToast { get; set; }
        public List<string> ToastMessage { get; set; }
        public string ToastType { get; set; }
    }
}
