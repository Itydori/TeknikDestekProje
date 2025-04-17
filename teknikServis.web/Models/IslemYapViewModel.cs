using TeknikServis.Entities.Servis;
using System.Collections.Generic;

namespace TeknikServis.Web.Models
{
	public class IslemYapViewModel
	{
		public Islem YeniIslem { get; set; } = new Islem();
		public IEnumerable<Islem> MevcutIslemler { get; set; } = new List<Islem>();
		public IsEmriTeslim TeslimBilgisi { get; set; } = new IsEmriTeslim();
	}
}
