using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeknikServis.Business.Abstract;
using TeknikServis.DataAccess;
using TeknikServis.Entities.Servis;

namespace TeknikServis.Business.Concrete
{
	public class MusteriService : IMusteriService
	{
		private readonly IRepository<Musteri> _repo;

		public MusteriService(IRepository<Musteri> repo)
		{
			_repo = repo;
		}

        private readonly IIslemRepository _islemRepo;

        public MusteriService(IRepository<Musteri> repo, IIslemRepository islemRepo)
        {
            _repo = repo;
            _islemRepo = islemRepo;
        }
        public async Task<IEnumerable<Musteri>> GetRecentAsync(int count = 20)
		{
			var liste = _repo.Get()
							.OrderByDescending(m => m.MusteriId)
							.Take(count)
							.ToList();
			return await Task.FromResult(liste);
		}

		public async Task<Musteri?> GetByIdAsync(int musteriId)
		{
			return await Task.FromResult(_repo.GetById(musteriId));
		}

		public async Task CreateAsync(Musteri musteri)
		{
			musteri.Aktif = true; // default
			_repo.Create(musteri);
			await Task.CompletedTask;
		}
        public async Task<IEnumerable<Musteri>> GetWithOpenIsEmriInfoAsync(int count = 20)
        {
            var musteriler = _repo.Get()
                .OrderByDescending(m => m.MusteriId)
                .Take(count)
                .ToList();

            foreach (var musteri in musteriler)
            {
                var acikIsSayisi = _islemRepo
    .Get()
    .Count(x =>
        x.IsEmriTeslimler != null &&
        x.IsEmriTeslimler.MusteriId == musteri.MusteriId &&
        x.IsEmriTeslimler.KapatmaGunu == null
    );

                musteri.AcikIsEmriSayisi = acikIsSayisi;
            }

            return await Task.FromResult(musteriler);
        }
        public async Task UpdateAsync(Musteri musteri)
		{
			var db = _repo.GetById(musteri.MusteriId);
			if (db is null) return;
			db.Ad = musteri.Ad;
			db.Telefon = musteri.Telefon;
			db.Eposta = musteri.Eposta;
			db.Adres = musteri.Adres;
			_repo.Update(db);
			await Task.CompletedTask;
		}

		public async Task DeleteAsync(int musteriId)
		{
			var db = _repo.GetById(musteriId);
			if (db is null) return;
			_repo.Delete(db);
			await Task.CompletedTask;
		}
    }
}
