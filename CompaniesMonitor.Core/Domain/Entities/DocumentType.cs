using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MSGCompaniesMonitor.Models
{
    public class DocumentType
    {

        public int Id { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime ExpireyDate { get; set; }

        public double Amount { get; set; }

        public string? Note { get; set; }

        //Foreign key to represent the one-to-many relationship
        public int CompanyId { get; set; }

        //Navigation property to represent the related Company
        public Company? Company { get; set; }

        [NotMapped]
        [Required(ErrorMessage = "Please select file")]
        public required IFormFile File { get; set; }
        public string? FileName { get; set; }


        //Foreign key to represent the one-to-many relationship

        public int DocumentId { get; set; }
        public Document? Document { get; set; }
    }
}
