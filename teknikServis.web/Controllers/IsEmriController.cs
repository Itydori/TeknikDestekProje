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

		public IsEmriController(IRepository<Musteri> repository, IRepository<IsEmriTeslim> isEmriTeslimRepository)
		{
			this.repository = repository;
			this.isEmriTeslimRepository = isEmriTeslimRepository;
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
            TempData["MusteriId"] = MusteriId;
            return View();
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
	}
}
