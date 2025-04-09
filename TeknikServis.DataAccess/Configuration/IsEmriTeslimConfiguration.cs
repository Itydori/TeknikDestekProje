using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeknikServis.Entities.Servis;

namespace TeknikServis.DataAccess.Configuration
{
	public class IsEmriTeslimConfiguration : IEntityTypeConfiguration<IsEmriTeslim>
	{
        public void Configure(EntityTypeBuilder<IsEmriTeslim> builder)
        {
            // Default değerleri SQL tarafında tanımla
            builder.Property(i => i.KapatmaGunu).HasDefaultValueSql("GETDATE()");
            builder.Property(i => i.KapatmaSaati).HasDefaultValueSql("CAST(GETDATE() AS time)");

            // Computed column için NULL kontrolü ekleyelim
            //builder.Property(i => i.KapatmaTarihi)
            //    .HasComputedColumnSql(
            //        "CASE WHEN KapatmaGunu IS NULL OR KapatmaSaati IS NULL " +
            //        "THEN CONVERT(varchar(10), GETDATE(), 104) + ' ' + CONVERT(varchar(5), CAST(GETDATE() AS time), 108) " +
            //        "ELSE CONVERT(varchar(10), KapatmaGunu, 104) + ' ' + CONVERT(varchar(5), KapatmaSaati, 108) END",
            //        stored: true)
            //    .HasMaxLength(20)
            //    .IsRequired();

        
            builder.Property(i => i.KapatmaTarihi)
    .HasComputedColumnSql(
        "CASE WHEN KapatmaGunu IS NULL OR KapatmaSaati IS NULL " +
        "THEN CONVERT(varchar(10), GETDATE(), 104) + ' ' + CONVERT(varchar(5), CAST(GETDATE() AS time), 108) " +
        "ELSE CONVERT(varchar(10), KapatmaGunu, 104) + ' ' + CONVERT(varchar(5), KapatmaSaati, 108) END",
        stored: true)  // persist edilen column yerine, hesaplanan column olarak bırakıyoruz.
    .HasMaxLength(20)
    .IsRequired();
            // Diğer default değerler
            builder.Property(i => i.AlinanOdeme).HasDefaultValue(0);
            builder.Property(i => i.TeslimatAciklama).HasDefaultValue("Teslimat yapılmadı");
            builder.Property(i => i.SiparisDurumu).HasDefaultValue("Sipariş verilmedi");
            builder.Property(i => i.OdemeSekli).HasDefaultValue("Nakit");
            builder.Property(i => i.GelisTarih).HasDefaultValueSql("GETDATE()"); // Bu da SQL ifadesi olmalı
            builder.Property(i => i.Kapali).HasDefaultValue(false);
            //builder.HasQueryFilter(i => !i.Kapali);
        }
    }
}
