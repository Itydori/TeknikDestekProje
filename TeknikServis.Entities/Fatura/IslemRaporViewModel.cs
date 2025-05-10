namespace teknikServis.Entities.Fatura
{
	public class IslemRaporViewModel
	{
		// İşlem Detayları
		public string YapilanIslemler { get; set; }
		public decimal Ucret { get; set; }
		public string OnarimYapan { get; set; }
		public DateTime OnarimTarihi { get; set; }
		public string StokYeri { get; set; }
		public string Aciklama { get; set; }

		// Müşteri Bilgileri
		public string Ad { get; set; }
		public string Telefon { get; set; }
		public string Adres { get; set; }
		public string Eposta { get; set; }

		// Cihaz / Servis Bilgileri
		public string Marka { get; set; }
		public string Model { get; set; }
		public int Yil { get; set; }
		public string ArizaDurumu { get; set; }
		public string GarantiDurumu { get; set; }
		public string ServisTalebi { get; set; }
		public string FisNo { get; set; }

		// Teslimat Bilgileri
		public string KapatmaTarihi { get; set; }
		public string KapatmaSaati { get; set; }
		public string SiparisDurumu { get; set; }
		public string TeslimatAciklama { get; set; }

		// Ödeme Bilgileri
		public string OdemeSekli { get; set; }
		public int? AlinanOdeme { get; set; }

		// Toplam
		public decimal ToplamUcret { get; set; }  // Bu istenirse controller'da hesaplanıp her satıra yazılır
	}
}
