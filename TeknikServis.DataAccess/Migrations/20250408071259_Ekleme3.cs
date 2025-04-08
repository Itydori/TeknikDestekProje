using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeknikServis.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class Ekleme3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AlinanOdeme",
                table: "IsEmriTeslim",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "KapatmaGunu",
                table: "IsEmriTeslim",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<TimeSpan>(
                name: "KapatmaSaati",
                table: "IsEmriTeslim",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AddColumn<string>(
                name: "OdemeSekli",
                table: "IsEmriTeslim",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SiparisDurumu",
                table: "IsEmriTeslim",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TeslimatAciklama",
                table: "IsEmriTeslim",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "KapatmaTarihi",
                table: "IsEmriTeslim",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                computedColumnSql: "CONVERT(varchar(10), KapatmaGunu, 104) + ' ' + CONVERT(varchar(5), KapatmaSaati, 108)",
                stored: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "KapatmaTarihi",
                table: "IsEmriTeslim");

            migrationBuilder.DropColumn(
                name: "AlinanOdeme",
                table: "IsEmriTeslim");

            migrationBuilder.DropColumn(
                name: "KapatmaGunu",
                table: "IsEmriTeslim");

            migrationBuilder.DropColumn(
                name: "KapatmaSaati",
                table: "IsEmriTeslim");

            migrationBuilder.DropColumn(
                name: "OdemeSekli",
                table: "IsEmriTeslim");

            migrationBuilder.DropColumn(
                name: "SiparisDurumu",
                table: "IsEmriTeslim");

            migrationBuilder.DropColumn(
                name: "TeslimatAciklama",
                table: "IsEmriTeslim");
        }
    }
}
