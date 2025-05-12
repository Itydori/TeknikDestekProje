using System.ComponentModel.DataAnnotations;

namespace TeknikServis.web.Models;

public class YeniAdminViewModel
{
    [Required] public string UserName { get; set; } = null!;
    [Required, EmailAddress] public string Email { get; set; } = null!;
    [Required, DataType(DataType.Password)] public string Password { get; set; } = null!;
    [Required] public string Name { get; set; } = null!;     // 👈 eklendi

}
