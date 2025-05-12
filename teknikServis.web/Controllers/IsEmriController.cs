using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Core.Types;
using TeknikServis.Business.Abstract;
using TeknikServis.Entities.Servis;
using System.Linq;
using TeknikServis.DataAccess;
using System.Collections.Generic;
using TeknikServis.Web.Models;
using TeknikServis.Business;
using Microsoft.AspNetCore.Authorization;

namespace teknikServis.web.Controllers
{
	[Authorize]
	public class IsEmriController : Controller
	{
		private readonly IIsEmriService _service;
		public IsEmriController(IIsEmriService service) => _service = service;
		public async ValueTask<IActionResult> Index(string ara)
		{
			IEnumerable<Musteri> model;
			if (string.IsNullOrWhiteSpace(ara))
				model = _service.GetRecentCustomersAsync(20).Result.Data;
			else
				model = await _service.SearchCustomersAsync(ara);
			return View(model);
		}
		public async Task<IActionResult> IsEmriOlustur(int musteriId)
		{
			var musteri = await _service.GetCustomerByIdAsync(musteriId);
			if (musteri == null) return RedirectToAction(nameof(AcikIsEmirleri));

			var vm = new IsEmriOlusturViewModel
			{
				Musteri = musteri,
				AcikIsEmirleri = await _service.GetOpenOrdersByCustomerAsync(musteriId),
				KapaliIsEmirleri = await _service.GetClosedOrdersAsync(musteriId),
				NewIsEmri = new IsEmriTeslim { MusteriId = musteriId, GelisTarih = DateTime.Today }
			};

			return View(vm);
		}
		[HttpPost]
		public async Task<IActionResult> IsEmriKaydet(IsEmriOlusturViewModel model)
		{
			if (!ModelState.IsValid)
			{
				model.Musteri = await _service.GetCustomerByIdAsync(model.NewIsEmri.MusteriId);
				model.AcikIsEmirleri = await _service.GetOpenOrdersByCustomerAsync(model.NewIsEmri.MusteriId);
				model.KapaliIsEmirleri = await _service.GetClosedOrdersAsync(model.NewIsEmri.MusteriId);
				await _service.CreateOrderAsync(model.NewIsEmri);
				return RedirectToAction(nameof(IsEmriOlustur), new { musteriId = model.Musteri.MusteriId });
			}


			return RedirectToAction(nameof(IsEmriOlustur), new { musteriId = model.NewIsEmri.MusteriId });
		}
		public async Task<IActionResult> AcikIsEmirleri()
		{
			var tumAC = await _service.GetAllOpenOrdersAsync();
			return View(tumAC);  // Model: IEnumerable<IsEmriTeslim>
		}
		public async Task<IActionResult> IslemYap(int isEmriTeslimId)
		{
			var islemler = await _service.GetOperationsAsync(isEmriTeslimId);
			var teslim = await _service.GetOrderByIdAsync(isEmriTeslimId);

			var vm = new IslemYapViewModel
			{
				YeniIslem = new Islem
				{
					IsEmriTeslimId = isEmriTeslimId,
					OnarimTarihi = DateTime.Today // 👈 BURAYA EKLEDİK!
				},
				MevcutIslemler = islemler,

				
				TeslimBilgisi = teslim
			};

			ViewBag.IsEmriTeslimId = isEmriTeslimId;

			return View(vm);
		}
		[HttpPost("/IsEmri/IslemKaydet")]
		[ActionName("IslemKaydet")]
		public async Task<IActionResult> IslemKaydetPost(IslemYapViewModel model)
		{
			Console.WriteLine("🔥 FORM POST GELDİ");

			if (!ModelState.IsValid)
			{
				Console.WriteLine("❌ ModelState geçersiz!");

				var errors = ModelState.AddModelStateExtension();

				//ModelState.AddModelStateExtension();


				foreach (var error in ModelState)
				{
					foreach (var e in error.Value.Errors)
					{
						Console.WriteLine($"🔴 {error.Key}: {e.ErrorMessage}");
					}
				}

				model.MevcutIslemler = await _service.GetOperationsAsync(model.YeniIslem.IsEmriTeslimId);
				model.TeslimBilgisi = await _service.GetOrderByIdAsync(model.YeniIslem.IsEmriTeslimId);
				return View("IslemYap", model);
			}

			Console.WriteLine("✅ ModelState OK. Kayıt ediliyor...");
			await _service.AddOperationAsync(model.YeniIslem);
			return RedirectToAction(nameof(IslemYap), new { isEmriTeslimId = model.YeniIslem.IsEmriTeslimId });
		}
		[HttpPost]
		public async Task<IActionResult> Delete(int islemId, int isEmriTeslimId)
		{
			await _service.DeleteOperationAsync(islemId);
			return RedirectToAction(nameof(IslemYap), new { isEmriTeslimId });
		}
		//[HttpPost]
		//[ValidateAntiForgeryToken]
		//public async Task<IActionResult> IsEmriKapat(IslemYapViewModel model)
		//{
		//	Console.WriteLine("🚀 [IsEmriKapat] POST edildi.");
		//	Console.WriteLine($"📦 Teslim ID: {model.YeniTeslimBilgisi?.IsEmriTeslimId}");
		//	Console.WriteLine($"💰 Alınan Ödeme: {model.YeniTeslimBilgisi?.AlinanOdeme}");
		//	Console.WriteLine($"🕒 Tarih/Saat:  {model.YeniTeslimBilgisi?.KapatmaGunu:d} {model.YeniTeslimBilgisi?.KapatmaSaati}");
		//	Console.WriteLine($"📦 Sipariş Durumu: {model.YeniTeslimBilgisi?.SiparisDurumu}");

		//	// 1) Basit validasyon: ID gelmemişse devam etmeyelim
		//	if (model.YeniTeslimBilgisi?.IsEmriTeslimId is null or 0)
		//		return BadRequest("Teslim ID gelmedi.");
		//	// kaydetme islemi ekle 
		//	if (!ModelState.IsValid)
		//	{
		//		Console.WriteLine("❌ ModelState geçersiz!");
		//		// Hataları loglayıp sayfayı geri döndür.
		//		model.MevcutIslemler = await _service.GetOperationsAsync(model.YeniTeslimBilgisi.IsEmriTeslimId);
		//		model.TeslimBilgisi = await _service.GetOrderByIdAsync(model.YeniTeslimBilgisi.IsEmriTeslimId);
		//		return View("IslemYap", model);
		//	}

		//	Console.WriteLine("✅ ModelState OK → Servise devrediliyor...");

		//	await _service.CloseOrderAsync(
		//		model.YeniTeslimBilgisi.IsEmriTeslimId,
		//		model.YeniTeslimBilgisi.KapatmaGunu,
		//		model.YeniTeslimBilgisi.KapatmaSaati,
		//		model.YeniTeslimBilgisi.AlinanOdeme,
		//		model.YeniTeslimBilgisi.OdemeSekli,
		//		model.YeniTeslimBilgisi.TeslimatAciklama,
		//		model.YeniTeslimBilgisi.SiparisDurumu);

		//	Console.WriteLine("✅ Güncelleme tamamlandı.");
		//	return RedirectToAction(nameof(AcikIsEmirleri));
		//}
		// Controllers/IsEmriController.cs
[HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> IsEmriKapat(IslemYapViewModel model)
{
			ModelState.Remove("YeniIslem.OnarimYapan");
			ModelState.Remove("YeniIslem.OnarimTarihi");
			ModelState.Remove("YeniIslem.StokYeri");
			ModelState.Remove("YeniIslem.YapilanIslemler");
			ModelState.Remove("YeniIslem.Ucret");
			ModelState.Remove("YeniIslem.Aciklama");
			Console.WriteLine("🚀 [IsEmriKapat] POST başladı.");
    // ModelState kontrolü
    if (!ModelState.IsValid)
    {
        Console.WriteLine("❌ ModelState geçersiz!");
        foreach (var kv in ModelState)
            foreach (var err in kv.Value.Errors)
                Console.WriteLine($"🔴 {kv.Key}: {err.ErrorMessage}");
        return View("IslemYap", model);
    }

    try
    {
        // Service metodunu çağır
        await _service.CloseOrderAsync(
            model.YeniTeslimBilgisi.IsEmriTeslimId,
            model.YeniTeslimBilgisi.KapatmaGunu,
            model.YeniTeslimBilgisi.KapatmaSaati,
            model.YeniTeslimBilgisi.AlinanOdeme,
            model.YeniTeslimBilgisi.OdemeSekli,
            model.YeniTeslimBilgisi.TeslimatAciklama,
            model.YeniTeslimBilgisi.SiparisDurumu
        );

        // Başarı mesajı ve yönlendirme
        TempData["Success"] = "İş emri başarıyla kapatıldı.";
        return RedirectToAction("IslemYap", new { MusteriId = model.YeniTeslimBilgisi.IsEmriTeslimId });
    }
    catch (InvalidOperationException ex)
    {
        Console.WriteLine($"❌ Serviste hata: {ex.Message}");
        ModelState.AddModelError("", "Kapanma işlemi başarısız: " + ex.Message);
        return View("IslemYap", model);
    }
    catch (Exception ex)
    {
				Console.WriteLine($"❌ Beklenmedik hata: {ex}");
				ModelState.AddModelError("", "Beklenmedik bir hata oluştu.");
        return View("IslemYap", model);
    }
}



		[HttpPost]
		public async Task<IActionResult> IsEmriSil(int id)
		{
			await _service.DeleteOrderAsync(id);
			return RedirectToAction(nameof(AcikIsEmirleri));
		}
	}
}