﻿using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Core.Types;
using TeknikServis.Business.Abstract;
using TeknikServis.Entities.Servis;
using System.Linq;
using TeknikServis.DataAccess;
using System.Collections.Generic;

namespace teknikServis.web.Controllers
{
	public class IsEmriController : Controller
	{
		private readonly IRepository<Musteri> repository;
		private readonly IRepository<IsEmriTeslim> isEmriTeslimRepository;
		private readonly IRepository<Islem> isEmriIslemRepository;
		public IsEmriController(IRepository<Musteri> repository, IRepository<IsEmriTeslim> isEmriTeslimRepository, IRepository<Islem> isEmriIslemRepository)
		{
			this.repository = repository;
			this.isEmriTeslimRepository = isEmriTeslimRepository;
			this.isEmriIslemRepository = isEmriIslemRepository;
		}
		public IActionResult Index(string ara)
		{
			if (ara == "" || ara == null)
			{

				var musteri = repository.Get().OrderByDescending(i => i.MusteriId).Take(20).ToList();
				return View(musteri);
			}
			var musteriAra = repository.Get(i => i.Ad.StartsWith(ara)).ToList();
			return View(musteriAra);
		}
		public IActionResult IsEmriOlustur(int MusteriId)
		{
			try
			{
				var musteri = repository.GetById(MusteriId);
				if (musteri == null)
				{
					return RedirectToAction("Index");
				}

				TempData["MusteriId"] = MusteriId;
				ViewBag.MusteriId = MusteriId;

				try
				{
					// Açık iş emirlerini getir
					ViewBag.AcikIsEmirleri = isEmriTeslimRepository.Get(
						i => i.MusteriId == MusteriId && i.Kapali == false).ToList();
				}
				catch (Exception ex)
				{
					// Hata durumunda boş liste kullan
					ViewBag.AcikIsEmirleri = new List<IsEmriTeslim>();
				}

				ViewBag.Title = "İş Emri Oluştur -" + musteri.Ad;

				try
				{
					// Kapalı iş emirlerini getir ve view'e gönder
					return View(isEmriTeslimRepository.Get(
						i => i.Kapali == true && i.MusteriId == MusteriId)
						.OrderByDescending(i => i.GelisTarih)
						.ToList());
				}
				catch (Exception ex)
				{
					// Hata durumunda boş liste gönder
					return View(new List<IsEmriTeslim>());
				}
			}
			catch (Exception ex)
			{
				return RedirectToAction("Index");
			}
		}

		[HttpPost]
		public IActionResult IsEmriKaydet(IsEmriTeslim isEmriTeslim)
		{
			try
			{
				var deger = TempData["MusteriId"];
				if (deger == null)
				{
					return RedirectToAction("Index");
				}

				isEmriTeslim.MusteriId = Convert.ToInt32(deger);
				isEmriTeslim.GelisTarih = DateTime.Now;
				isEmriTeslim.KapatmaTarihi = null; // Nullable olduğu için null atıyoruz
				isEmriTeslim.Kapali = false;

				// Diğer gerekli alanları doldur
				if (string.IsNullOrEmpty(isEmriTeslim.Marka))
					isEmriTeslim.Marka = "Belirtilmedi";

				if (string.IsNullOrEmpty(isEmriTeslim.Model))
					isEmriTeslim.Model = "Belirtilmedi";

				if (string.IsNullOrEmpty(isEmriTeslim.ArizaDurumu))
					isEmriTeslim.ArizaDurumu = "Belirtilmedi";

				if (string.IsNullOrEmpty(isEmriTeslim.FisNo))
					isEmriTeslim.FisNo = "FN" + DateTime.Now.ToString("yyyyMMddHHmmss");

				isEmriTeslimRepository.Create(isEmriTeslim);
				return RedirectToAction("AcikIsEmirleri");
			}
			catch (Exception ex)
			{
				// Hata mesajını TempData ile sakla
				TempData["Hata"] = "İş emri kaydedilirken hata oluştu: " + ex.Message;
				return RedirectToAction("IsEmriOlustur", new { MusteriId = isEmriTeslim.MusteriId });
			}
		}
		public IActionResult AcikIsEmirleri()
		{
			try
			{
				// Sorgunuzun direkt olarak ihtiyaç duyduğunuz alanları seçtiğinden emin olun


				var acikIsEmirleri1 = isEmriTeslimRepository.Get(null, null, "Musteri").ToList();
				var acikIsEmirleri = isEmriTeslimRepository.Get()
					// Kapalı olmayan iş emirlerini filtreleyin
					.Where(i => i.Kapali == false)
					// Musteri ilişkisini include edin

					.ToList();

				// Verileri doğrudan görünüme gönderin
				return View(acikIsEmirleri);
			}
			catch (Exception ex)
			{
				// Hata yönetimi
				// Örneğin: TempData["Error"] = ex.Message;
				return View(new List<IsEmriTeslim>()); // Boş liste dönün
			}
			//return View(isEmriTeslimRepository.Get(i => i.Kapali == false, includeProperties: "Musteri").ToList());
		}
		public IActionResult IslemYap(int isEmriTeslim)
		{
			//var baslik = isEmriTeslimRepository.Get(x => x.IsEmriTeslimId == isEmriTeslim, includeProperties: "Musteri").FirstOrDefault();

			////if (baslik == null || baslik.Musteri == null)
			////{
			////	// Redirect to AcikIsEmirleri if data is null
			////	return RedirectToAction("AcikIsEmirleri");
			////}
			//ViewBag.Title = "İş Emri İşlem Yap -" + baslik.Musteri.Ad + " " + baslik.Marka + " " + baslik.Model + " " + baslik.GelisTarih.ToString("dd/MM/yyyy") + " " + baslik.FisNo;
			//ViewBag.IsEmriTeslimId = isEmriTeslim;
			//return View(isEmriIslemRepository.Get(x => x.IsEmriId == isEmriTeslim).OrderByDescending(x => x.IslemId).ToList());
			var baslik = isEmriTeslimRepository.Get(x => x.IsEmriTeslimId == isEmriTeslim, includeProperties: "Musteri").FirstOrDefault();

			if (baslik == null || baslik.Musteri == null)
			{
				// Redirect to AcikIsEmirleri if data is null
				return RedirectToAction("AcikIsEmirleri");
			}

			ViewBag.Title = "İş Emri İşlem Yap -" + baslik.Musteri.Ad + " " + baslik.Marka + " " + baslik.Model + " " + baslik.GelisTarih.ToString("dd/MM/yyyy") + " " + baslik.FisNo;
			ViewBag.IsEmriTeslimId = isEmriTeslim;
			return View(isEmriIslemRepository.Get(x => x.IsEmriId == isEmriTeslim).OrderByDescending(x => x.IslemId).ToList());

		}
		public IActionResult IslemKaydet(Islem islem)
		{
			//// IsEmriTeslimId değerinin işlem nesnesine aktarılması

			//islem.IsEmriTeslimId = islem.IsEmriId;
			//isEmriIslemRepository.Create(islem);
			//return RedirectToAction(nameof(IslemYap), new { isEmriTeslim = islem.IsEmriId });
			if (islem.IsEmriId <= 0)
			{
				// Handle invalid IsEmriId
				return RedirectToAction("AcikIsEmirleri");
			}

			isEmriIslemRepository.Create(islem);
			return RedirectToAction(nameof(IslemYap), new { isEmriTeslim = islem.IsEmriId });
		}

		[HttpPost]
		public IActionResult Delete(int islemId)
		{
			var islem = isEmriIslemRepository.GetById(islemId); // Burada doğru repository olmalı
			if (islem == null)
			{
				return NotFound();
			}

			var isEmriId = islem.IsEmriId; // Geri dönüşte aynı sayfaya gidebilmek için saklıyoruz

			isEmriIslemRepository.Delete(islem);
			TempData["Ok"] = "İşlem başarıyla silindi.";

			return RedirectToAction(nameof(IslemYap), new { isEmriTeslim = isEmriId });
		}
		//[HttpGet]
		//public IActionResult Delete(int isEmriTeslimId)
		//{
		//	var isEmriTeslim = isEmriIslemRepository.GetById(isEmriTeslimId);
		//	if (isEmriTeslim == null)
		//	{
		//		return NotFound();
		//	}

		//	// Silmeden önce IsEmriId değerini saklayın
		//	var isEmriId = isEmriTeslim.IsEmriId;

		//	isEmriIslemRepository.Delete(isEmriTeslim);
		//	TempData["Ok"] = "İş Emri başarıyla silindi.";

		//	// Parametre adı "isEmriTeslim" olmalı, "isEmriId" değil
		//	return RedirectToAction(nameof(IslemYap), new { isEmriTeslim = isEmriId });
		//}
		//public IActionResult IsEmriKapat(int isEmriTeslimId,string OdemeSekli,string TeslimatAciklama,int AlinanOdeme,string KapatmaSaati, DateTime KapatmaGunu)
		//{
		//	var isEmri = isEmriTeslimRepository.GetById(isEmriTeslimId);
		//	if (isEmri == null)
		//	{
		//		return NotFound();
		//	}
		//	// Kapatma işlemi için gerekli alanları güncelleyin
		//	isEmri.KapatmaGunu = KapatmaGunu;
		//	isEmri.KapatmaSaati = TimeSpan.Parse(KapatmaSaati);
		//	isEmri.AlinanOdeme = AlinanOdeme;
		//	isEmri.OdemeSekli = OdemeSekli;
		//	isEmri.KapatmaTarihi = isEmri.KapatmaGunu + isEmri.KapatmaSaati;
		//	isEmri.TeslimatAciklama = TeslimatAciklama;
		//	isEmri.Kapali = true;
		//	isEmri.IsEmriTeslimId = isEmriTeslimId;
		//	isEmriTeslimRepository.Update(isEmri);
		//	TempData["Ok"] = "İş Emri başarıyla kapatıldı.";
		//	return RedirectToAction(nameof(AcikIsEmirleri));
		//}
		public IActionResult IsEmriKapat(int isEmriTeslimId, string OdemeSekli, string TeslimatAciklama,
	decimal AlinanOdeme, string KapatmaSaati, DateTime KapatmaGunu, string SiparisDurumu)
		{
			var isEmri = isEmriTeslimRepository.GetById(isEmriTeslimId);
			if (isEmri == null)
			{
				return NotFound();
			}

			// Kapatma işlemi için gerekli alanları güncelleyin
			isEmri.KapatmaGunu = KapatmaGunu;
			isEmri.KapatmaSaati = TimeSpan.Parse(KapatmaSaati);
			isEmri.AlinanOdeme = Convert.ToInt32(AlinanOdeme);
			isEmri.OdemeSekli = OdemeSekli;
			isEmri.SiparisDurumu = SiparisDurumu; // Make sure to add this field to your model
			//isEmri.KapatmaTarihi =  KapatmaGunu + isEmri.KapatmaSaati;
			isEmri.TeslimatAciklama = TeslimatAciklama;
			isEmri.Kapali = true;

			isEmriTeslimRepository.Update(isEmri);
			TempData["Ok"] = "İş Emri başarıyla kapatıldı.";
			return RedirectToAction(nameof(AcikIsEmirleri));
		}

        [HttpPost]
        public IActionResult IsEmriSil(int id)
        {
            try
            {
                var isEmri = isEmriTeslimRepository.GetById(id);
                if (isEmri == null)
                {
                    TempData["Hata"] = "İş emri bulunamadı.";
                    return RedirectToAction(nameof(AcikIsEmirleri));
                }

                isEmriTeslimRepository.Delete(isEmri);
                TempData["Ok"] = "İş emri başarıyla silindi.";
            }
            catch (Exception ex)
            {
                TempData["Hata"] = "İş emri silinirken bir hata oluştu: " + ex.Message;
            }

            return RedirectToAction(nameof(AcikIsEmirleri));
        }
		


	}
}

