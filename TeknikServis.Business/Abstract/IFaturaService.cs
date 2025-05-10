using System.Threading.Tasks;

namespace TeknikServis.Business.Abstract
{
	public interface IFaturaService
	{
		Task<byte[]> GenerateInvoicePdfAsync(int isEmriTeslimId);
	}
}