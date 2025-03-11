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

        public IsEmriController(IRepository<Musteri> repository)
        {
            this.repository = repository;
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

            return View();
        }
	}
}
