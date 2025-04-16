using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TeknikServis.DataAccess;
using System.IO;
using Microsoft.Reporting.NETCore;
using teknikServis.Entities.Fatura;
using TeknikServis.Business.Abstract;
using AspNetCore.Reporting;
using System.Text;


public class FaturaController : Controller
{
	private readonly IIslemRepository _islemRepository;
	private readonly TeknikServisDbContext _context;

	public FaturaController(IIslemRepository islemRepository, TeknikServisDbContext context)
	{
		_islemRepository = islemRepository;
		_context = context;

		// Encoding ayarı constructor'da da olabilir
		Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
	}

	public IActionResult Index()
	{
		return View();
	}

	public IActionResult FaturaYazdir(int id)
	{
		
		Console.WriteLine("Gelen ID: " + id);
		var veri = _islemRepository.GetAllReport(id);

		if (!veri.Any())
			return Content("Rapor verisi bulunamadı.");

		string path = Path.Combine(Directory.GetCurrentDirectory(), "Rapor", "Fatura.rdlc");
		AspNetCore.Reporting.LocalReport rapor = new AspNetCore.Reporting.LocalReport(path);

		rapor.AddDataSource("FaturaDataset", veri); // Dataset adı .rdlc dosyasındakiyle birebir aynı

		var result = rapor.Execute(RenderType.Pdf, 1, null, "");

		return File(result.MainStream, "application/pdf", "fatura.pdf");
	}
}
