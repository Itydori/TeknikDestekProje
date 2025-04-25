using Microsoft.AspNetCore.Mvc;
using teknikServis.web.Models;

namespace teknikServis.web.Controllers
{
    public class ElasticController : Controller
    {
        private readonly IslemIndexService _indexService;

        public ElasticController(IslemIndexService indexService)
        {
            _indexService = indexService;
        }

        public async Task<IActionResult> Indexle()
        {
            try
            {
                await _indexService.IndexAllAsync();
                return Content("Indexleme tamamlandı!");
            }
            catch (Exception ex)
            {
                return Content("HATA OLUŞTU:\n" + ex.Message + "\n\n" + ex.StackTrace);
            }
        }
        public async Task<IActionResult> TestElastic()
        {
            var docs = await _indexService.TestElasticSearchAsync();
            return Json(docs);
        }
        public async Task<IActionResult> Index()
        {
            await _indexService.IndexAllAsync(); // ← ANA SAYFA AÇILIRKEN INDEXLENİR
            return View();
        }   
    }
}
