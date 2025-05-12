using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TeknikServis.Business.Response;
using TeknikServis.Entities.Servis;


namespace TeknikServis.Business.Abstract
{
	public interface IIsEmriService
	{
		// Müşteri
		ValueTask<BaseResponse<IEnumerable<Musteri>>> GetRecentCustomersAsync(int count = 20);
		Task<IEnumerable<Musteri>> SearchCustomersAsync(string term);
		Task<Musteri?> GetCustomerByIdAsync(int musteriId);

		// İş Emri Teslim
		// Business/Abstract/IIsEmriService.cs
		Task<IEnumerable<IsEmriTeslim>> GetAllOpenOrdersAsync();
		Task<IEnumerable<IsEmriTeslim>> GetOpenOrdersByCustomerAsync(int musteriId);


        Task<IEnumerable<IsEmriTeslim>> GetClosedOrdersAsync(int musteriId);
		Task CreateOrderAsync(IsEmriTeslim order);
		Task CloseOrderAsync(
			int isEmriTeslimId, DateTime kapanmaGunu, TimeSpan kapanmaSaati,
			decimal alinanOdeme, string odemeSekli, string teslimatAciklama, string siparisDurumu);
		Task DeleteOrderAsync(int isEmriTeslimId);

		// İşlem (Islem)
		Task<IEnumerable<Islem>> GetOperationsAsync(int isEmriTeslimId);
		Task AddOperationAsync(Islem islem);
		Task DeleteOperationAsync(int islemId);
		Task<IsEmriTeslim?> GetOrderByIdAsync(int isEmriTeslimId);

	}
}