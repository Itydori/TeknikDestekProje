using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeknikServis.Entities.Servis
{
	public class Islem
	{
        public int IslemId { get; set; }
        public int IsEmriId { get; set; }
        public string OnarimYapan { get; set; }
        public DateTime OnarimTarihi { get; set; }
        public string StokYeri { get; set; }
        public string YapilanIslemler { get; set; }

		public  decimal Ucret { get; set; }
        public string Aciklama { get; set; } = default;
        public int? IsEmriTeslimId { get; set; } 
        public virtual IsEmriTeslim IsEmriTeslimler { get; set; }

    }
}
