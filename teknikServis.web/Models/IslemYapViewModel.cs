using TeknikServis.Entities.Servis;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace TeknikServis.Web.Models
{
	public class IslemYapViewModel
	{
		public Islem YeniIslem { get; set; } = new Islem();
		public IEnumerable<Islem>? MevcutIslemler { get; set; } = default;
		[ValidateNever]               // ← TeslimBilgisi’ni bind & validate etme
		public IsEmriTeslim TeslimBilgisi { get; set; } = default;
		public TeslimViewModel? YeniTeslimBilgisi { get; set; } =default;


	}
}
	