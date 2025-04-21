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
			builder.Property(i => i.GelisTarih)
		   .HasDefaultValueSql("GETDATE()");
			builder.Property(i => i.Kapali)
				   .HasDefaultValue(false);

			// Computed column için NULL kontrolü ekleyelim
			//builder.Property(i => i.KapatmaTarihi)
			//    .HasComputedColumnSql(
			//        "CASE WHEN KapatmaGunu IS NULL OR KapatmaSaati IS NULL " +
			//        "THEN CONVERT(varchar(10), GETDATE(), 104) + ' ' + CONVERT(varchar(5), CAST(GETDATE() AS time), 108) " +
			//        "ELSE CONVERT(varchar(10), KapatmaGunu, 104) + ' ' + CONVERT(varchar(5), KapatmaSaati, 108) END",
			//        stored: true)
			//    .HasMaxLength(20)
			//    .IsRequired();
			builder.Property(i => i.KapatmaGunu)
		  .IsRequired(false);
			builder.Property(i => i.KapatmaSaati)
				   .IsRequired(false);
			builder.Property(i => i.AlinanOdeme)
				   .IsRequired(false);
			builder.Property(i => i.OdemeSekli)
				   .IsRequired(false);
			builder.Property(i => i.SiparisDurumu)
				   .IsRequired(false);
			builder.Property(i => i.TeslimatAciklama)
				   .IsRequired(false);
			// Diğer default değerler
			builder.Ignore(x => x.KapatmaTarihi);
			//builder.HasQueryFilter(i => !i.Kapali);
		}
	}
}
