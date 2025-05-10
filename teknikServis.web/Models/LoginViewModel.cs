using System.ComponentModel.DataAnnotations;

namespace teknikServis.web.Models.Account
{
    public class LoginViewModel
    {
        [Required]
        public string UserName { get; set; }

        [Required, DataType(DataType.Password)]
        public string Password { get; set; }

        public bool RememberMe { get; set; }

        // Geri dönüş yolu gizli input için
        public string? ReturnUrl { get; set; }
    }

}
