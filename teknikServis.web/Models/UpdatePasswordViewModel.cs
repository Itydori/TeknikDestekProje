using System.ComponentModel.DataAnnotations;

namespace TeknikServis.web.Models;

public class UpdatePasswordViewModel
{
    public string Id { get; set; } = null!;          // kullanıcı Id
    public string Email { get; set; } = null!;       // sadece görüntü için

    [Required, DataType(DataType.Password)]
    [Display(Name = "Yeni Şifre")]
    public string NewPassword { get; set; } = null!;

    [Required, DataType(DataType.Password)]
    [Display(Name = "Yeni Şifre (Tekrar)")]
    [Compare(nameof(NewPassword))]
    public string ConfirmPassword { get; set; } = null!;
}
