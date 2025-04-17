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
	public class IslemConfiguration : IEntityTypeConfiguration<Islem>
	{
		public void Configure(EntityTypeBuilder<Islem> builder)
		{
			builder.HasKey(i=>i.IslemId);
			builder.Property(i => i.StokYeri).HasMaxLength(200).IsRequired();
			builder.Property(i => i.OnarimYapan).HasMaxLength(200).IsRequired();
			builder.Property(i => i.YapilanIslemler).HasMaxLength(1000).IsRequired();
			builder.Property(i => i.Aciklama).HasMaxLength(750).IsRequired();

		}
	}
}
