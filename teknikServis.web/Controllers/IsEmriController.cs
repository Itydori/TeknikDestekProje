using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Core.Types;
using TeknikServis.Business.Abstract;
using TeknikServis.Entities.Servis;
using System.Linq;
using TeknikServis.DataAccess;
using System.Collections.Generic;
using TeknikServis.Web.Models;

namespace teknikServis.web.Controllers
{
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
		[HttpPost]
		public async Task<IActionResult> IsEmriKapat(
	  int isEmriTeslimId,
	  string OdemeSekli,
	  string TeslimatAciklama,
	  decimal AlinanOdeme,
	  string KapatmaSaati,
	  DateTime KapatmaGunu,
	  string SiparisDurumu)
		{
			await _service.CloseOrderAsync(
				isEmriTeslimId,
				KapatmaGunu,
				TimeSpan.Parse(KapatmaSaati),
				AlinanOdeme,
				OdemeSekli,
				TeslimatAciklama,
				SiparisDurumu


			);

			return RedirectToAction(nameof(AcikIsEmirleri));
		}
		[HttpPost]
		public async Task<IActionResult> IsEmriSil(int id)
		{
			await _service.DeleteOrderAsync(id);
			return RedirectToAction(nameof(AcikIsEmirleri));
		}
	}
}