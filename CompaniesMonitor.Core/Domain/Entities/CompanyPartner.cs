﻿using System.ComponentModel.DataAnnotations;

namespace CompaniesMonitor.Core.Entities
{
    public class CompanyPartner
    {
        [Key]
        public int CompanyPartnerId { get; set; }
        // Composite key to uniquely identify the relationship
        public int CompanyId { get; set; }
        public int PartnerId { get; set; }

        [Required]
        public double SharedJD { get; set; }
        public string? Percentage { get; set; } 

        // Navigation properties to represent the related entities
        public Company? Company { get; set; }
        public Partner? Partner { get; set; }
    }
}
