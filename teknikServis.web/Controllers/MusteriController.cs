﻿using Microsoft.AspNetCore.Mvc;
using TeknikServis.Business.Abstract;
using TeknikServis.Entities.Servis;
using System.Threading.Tasks;

namespace TeknikServis.Web.Controllers
{
	public class MusteriController : Controller
	{
		private readonly IMusteriService _service;
		public MusteriController(IMusteriService service)
		{
			_service = service;
		}
		public async Task<IActionResult> Index()
		{
			var model = await _service.GetRecentAsync();
			return View(model);
		}
		public IActionResult Create()
			=> View();		
		[HttpPost]
		public async Task<IActionResult> Create(Musteri musteri)
		{
			if (!ModelState.IsValid)
				return View(musteri);

			await _service.CreateAsync(musteri);
			return RedirectToAction(nameof(Index));
		}
		public async Task<IActionResult> Edit(int id)
		{
			var m = await _service.GetByIdAsync(id);
			if (m == null) return NotFound();
			return View(m);
		}
		[HttpPost]
		public async Task<IActionResult> Edit(Musteri musteri)
		{
			if (!ModelState.IsValid)
				return View(musteri);

			await _service.UpdateAsync(musteri);
			TempData["Ok"] = $"{musteri.Ad} başarıyla güncellendi";
			return RedirectToAction(nameof(Index));
		}
		public async Task<IActionResult> Delete(int MusteriId)
		{
			var m = await _service.GetByIdAsync(MusteriId);
			if (m == null) return NotFound();

			await _service.DeleteAsync(MusteriId);
			TempData["Ok"] = $"{m.Ad} başarıyla silindi";
			return RedirectToAction(nameof(Index));
		}

	}
}
