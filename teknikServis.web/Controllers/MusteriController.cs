using Microsoft.AspNetCore.Mvc;
using TeknikServis.Business.Abstract;
using TeknikServis.Entities.Servis;

namespace teknikServis.web.Controllers
{
	public class MusteriController : Controller
	{
		private readonly IRepository<Musteri> repository;

		public MusteriController(IRepository<Musteri> repository)
		{
			this.repository = repository;
		}
		public IActionResult Index()
		{
			var Musteri = repository.Get();
			if (Musteri == null || !Musteri.Any())
			{
				Console.WriteLine("Veri bulunamadı.");
			}
			return View(repository.Get().OrderByDescending(i => i.MusteriId).Take(20).ToList());
		}

		public IActionResult Create()
		{
			return View();
		}
		[HttpPost]
		public IActionResult Create(Musteri musteri)
		{
			try
			{
				if (ModelState.IsValid)
				{
					// Set default values for properties not included in the form
					musteri.Aktif = true; // Set to true by default

					repository.Create(musteri);
					return RedirectToAction("Index");
				}
				// If we get here, something failed, redisplay form
				return View(musteri);
			}
			catch (Exception ex)
			{
				// Log the exception
				ModelState.AddModelError("", "Kayıt sırasında bir hata oluştu: " + ex.Message);
				return View(musteri);
			}
		}
		public IActionResult Edit(int MusteriId)
		{
			var musteri = repository.GetById(MusteriId);
			if (musteri == null)
			{
				return NotFound(); // Eğer müşteri bulunamazsa hata döndür
			}
			return View(musteri);
		}
		[HttpPost]
		public IActionResult Update(Musteri musteri)
		{
			var edit = repository.GetById(musteri.MusteriId);
			edit.Adres=musteri.Adres;
			edit.Eposta=musteri.Eposta;
			edit.Telefon=musteri.Telefon;
			edit.Ad=musteri.Ad;
			repository.Update(edit);
			TempData["Ok"] = edit.Ad + " isimli müşteri başarıyla güncellendi";
			return RedirectToAction("Index");
		}
		public IActionResult Delete(int MusteriId)
		{
			var musteri = repository.GetById(MusteriId);
			if (musteri == null)
			{
				return NotFound();
			}
			repository.Delete(musteri);
			TempData["Ok"] = musteri.Ad + " isimli müşteri başarıyla silindi";
			return RedirectToAction("Index");
		}
		[HttpPost]
		public IActionResult Delete(Musteri musteri)
		{
			repository.Delete(musteri);
			return RedirectToAction("Index");
		}
	}
}
