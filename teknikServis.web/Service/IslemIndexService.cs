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

        foreach (var islem in islemler)
        {
            if (islem.IsEmriTeslimler is null || islem.IsEmriTeslimler.Musteri is null)
                continue;

            var doc = new IslemIndexModel
            {
                Id = islem.IslemId,
                MusteriAd = islem.IsEmriTeslimler.Musteri?.Ad ?? "Bilinmiyor",
                Marka = islem.IsEmriTeslimler?.Marka ?? "Yok",
                Model = islem.IsEmriTeslimler?.Model ?? "Yok",
                FisNo = islem.IsEmriTeslimler?.FisNo ?? "Yok",
                GarantiDurumu = (int)islem.IsEmriTeslimler.GarantiDurumu == 1 ? "Garantili" : "Garantisiz",
                Tarih = islem.OnarimTarihi,
                Ucret = islem.Ucret
            };
            await _client.IndexDocumentAsync(doc);
        }
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
