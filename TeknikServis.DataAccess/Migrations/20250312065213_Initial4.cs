using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeknikServis.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class Initial4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "IsEmriTeslim",
                columns: table => new
                {
                    IsEmriTeslimId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MusteriId = table.Column<int>(type: "int", nullable: false),
                    TeslimId = table.Column<int>(type: "int", nullable: false),
                    Marka = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Model = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GelisTarih = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ArizaDurumu = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Yil = table.Column<int>(type: "int", nullable: false),
                    GarantiDurumu = table.Column<int>(type: "int", nullable: false),
                    ServisTalebi = table.Column<int>(type: "int", nullable: false),
                    FisNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Kapali = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IsEmriTeslim", x => x.IsEmriTeslimId);
                    table.ForeignKey(
                        name: "FK_IsEmriTeslim_Musteris_MusteriId",
                        column: x => x.MusteriId,
                        principalTable: "Musteris",
                        principalColumn: "MusteriId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_IsEmriTeslim_MusteriId",
                table: "IsEmriTeslim",
                column: "MusteriId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IsEmriTeslim");
        }
    }
}
