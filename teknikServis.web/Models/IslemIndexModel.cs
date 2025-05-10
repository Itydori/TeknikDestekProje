namespace teknikServis.web.Models
{   
    public class IslemIndexModel
    {
        public int Id { get; set; }
        public string MusteriAd { get; set; }
        public string Marka { get; set; }
        public string Model { get; set; }
        public string GarantiDurumu { get; set; }
        public string FisNo { get; set; }
        public DateTime Tarih { get; set; }
        public decimal Ucret { get; set; }
    }
}
