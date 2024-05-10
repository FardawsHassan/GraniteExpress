using Microsoft.AspNetCore.Identity;

namespace GraniteExpress.Models
{
    public class User : IdentityUser
    {
        public string? FullName { get; set; }
        public string? LastName { get; set; }

        public List<Journal> Journals { get; set; }
    }
}
