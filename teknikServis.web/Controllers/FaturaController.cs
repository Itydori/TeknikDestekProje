using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TeknikServis.DataAccess;
using System.IO;
using Microsoft.Reporting.NETCore;
using teknikServis.Entities.Fatura;


namespace teknikServis.web.Controllers
{
	public class FaturaController : Controller
	{
		private readonly TeknikServisDbContext _context; // 🔥 bu tam burada olacak

		// 💉 Dependency Injection burada
		public FaturaController(TeknikServisDbContext context)
		{
			_context = context;
		}

		public IActionResult Index()
		{
			return View();
		}

		public IActionResult FaturaYazdir(int id)
		{
			var veri = _context.Islemler
				.Include(x => x.IsEmriTeslim)
				.ThenInclude(x => x.Musteri)
				.Where(x => x.IsEmriTeslimId == id)
				.Select(x => new IslemRaporViewModel
				{
					YapilanIslemler = x.YapilanIslemler,
					Ucret = x.Ucret,
					OnarimYapan = x.OnarimYapan,
					OnarimTarihi = x.OnarimTarihi,
					StokYeri = x.StokYeri,
					Aciklama = x.Aciklama,
					Ad = x.IsEmriTeslim.Musteri.Ad,
					TeslimTarihi = x.IsEmriTeslim.KapatmaTarihi
				}).ToList();

			var rapor = new LocalReport();
			rapor.ReportPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Rapor", "Fatura.rdlc"); // Yol doğruysa burası

			rapor.DataSources.Add(new ReportDataSource("FaturaDataSet", veri)); // Dataset adı RDLC'deki ile aynı olmalı

			var sonuc = rapor.Render("PDF");

			return File(sonuc, "application/pdf", "fatura.pdf");
		}

	}
}
