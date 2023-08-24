

using Microsoft.AspNetCore.Identity;

namespace MSGCompaniesMonitor.Models
{
    public class User : IdentityUser
    {
        public string? Name { get; set; }
    }
}
