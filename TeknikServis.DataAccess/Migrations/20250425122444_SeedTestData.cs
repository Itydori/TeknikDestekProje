using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeknikServis.DataAccess.Migrations
{
    public partial class SeedTestData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
DECLARE @i INT = 1;
WHILE @i <= 100
BEGIN
    INSERT INTO Musteris (Ad, Telefon, Adres, Eposta, Aktif)
    VALUES (
        CONCAT('TestMusteri_', @i),
        CONCAT('555000', RIGHT('0000' + CAST(@i AS VARCHAR(4)),4)),
        CONCAT('Adres ', @i),
        CONCAT('test', @i, '@mail.com'),
        1
    );
    DECLARE @musteriId INT = SCOPE_IDENTITY();

    INSERT INTO IsEmriTeslimler (
        MusteriId, Marka, Model, GelisTarih, ArizaDurumu, Yil, Kapali,
        FisNo, GarantiDurumu, ServisTalebi, OdemeSekli, AlinanOdeme,
        KapatmaGunu, KapatmaSaati, SiparisDurumu, TeslimatAciklama
    )
    VALUES (
        @musteriId,
        'Marka_' + CAST((@i % 10 + 1) AS VARCHAR),
        'Model_' + CAST((@i % 20 + 1) AS VARCHAR),
        GETDATE(),
        'Arıza açıklaması ' + CAST(@i AS VARCHAR),
        YEAR(GETDATE()),
        0,
        CONCAT('FIS', RIGHT('000000' + CAST(@i AS VARCHAR),6)),
        CASE WHEN @i % 2 = 0 THEN 1 ELSE 2 END,
        1,
        'Nakit',
        0,
        GETDATE(),
        CONVERT(time, GETDATE()),
        'Sipariş verilmedi',
        'Teslim edilmedi'
    );
    DECLARE @teslimId INT = SCOPE_IDENTITY();

    INSERT INTO Islemler (
        OnarimYapan, OnarimTarihi, StokYeri, YapilanIslemler,
        Ucret, Aciklama, IsEmriTeslimId
    )
    VALUES (
        'Teknisyen_' + CAST((@i % 5 + 1) AS VARCHAR),
        DATEADD(DAY, -(@i % 30), GETDATE()),
        'StokYeri_' + CAST((@i % 3 + 1) AS VARCHAR),
        'Yapılan işlem ' + CAST(@i AS VARCHAR),
        @i * 10,
        'Açıklama ' + CAST(@i AS VARCHAR),
        @teslimId
    );

    SET @i += 1;
END
");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
DELETE FROM Islemler WHERE YapilanIslemler LIKE 'Yapılan işlem %';
DELETE FROM IsEmriTeslimler WHERE FisNo LIKE 'FIS%';
DELETE FROM Musteris WHERE Ad LIKE 'TestMusteri_%';
");
        }
    }
}
