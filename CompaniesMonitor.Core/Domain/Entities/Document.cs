using System.ComponentModel.DataAnnotations;

namespace MSGCompaniesMonitor.Models
{
    public class Document
    {
        
        public int DocumentId { get; set; }

        [Required]
        public string Name { get; set; }

        
        public  ICollection<DocumentType>? DocumentesType { get; set; }


    }
}
