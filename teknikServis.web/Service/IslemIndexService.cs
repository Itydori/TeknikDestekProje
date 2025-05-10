using Microsoft.EntityFrameworkCore;
using Nest;
using teknikServis.web.Models;
using TeknikServis.DataAccess;

public class IslemIndexService
{
    private readonly TeknikServisDbContext _ctx;
    private readonly ElasticClient _client;

    public IslemIndexService(TeknikServisDbContext ctx, IConfiguration config)
    {
        _ctx = ctx;

        var settings = new ConnectionSettings(new Uri(config["Elastic:Uri"]))
                .ServerCertificateValidationCallback((o, certificate, chain, errors) => true) // ← SSL kontrolünü geç
    .BasicAuthentication(config["Elastic:Username"], config["Elastic:Password"])
    .DefaultIndex("islemler");

        _client = new ElasticClient(settings);
    }

    public async Task IndexAllAsync()
    {
        var islemler = await _ctx.Islemler
            .Include(x => x.IsEmriTeslimler)
                .ThenInclude(t => t.Musteri)
            .AsNoTracking()
            .ToListAsync();

        Console.WriteLine($"TOPLAM {islemler.Count} adet işlem bulundu.");

        int indexlenen = 0;
        int atlanan = 0;

        foreach (var islem in islemler)
        {
            var teslim = islem.IsEmriTeslimler;
            var musteri = teslim?.Musteri;

            if (teslim == null || musteri == null)
            {
                Console.WriteLine($"SKIPPED → ID: {islem.IslemId} (Teslim ya da Müşteri null)");
                atlanan++;
                continue;
            }

            var doc = new IslemIndexModel
            {
                Id = islem.IslemId,
                MusteriAd = musteri.Ad ?? "Bilinmiyor",
                Marka = teslim.Marka ?? "Markasız",
                Model = teslim.Model ?? "Modelsiz",
                FisNo = teslim.FisNo ?? "Fissiz",
                GarantiDurumu = teslim.GarantiDurumu.ToString(),
                Tarih = islem.OnarimTarihi,
                Ucret = islem.Ucret
            };

            Console.WriteLine($"INDEX → ID: {doc.Id}, Müşteri: {doc.MusteriAd}, Marka: {doc.Marka}");
            await _client.IndexDocumentAsync(doc);
            indexlenen++;
        }

        Console.WriteLine($"🟢 Indexlenen: {indexlenen} kayıt");
        Console.WriteLine($"⚠️  Atlanan (null yüzünden): {atlanan} kayıt");
    }
    public async Task<List<IslemIndexModel>> TestElasticSearchAsync()
    {
        var result = await _client.SearchAsync<IslemIndexModel>(s => s
            .Query(q => q.MatchAll())
            .Size(10)
        );

        return result.Documents.ToList();
    }
}
