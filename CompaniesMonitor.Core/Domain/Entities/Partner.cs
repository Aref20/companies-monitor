using System.ComponentModel.DataAnnotations;


namespace MSGCompaniesMonitor.Models
{
    public class Partner
    {
        
        public int PartnerId { get; set; }

        [Required]
        public string EnglishName { get; set; }

        [Required]
        public string ArabicName { get; set; }

        [Required]
        public double SharedJD { get; set; }


        public double? Percentage { get; set; }


        public ICollection<CompanyPartner>? CompaniesPartner { get; set; }

        /*public Company? Company { get; set; }*/


    }
}
