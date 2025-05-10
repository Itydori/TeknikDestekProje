using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AspNetCore.Reporting;             // LocalReport & RenderType
  // IWebHostEnvironment
using TeknikServis.Business.Abstract;
using TeknikServis.DataAccess;
using teknikServis.Entities.Fatura;
using Microsoft.AspNetCore.Hosting;

namespace TeknikServis.Business.Concrete
{
	public class FaturaService : IFaturaService
	{
		private readonly IIslemRepository _islemRepository;
		private readonly IWebHostEnvironment _env;

		public FaturaService(IIslemRepository islemRepository, IWebHostEnvironment env)
		{
			_islemRepository = islemRepository;
			_env = env;
			Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
		}

		public async Task<byte[]> GenerateInvoicePdfAsync(int id)
		{
			var data = _islemRepository.GetAllReport(id);
			if (!data.Any())
				return Array.Empty<byte>();

			var rdlcPath = Path.Combine(_env.ContentRootPath, "Rapor", "Fatura.rdlc");
			var report = new LocalReport(rdlcPath);
			report.AddDataSource("FaturaDataset", data);

			// Pozisyonel argümanlarla çağrı
			var result = report.Execute(RenderType.Pdf, 1, null, "");
			return await Task.FromResult(result.MainStream);
		}
	}
}
