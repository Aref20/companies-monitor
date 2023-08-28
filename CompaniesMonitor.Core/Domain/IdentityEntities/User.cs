using Microsoft.AspNetCore.Identity;

namespace CompaniesMonitor.Core.Entities
{
    public class User : IdentityUser
    {
        public string? Name { get; set; }
    }
}
