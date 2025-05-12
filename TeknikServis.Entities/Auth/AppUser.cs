using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace TeknikServis.Entities
{
	public class AppUser : IdentityUser<string>
	{
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
		public string Password { get; set; }
	}
}
