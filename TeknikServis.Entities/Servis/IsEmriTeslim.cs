using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeknikServis.Entities.Servis
{
	public class IsEmriTeslim
	{
		[Key]
		public int IsEmriTeslimId { get; set; }
		public int MusteriId { get; set; }

        public Musteri Musteri { get; set; }
        public string Marka { get; set; }
		public string Model { get; set; }
		public DateTime GelisTarih { get; set; }
		public string ArizaDurumu { get; set; }
		public int Yil { get; set; }
		public bool Kapali { get; set; }
		public string FisNo { get; set; }
		public GarantiDurumuEnum GarantiDurumu { get; set; }
		public ServisTalebiEnum ServisTalebi { get; set; }
		public List<İslem> Islems{ get; set; }
	}
	public enum GarantiDurumuEnum
	{
		Garantili = 1,
		Garantisiz = 2
	}
	public enum ServisTalebiEnum
	{
		ArizaliUrun = 1,
		YedekParcaSiparis = 2
	}
}
