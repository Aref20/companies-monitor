using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace CompaniesMonitor.Core.Entities
{
    public class DocumentType
    {
        [Key]
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

        public IEnumerable<UploadedFile>? Files { get; set; }

        //Foreign key to represent the one-to-many relationship
        public int DocumentId { get; set; }
        public Document? Document { get; set; }


    }
}
