using System;
using System.ComponentModel.DataAnnotations;

namespace TeknikServis.Web.Models
{
	public class TeslimViewModel
	{
		public int IsEmriTeslimId { get; set; }

		[Required(ErrorMessage = "Ödeme şekli gereklidir.")]
		public string OdemeSekli { get; set; }

		[Required(ErrorMessage = "Alınan ödeme tutarı gereklidir.")]
		[Range(0, int.MaxValue, ErrorMessage = "Ödeme negatif olamaz.")]
		public decimal AlinanOdeme { get; set; }

		[Required(ErrorMessage = "Teslim tarihi zorunludur.")]
		public DateTime KapatmaGunu { get; set; }

		[Required(ErrorMessage = "Teslim saati zorunludur.")]
		public TimeSpan KapatmaSaati { get; set; }

		[Required(ErrorMessage = "Sipariş durumu zorunludur.")]
		public string SiparisDurumu { get; set; }

		[StringLength(500)]
		public string? TeslimatAciklama { get; set; }
	}
}
