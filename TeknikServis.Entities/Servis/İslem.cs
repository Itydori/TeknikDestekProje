using System;
using System.ComponentModel.DataAnnotations;

namespace TeknikServis.Entities.Servis
{
    public class Islem
    {
        [Key]
        public int IslemId { get; set; }

        [Required]
        public int IsEmriTeslimId { get; set; }
        public virtual IsEmriTeslim IsEmriTeslimler { get; set; }

        [Required(ErrorMessage = "Onarımı yapan kişi girilmelidir.")]
        [StringLength(50, ErrorMessage = "En fazla 50 karakter olabilir.")]
        [Display(Name = "Onarımı Yapan Kişi")]
        public string OnarimYapan { get; set; }

        [Required(ErrorMessage = "Onarım tarihi girilmelidir.")]
        [DataType(DataType.Date)]
        [Display(Name = "Onarım Tarihi")]
        public DateTime OnarimTarihi { get; set; }

        [Required(ErrorMessage = "Stok yeri girilmelidir.")]
        [StringLength(100, ErrorMessage = "En fazla 100 karakter olabilir.")]
        [Display(Name = "Stok Yeri")]
        public string StokYeri { get; set; }

        [Required(ErrorMessage = "Yapılan işlemler girilmelidir.")]
        [StringLength(500, ErrorMessage = "En fazla 500 karakter olabilir.")]
        [Display(Name = "Yapılan İşlemler")]
        public string YapilanIslemler { get; set; }

        [Required(ErrorMessage = "Ücret girilmelidir.")]
        [Range(0, 1000000, ErrorMessage = "Ücret 0 ile 1.000.000 arasında olmalı.")]
        [Display(Name = "Ücret")]
        public decimal Ucret { get; set; }

        [Required(ErrorMessage = "Açıklama girilmelidir.")]
        [StringLength(500, ErrorMessage = "En fazla 500 karakter olabilir.")]
        [Display(Name = "Açıklama")]
        public string Aciklama { get; set; }
    }
}
