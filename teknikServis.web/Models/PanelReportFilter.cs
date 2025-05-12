namespace teknikServis.web.Models
{
    public class PanelReportFilter
    {
        public DateTime? Tarihten { get; set; }
        public DateTime? Tarihe { get; set; }
        public string? MusteriAd { get; set; }
        public string? Marka { get; set; }
        public string? Model { get; set; }
        public string? FisNo { get; set; }
    }
}
