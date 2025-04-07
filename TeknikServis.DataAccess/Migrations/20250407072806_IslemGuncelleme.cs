using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeknikServis.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class IslemGuncelleme : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "İslem",
                columns: table => new
                {
                    IslemId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsEmriId = table.Column<int>(type: "int", nullable: false),
                    OnarimYapan = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    OnarimTarihi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StokYeri = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    YapilanIslemler = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    Ucret = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Aciklama = table.Column<string>(type: "nvarchar(750)", maxLength: 750, nullable: false),
                    IsEmriTeslimId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_İslem", x => x.IslemId);
                    table.ForeignKey(
                        name: "FK_İslem_IsEmriTeslim_IsEmriTeslimId",
                        column: x => x.IsEmriTeslimId,
                        principalTable: "IsEmriTeslim",
                        principalColumn: "IsEmriTeslimId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_İslem_IsEmriTeslimId",
                table: "İslem",
                column: "IsEmriTeslimId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "İslem");
        }
    }
}
