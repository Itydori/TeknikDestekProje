using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using teknikServis.Entities.Fatura;
using TeknikServis.DataAccess;
using TeknikServis.Entities.Servis;

namespace TeknikServis.Business.Abstract
{
	public class IslemRepository : Repository<Islem>, IIslemRepository
	{
		public IslemRepository(TeknikServisDbContext context) : base(context)
		{
		}


		public List<IslemRaporViewModel> GetAllReport(int id)
		{
			var veri = _context.Islemler
			.Include(x => x.IsEmriTeslimler)
				.ThenInclude(y => y.Musteri)
			.Where(x => x.IsEmriTeslimId == id)
			.Select(x => new IslemRaporViewModel
			{
				// İşlem Bilgileri
				YapilanIslemler = x.YapilanIslemler,
				Ucret = x.Ucret,
				OnarimYapan = x.OnarimYapan,
				OnarimTarihi = x.OnarimTarihi,
				StokYeri = x.StokYeri,
				Aciklama = x.Aciklama,

				// Müşteri
				Ad = x.IsEmriTeslimler.Musteri.Ad,
				Telefon = x.IsEmriTeslimler.Musteri.Telefon,
				Adres = x.IsEmriTeslimler.Musteri.Adres,
				Eposta = x.IsEmriTeslimler.Musteri.Eposta,

				// Cihaz / Servis
				Marka = x.IsEmriTeslimler.Marka,
				Model = x.IsEmriTeslimler.Model,
				Yil = x.IsEmriTeslimler.Yil,
				ArizaDurumu = x.IsEmriTeslimler.ArizaDurumu,
				GarantiDurumu = x.IsEmriTeslimler.GarantiDurumu.ToString(),
				ServisTalebi = x.IsEmriTeslimler.ServisTalebi.ToString(),
				FisNo = x.IsEmriTeslimler.FisNo,

				// Teslimat
				KapatmaTarihi = x.IsEmriTeslimler.KapatmaTarihi,
				KapatmaSaati = x.IsEmriTeslimler.KapatmaSaati.HasValue ? x.IsEmriTeslimler.KapatmaSaati.Value.ToString(@"hh\:mm") : "",
				SiparisDurumu = x.IsEmriTeslimler.SiparisDurumu,
				TeslimatAciklama = x.IsEmriTeslimler.TeslimatAciklama,

				// Ödeme
				OdemeSekli = x.IsEmriTeslimler.OdemeSekli,
				AlinanOdeme = x.IsEmriTeslimler.AlinanOdeme,

				// Toplam (her satıra yazılabilir, toplanmış hali)
				ToplamUcret = _context.Islemler
					.Where(i => i.IsEmriTeslimId == id)
					.Sum(i => i.Ucret)
			}).ToList();

			return veri;

		}
	}
}
