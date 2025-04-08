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
			builder.Property(i => i.KapatmaTarihi)
	.HasComputedColumnSql(
		"CONVERT(varchar(10), KapatmaGunu, 104) + ' ' + CONVERT(varchar(5), KapatmaSaati, 108)",
		stored: true)
	.HasMaxLength(20)
	.IsRequired();
		}
	}

}
