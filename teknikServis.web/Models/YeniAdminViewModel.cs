using System.ComponentModel.DataAnnotations;

namespace TeknikServis.web.Models;

public class YeniAdminViewModel
{
    [Required] public string UserName { get; set; } = null!;
    [Required(ErrorMessage ="E-posta adresi zorunludur!")]
    [RegularExpression(
         @"(?i)^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,10}$",
         ErrorMessage = "Sonunda 2–10 harfli uzantısı olan geçerli bir e-posta gir (örn. ali@site.dev).")]
    public string Email { get; set; } = default!;
    [Required, DataType(DataType.Password)] public string Password { get; set; } = null!;
    [Required] public string Name { get; set; } = null!;     // 👈 eklendi

}
