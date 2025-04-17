using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace TeknikServis.Entities.Servis 
{
    public class Musteri 
    {
        [Key]
        public int MusteriId { get; set; }

        [Required(ErrorMessage = "Ad alanı zorunludur.")]
        [StringLength(100, ErrorMessage = "Ad en fazla 100 karakter olabilir.")]
        public string Ad { get; set; }

        [Required(ErrorMessage = "Telefon numarası zorunludur.")]
        [StringLength(15, ErrorMessage = "Telefon numarası en fazla 15 hane olabilir.")]
        [RegularExpression(@"^\d{10,15}$", ErrorMessage = "Telefon sadece rakam ve en az 10, en fazla 15 haneden oluşmalı.")]
        public string Telefon { get; set; }

        [Required(ErrorMessage = "Adres zorunludur.")]
        [StringLength(500, ErrorMessage = "Adres en fazla 500 karakter olabilir.")]
        public string Adres { get; set; }

        [Required(ErrorMessage = "E-posta zorunludur.")]
        [EmailAddress(ErrorMessage = "Geçerli bir e-posta giriniz.")]
        [StringLength(255, ErrorMessage = "E-posta en fazla 255 karakter olabilir.")]
        public string Eposta { get; set; }

        public bool Aktif { get; set; }
        public List<IsEmriTeslim>? IsEmriTeslim { get; set; }
    }
}
