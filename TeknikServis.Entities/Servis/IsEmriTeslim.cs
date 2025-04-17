using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeknikServis.Entities.Enums;

namespace TeknikServis.Entities.Servis
{
	public class IsEmriTeslim
    {
        [Key]
        public int IsEmriTeslimId { get; set; }

        [Required]
        public int MusteriId { get; set; }
        public Musteri Musteri { get; set; }

        [Required(ErrorMessage = "Marka zorunludur.")]
        [StringLength(50, ErrorMessage = "Marka en fazla 50 karakter olabilir.")]
        public string Marka { get; set; }

        [Required(ErrorMessage = "Model zorunludur.")]
        [StringLength(100, ErrorMessage = "Model en fazla 100 karakter olabilir.")]
        public string Model { get; set; }

        [Required(ErrorMessage = "Geliş tarihi zorunludur.")]
        [DataType(DataType.Date)]
        [Display(Name = "Geliş Tarihi")]
        public DateTime GelisTarih { get; set; }

        [Required(ErrorMessage = "Arıza durumu girilmelidir.")]
        [StringLength(200, ErrorMessage = "Arıza durumu en fazla 200 karakter olabilir.")]
        [Display(Name = "Ürün Arızası")]
        public string ArizaDurumu { get; set; }

        [Required]
        [Range(2000, 2100, ErrorMessage = "Yıl 2000 ile 2100 arasında olmalıdır.")]
        [Display(Name = "Model Yılı")]
        public int Yil { get; set; }

        public bool Kapali { get; set; }

        [Required(ErrorMessage = "Fiş numarası zorunludur.")]
        [StringLength(20, ErrorMessage = "Fiş numarası en fazla 20 karakter olabilir.")]
        [Display(Name = "Fiş No")]
        public string FisNo { get; set; }

        [Required(ErrorMessage = "Garanti durumu seçilmelidir.")]
        [Display(Name = "Ürün Garanti Durumu")]
        public GarantiDurumuEnum GarantiDurumu { get; set; }

        [Required(ErrorMessage = "Ürün geliş sebebi seçilmelidir.")]
        [Display(Name = "Ürün Geliş Sebebi")]
        public ServisTalebiEnum ServisTalebi { get; set; }

        [StringLength(50)]
        [Display(Name = "Ödeme Şekli")]
        public string? OdemeSekli { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Alınan ödeme negatif olamaz.")]
        [Display(Name = "Alınan Ödeme")]
        public int? AlinanOdeme { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Teslim Tarihi")]
        public DateTime? KapatmaGunu { get; set; }

		public string? KapatmaTarihi { get; set; } // view'de kullanılıyorsa

		[DataType(DataType.Time)]
        [Display(Name = "Teslim Saati")]
        public TimeSpan? KapatmaSaati { get; set; }

        [StringLength(100)]
        [Display(Name = "Sipariş Durumu")]
        public string? SiparisDurumu { get; set; }

        [StringLength(500)]
        [Display(Name = "Teslimat Açıklaması")]
        public string? TeslimatAciklama { get; set; }

        public List<Islem> Islems { get; set; }
    }

}
