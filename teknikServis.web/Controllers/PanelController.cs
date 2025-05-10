using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using teknikServis.web.Models;

namespace teknikServis.web.Controllers
{
    public class PanelController : Controller
    {
        private readonly IPanelReportService _srv;
        public PanelController(IPanelReportService srv) => _srv = srv;
        public async Task<IActionResult> Index([FromQuery] PanelReportFilter f)
                => View(await _srv.GetAsync(f));
    }
}