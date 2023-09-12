using System.ComponentModel.DataAnnotations;


namespace CompaniesMonitor.Core.Entities
{
    public class Partner
    {
        
        public int PartnerId { get; set; }

        [Required]
        public string EnglishName { get; set; }

        [Required]
        public string ArabicName { get; set; }

        [Required]
        public string Nationality { get; set; }

        public ICollection<CompanyPartner>? CompaniesPartner { get; set; }




    }
}
