using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Core.Types;
using TeknikServis.Business.Abstract;
using TeknikServis.Entities.Servis;
using System.Linq;
using TeknikServis.DataAccess;

namespace teknikServis.web.Controllers
{
    public class IsEmriController : Controller
    {
        private readonly IRepository<Musteri> repository;
        private readonly IRepository<IsEmriTeslim> isEmriTeslimRepository;
        private readonly IRepository<İslem> isEmriIslemRepository;
		public IsEmriController(IRepository<Musteri> repository, IRepository<IsEmriTeslim> isEmriTeslimRepository, IRepository<İslem> isEmriIslemRepository)
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
            var musteri = repository.GetById(MusteriId);
            TempData["MusteriId"] = MusteriId;
            ViewBag.MusteriId = MusteriId;
            ViewBag.AcikIsEmirleri = isEmriTeslimRepository.Get(i => i.MusteriId == MusteriId && i.Kapali == false).ToList();
            ViewBag.Title = "İş Emri Oluştur -" + musteri.Ad;
			return View(isEmriTeslimRepository.Get(i => i.Kapali == true && i.MusteriId == MusteriId).OrderByDescending(i=> i.GelisTarih).ToList());
        }

        [HttpPost]
        public IActionResult IsEmriKaydet(IsEmriTeslim isEmriTeslim)
		{
            var deger = TempData["MusteriId"];
            isEmriTeslim.MusteriId = (int)deger;

			isEmriTeslimRepository.Create(isEmriTeslim);
			return RedirectToAction("AcikIsEmirleri");
		}
        public IActionResult AcikIsEmirleri()
        {
            return View(isEmriTeslimRepository.Get(i => i.Kapali == false,includeProperties:"Musteri").ToList());
        }
        public IActionResult IslemYap(int isEmriTeslim)
        {
            var baslik = isEmriTeslimRepository.Get(x => x.IsEmriTeslimId == isEmriTeslim,includeProperties:"Musteri").FirstOrDefault();

			//if (baslik == null || baslik.Musteri == null)
			//{
			//	// Redirect to AcikIsEmirleri if data is null
			//	return RedirectToAction("AcikIsEmirleri");
			//}
			ViewBag.Title = "İş Emri İşlem Yap -" + baslik.Musteri.Ad +" " + baslik.Marka + " " + baslik.Model + " " + baslik.GelisTarih.ToString("dd/MM/yyyy") + " " + baslik.FisNo;
			ViewBag.IsEmriTeslimId = isEmriTeslim;
			return View(isEmriIslemRepository.Get(x=>x.IsEmriId== isEmriTeslim).OrderByDescending(x=>x.IslemId).ToList());
        }
        public IActionResult IslemKaydet(İslem islem)
        {
			// IsEmriTeslimId değerinin işlem nesnesine aktarılması

			islem.IsEmriTeslimId = islem.IsEmriId;
			isEmriIslemRepository.Create(islem);
			return RedirectToAction(nameof(IslemYap),new { isEmriTeslim = islem.IsEmriId });
        }

		[HttpGet]
		public IActionResult Delete(int isEmriTeslimId)
		{
			var isEmriTeslim = repository.GetById(isEmriTeslimId);
			if (isEmriTeslim == null)
			{
				return NotFound();
			}

			repository.Delete(isEmriTeslim);
			TempData["Ok"] = "İş Emri başarıyla silindi.";
			return View(isEmriTeslim);
		}
	}
}
