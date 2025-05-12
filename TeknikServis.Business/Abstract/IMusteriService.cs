using System.Collections.Generic;
using System.Threading.Tasks;
using TeknikServis.Entities.Servis;

namespace TeknikServis.Business.Abstract
{
	public interface IMusteriService
	{
		Task<IEnumerable<Musteri>> GetRecentAsync(int count = 20);
		Task<Musteri?> GetByIdAsync(int musteriId);
		Task CreateAsync(Musteri musteri);
		Task UpdateAsync(Musteri musteri);
		Task DeleteAsync(int musteriId);
			Task<IEnumerable<Musteri>> GetWithOpenIsEmriInfoAsync(int count = 20);

    }
}