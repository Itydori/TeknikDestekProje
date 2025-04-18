using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeknikServis.Business.Abstract;
using TeknikServis.Business.Response;
using TeknikServis.DataAccess;
using TeknikServis.Entities.Servis;

namespace TeknikServis.Business.Concrete
{
	public class IsEmriService : IIsEmriService
	{
		private readonly IRepository<Musteri> _musteriRepo;
		private readonly IRepository<IsEmriTeslim> _orderRepo;
		private readonly IRepository<Islem> _islemRepo;

		public IsEmriService(
			IRepository<Musteri> musteriRepo,
			IRepository<IsEmriTeslim> orderRepo,
			IRepository<Islem> islemRepo)
		{
			_musteriRepo = musteriRepo;
			_orderRepo = orderRepo;
			_islemRepo = islemRepo;
		}

		// --- Müşteri ---
		public ValueTask<BaseResponse<IEnumerable<Musteri>>> GetRecentCustomersAsync(int count = 20)
		{
			var list = _musteriRepo.Get().OrderByDescending(m => m.MusteriId).Take(count);


			return  ValueTask.FromResult(BaseResponse < IEnumerable < Musteri >>.Successfully(list));	
			
		}

		public async Task<IEnumerable<Musteri>> SearchCustomersAsync(string term)
		{
			var list = _musteriRepo.Get(m => m.Ad.StartsWith(term));
			return await Task.FromResult(list);
		}

		public async Task<Musteri?> GetCustomerByIdAsync(int musteriId)
			=> await Task.FromResult(_musteriRepo.GetById(musteriId));

		// --- İş Emri Teslim ---
		public async Task<IEnumerable<IsEmriTeslim>> GetAllOpenOrdersAsync()
		{
			var list = _orderRepo
        .Get(o => !o.Kapali, includeProperties: "Musteri")
        .OrderByDescending(o => o.GelisTarih)
        .ToList();

		return await Task.FromResult(list);
		}

		public async Task<IEnumerable<IsEmriTeslim>> GetOpenOrdersByCustomerAsync(int musteriId)
		{
			var list = _orderRepo
				.Get(o => o.MusteriId == musteriId && !o.Kapali, includeProperties: "Musteri")
				.OrderByDescending(o => o.GelisTarih)
				.ToList();
			return await Task.FromResult(list);
		}

		public async Task<IEnumerable<IsEmriTeslim>> GetClosedOrdersAsync(int musteriId)
		{
			var list = _orderRepo.Get(o => o.MusteriId == musteriId && o.Kapali)
								 .OrderByDescending(o => o.GelisTarih)
								 .ToList();
			return await Task.FromResult(list);
		}

		public async Task CreateOrderAsync(IsEmriTeslim order)
		{
			order.GelisTarih = DateTime.Now;
			order.Kapali = false;
			order.KapatmaTarihi = null;
			if (string.IsNullOrEmpty(order.FisNo))
				order.FisNo = "FN" + DateTime.Now.ToString("yyyyMMddHHmmss");
			_orderRepo.Create(order);
			await Task.CompletedTask;
		}

		public async Task CloseOrderAsync(
	int isEmriTeslimId,
	DateTime kapanmaGunu,
	TimeSpan kapanmaSaati,
	decimal alinanOdeme,
	string odemeSekli,
	string teslimatAciklama,
	string siparisDurumu)
		{
			Console.WriteLine("📦 [CloseOrderAsync] İş emri kapatma işlemi başlatıldı.");
			Console.WriteLine($"🆔 ID: {isEmriTeslimId}");
			Console.WriteLine($"🕒 Tarih/Saat: {kapanmaGunu.ToShortDateString()} - {kapanmaSaati}");
			Console.WriteLine($"💰 Ödeme: {alinanOdeme} ₺ - Şekil: {odemeSekli}");
			Console.WriteLine($"📦 Durum: {siparisDurumu}");
			Console.WriteLine($"📝 Açıklama: {teslimatAciklama}");

			var order = _orderRepo.GetById(isEmriTeslimId);

			if (order == null)
			{
				Console.WriteLine("❌ HATA: İş emri bulunamadı!");
				throw new InvalidOperationException("İş emri bulunamadı.");
			}

			// Verileri güncelle
			order.KapatmaGunu = kapanmaGunu;
			order.KapatmaSaati = kapanmaSaati;
			order.AlinanOdeme = (int)alinanOdeme;
			order.OdemeSekli = odemeSekli;
			order.TeslimatAciklama = teslimatAciklama;
			order.SiparisDurumu = siparisDurumu;
			order.Kapali = true;

			// Güncelleme işlemi
			_orderRepo.Update(order);
			Console.WriteLine("✅ İş emri başarıyla kapatıldı.");

			await Task.CompletedTask;
		}

		public async Task DeleteOrderAsync(int isEmriTeslimId)
		{
			var order = _orderRepo.GetById(isEmriTeslimId);
			if (order != null) _orderRepo.Delete(order);
			await Task.CompletedTask;
		}
		// --- Açık iş emri ---
		

		// --- İşlem ---
		public async Task<IEnumerable<Islem>> GetOperationsAsync(int isEmriTeslimId)
		{
			var list = _islemRepo.Get(i => i.IsEmriTeslimId == isEmriTeslimId)
								 .OrderByDescending(i => i.IslemId)
								 .ToList();
			return await Task.FromResult(list);
		}
		public async Task<IsEmriTeslim?> GetOrderByIdAsync(int isEmriTeslimId)
		{
			var result = _orderRepo
				.Get(x => x.IsEmriTeslimId == isEmriTeslimId, includeProperties: "Musteri")
				.FirstOrDefault();

			return await Task.FromResult(result);
		}
		public async Task AddOperationAsync(Islem islem)
		{
			_islemRepo.Create(islem);
			await Task.CompletedTask;
		}

		public async Task DeleteOperationAsync(int islemId)
		{
			var i = _islemRepo.GetById(islemId);
			if (i != null) _islemRepo.Delete(i);
			await Task.CompletedTask;
		}
	}
}
