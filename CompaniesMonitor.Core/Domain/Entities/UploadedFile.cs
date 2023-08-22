using Microsoft.AspNetCore.Http;
using MSGCompaniesMonitor.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MSGCompaniesMonitor.Models
{
    public class UploadedFile
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public int DocumentTypeId { get; set; }
        public DocumentType DocumentType { get; set; }

        [NotMapped]
        [Required(ErrorMessage = "Please select file")]
        public required IFormFile File { get; set; }
    }
}
