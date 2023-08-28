

namespace CompaniesMonitor.Core.Entities
{
    public class CompanyType
    {
        
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<Company>? Companies { get; set; }



    }



    
}
