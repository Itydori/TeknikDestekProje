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
                NewIsEmri = new IsEmriTeslim { MusteriId = musteriId }
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

                return View("IsEmriOlustur", model);
            }

            await _service.CreateOrderAsync(model.NewIsEmri);
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
            var teslim = await _service.GetOrderByIdAsync(isEmriTeslimId); // bu method varsa

            var vm = new IslemYapViewModel
            {
                YeniIslem = new Islem { IsEmriTeslimId = isEmriTeslimId },
                MevcutIslemler = islemler,
                TeslimBilgisi = teslim
            };

            return View(vm);
        }
        [HttpPost]
        public async Task<IActionResult> IslemKaydet(Islem islem)
        {
            await _service.AddOperationAsync(islem);
            return RedirectToAction(nameof(IslemYap), new { isEmriTeslimId = islem.IsEmriTeslimId });
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