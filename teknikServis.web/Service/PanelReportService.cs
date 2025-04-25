// teknikServis.web/Services/PanelReportService.cs
using teknikServis.web.Models;
using TeknikServis.DataAccess;
using Microsoft.EntityFrameworkCore;
using Nest;

public interface IPanelReportService
{
    Task<PanelReportViewModel> GetAsync(PanelReportFilter filter);
}

public class PanelReportService : IPanelReportService
{
    private readonly ElasticClient _client;

    public PanelReportService(IConfiguration config)
    {
        var settings = new ConnectionSettings(new Uri(config["Elastic:Uri"]))
                .ServerCertificateValidationCallback((o, certificate, chain, errors) => true) // ← SSL kontrolünü geç
       .BasicAuthentication(config["Elastic:Username"], config["Elastic:Password"])
       .DefaultIndex("islemler");

        _client = new ElasticClient(settings);

    }

    public async Task<PanelReportViewModel> GetAsync(PanelReportFilter f)
    {
        // 🔥 FILTRE YOKSA → TÜM VERİYİ GETİR
        if (string.IsNullOrWhiteSpace(f.MusteriAd)
            && string.IsNullOrWhiteSpace(f.Marka)
            && string.IsNullOrWhiteSpace(f.Model)
            && string.IsNullOrWhiteSpace(f.FisNo)
            && !f.Tarihten.HasValue
            && !f.Tarihe.HasValue)
        {
            var response = await _client.SearchAsync<IslemIndexModel>(s => s
                .Query(q => q.MatchAll())
                .Size(10000)
            );

            var allRows = response.Documents.Select(x => new PanelReportRow
            {
                Tarih = x.Tarih,
                MusteriAd = x.MusteriAd,
                Marka = x.Marka,
                Model = x.Model,
                Garanti = x.GarantiDurumu,
                FisNo = x.FisNo,
                Ucret = x.Ucret
            }).ToList();

            return new PanelReportViewModel
            {
                Rows = allRows
            };
        }

        // 🔎 FILTRE VARSA → QUERY YAP
        var query = new List<QueryContainer>();

        if (!string.IsNullOrWhiteSpace(f.MusteriAd))
            query.Add(new MatchQuery { Field = "musteriAd", Query = f.MusteriAd });

        if (!string.IsNullOrWhiteSpace(f.Marka))
            query.Add(new MatchQuery { Field = "marka", Query = f.Marka });

        if (!string.IsNullOrWhiteSpace(f.Model))
            query.Add(new MatchQuery { Field = "model", Query = f.Model });

        if (!string.IsNullOrWhiteSpace(f.FisNo))
            query.Add(new TermQuery { Field = "fisNo.keyword", Value = f.FisNo });

        if (f.Tarihten.HasValue || f.Tarihe.HasValue)
        {
            var range = new DateRangeQuery
            {
                Field = "tarih"
            };

            if (f.Tarihten.HasValue)
                range.GreaterThanOrEqualTo = f.Tarihten.Value.Date;

            if (f.Tarihe.HasValue)
                range.LessThanOrEqualTo = f.Tarihe.Value.Date.AddDays(1).AddTicks(-1);

            query.Add(range);
        }

        var filteredResponse = await _client.SearchAsync<IslemIndexModel>(s => s
            .Query(q => q.Bool(b => b.Must(query.ToArray())))
            .Size(10000)
        );

        var rows = filteredResponse.Documents.Select(x => new PanelReportRow
        {
            Tarih = x.Tarih,
            MusteriAd = x.MusteriAd,
            Marka = x.Marka,
            Model = x.Model,
            Garanti = x.GarantiDurumu,
            FisNo = x.FisNo,
            Ucret = x.Ucret
        }).ToList();

        return new PanelReportViewModel
        {
            Tarihten = f.Tarihten,
            Tarihe = f.Tarihe,
            MusteriAd = f.MusteriAd,
            Marka = f.Marka,
            Model = f.Model,
            FisNo = f.FisNo,
            Rows = rows
        };
    }
}