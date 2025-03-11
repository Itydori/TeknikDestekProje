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
		[StringLength(100)]
		public string Ad { get; set; }
		[StringLength(15)]
		public string Telefon { get; set; }
		[StringLength(500)]
		public string Adres { get; set; }
		[StringLength(255)]
        public string Eposta { get; set; }
        public bool Aktif { get; set; }
		public List<IsEmriTeslim> IsEmriTeslim { get; set; }
    }
}
