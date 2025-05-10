public class PanelReportRow
{
    public DateTime Tarih { get; set; }
    public string MusteriAd { get; set; }
    public string Marka { get; set; }
    public string Model { get; set; }
    public string Garanti { get; set; }
    public string? FisNo { get; set; }
    public decimal Ucret { get; set; }
}

public class PanelReportViewModel
{
    public DateTime? Tarihten { get; set; }
    public DateTime? Tarihe { get; set; }
    public string? MusteriAd { get; set; }
    public string? Marka { get; set; }
    public string? Model { get; set; }
    public string? FisNo { get; set; }

    public List<PanelReportRow> Rows { get; set; } = new();
    public int ToplamIs => Rows.Count;
    public decimal ToplamUcret => Rows.Sum(x => x.Ucret);
    public int ToplamGarantiKapsamıIs => Rows.Count(x => x.Garanti == "Garantili");
}