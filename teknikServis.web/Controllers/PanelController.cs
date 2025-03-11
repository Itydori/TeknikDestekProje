using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using teknikServis.web.Models;

namespace teknikServis.web.Controllers
{
    public class PanelController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}