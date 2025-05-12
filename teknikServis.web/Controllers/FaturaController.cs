using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TeknikServis.DataAccess;
using System.IO;
using Microsoft.Reporting.NETCore;
using teknikServis.Entities.Fatura;
using TeknikServis.Business.Abstract;
using AspNetCore.Reporting;
using System.Text;
using Microsoft.AspNetCore.Authorization;

[Authorize]
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

		System.Diagnostics.Debug.WriteLine("Gelen ID: " + id);
		var veri = _islemRepository.GetAllReport(id);

		if (!veri.Any())
			return Content("Rapor verisi bulunamadı.");

		var header = veri.First();
		string musteriAdi = header.Ad;      // viewmodel’ında varsa
		string cihaz = header.Marka.Replace(" ", "_");
		string model = header.Marka.Replace(" ", "_") + "_" + header.Model.Replace(" ", "_");
		string tarih = DateTime.Now.ToString("dd/MM/yyyy");
		string fileName = $"{musteriAdi}_{cihaz}_{model}_{tarih}.pdf";
		

		// Raporu oluşturmak için gerekli olan dosya yolu
		string path = Path.Combine(Directory.GetCurrentDirectory(), "Rapor", "Fatura.rdlc");
		var rapor = new AspNetCore.Reporting.LocalReport(path);
		rapor.AddDataSource("FaturaDataset", veri);


		rapor.AddDataSource("FaturaDataset", veri); // Dataset adı .rdlc dosyasındakiyle birebir aynı

		var result = rapor.Execute(RenderType.Pdf, 1, null, "");

		return File(result.MainStream, "application/pdf", fileName);
	}
}
