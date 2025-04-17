namespace teknikServis.web.Models
{
	public class IslemRaporViewModel
	{
		public string YapilanIslemler { get; set; }
		public decimal Ucret { get; set; }
		public string OnarimYapan { get; set; }
		public DateTime OnarimTarihi { get; set; }
		public string StokYeri { get; set; }
		public string Aciklama { get; set; }

		// Yeni Eklenenler:
		public string Ad { get; set; }
		public string TeslimTarihi { get; set; }
	}
}
