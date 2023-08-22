using System.ComponentModel.DataAnnotations;


namespace MSGCompaniesMonitor.Models
{
    public class Company
    {
        
        public int CompanyId { get; set; }

        [Required]
        public string EnglishName { get; set; }

        [Required]
        public string ArabicName { get; set; }

        public string? Number { get; set; }

        [DataType(DataType.Date)]
        public DateTime? CreatedDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime? CloseDate { get; set; }

        [Required]
        public double CapitalJD { get; set; }

        public string? EnglishNotes { get; set; }

        public string? ArabicNotes { get; set; }

        public ICollection<CompanyPartner> CompaniesPartner { get; set; }

        public virtual ICollection<DocumentType>? DocumentesType { get; set; }

        //Foreign key to represent the one-to-many relationship
        public int? CompanyTypeId { get; set; }

        //Navigation property to represent the related Company
        public CompanyType? CompanyType { get; set; }


    }
}
