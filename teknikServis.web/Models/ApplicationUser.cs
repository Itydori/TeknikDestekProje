using Microsoft.AspNet.Identity.EntityFramework;

namespace teknikServis.web.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Ad { get; set; } 
        public bool Aktif { get; set; }
        public string Rol { get; set; } = "User";
    }
}
